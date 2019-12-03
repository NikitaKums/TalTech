//
//  JsonAPI.swift
//  Radio_iOS
//
//  Created by user154645 on 5/11/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import Foundation

class JsonAPI {
    // @escaping - passed in delegate will live longer then the actual body of function
    func getCurrentSong(jsonUrl: String,
                        completionHandler: @escaping (_ success: Bool, _ artist: String?, _ title: String?)->() ){
        
        get(request: clientURLRequest(path: jsonUrl)) { (success: Bool, responseObject: Any) in
            DispatchQueue.main.async(execute: {
                if success {
                    var artist = "-"
                    var title = "-"
                    // parse json
                    if let jsonDict = responseObject as? [String: Any],
                        let songHistoryList = jsonDict["SongHistoryList"] as? [Any],
                        let currentSong = songHistoryList.first as? [String: Any] {
                        artist = currentSong["Artist"] as? String ?? "😂"
                        title = currentSong["Title"] as? String ?? "💩"
                    }
                    completionHandler(true, artist, title)
                } else {
                    completionHandler(false, nil, nil)
                }
            })
        }
        
        
        
    }
    
    // should do also post, put, delete, ...
    private func get(request: URLRequest,
                     completionHandler: @escaping (_ success: Bool, _ object: Any)->() ){
        dataTask(request: request, method: "GET", completionHandler: completionHandler)
    }
    
    
    
    private func dataTask(request: URLRequest, method: String,
                          completionHandler: @escaping (_ success: Bool, _ object: Any?)->()){
        var request = request
        request.httpMethod = method
        
        let session = URLSession(configuration: URLSessionConfiguration.default)
        session
            .dataTask(with: request,
                      completionHandler: { (data: Data?, response: URLResponse?, error: Error?) in
                        if let data = data {
                            let json = try? JSONSerialization.jsonObject(with: data, options: [])
                            
                            // match a value with a range of values,
                            // by checking whether the value is contained within the range
                            if let response = response as? HTTPURLResponse, 200...299 ~= response.statusCode    {
                                completionHandler(true, json)
                            } else {
                                completionHandler(false, json)
                            }
                        }
            })
            // start it!
            .resume()
    }
    
    
    
    private func clientURLRequest(path: String) -> URLRequest {
        var request = URLRequest(url: URL(string: path)!)
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")
        return request
    }
    
}

