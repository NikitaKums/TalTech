//
//  Station.swift
//  Radio_iOS
//
//  Created by user154645 on 5/11/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import Foundation

class Station {
    var id: Int
    var stationName: String
    
    var stationListenUrl: String
    var stationJSONUrl: String
    
    convenience init(stationName: String, stationListenUrl: String, stationJSONUrl: String){
        self.init(id: 0, stationName: stationName, stationListenUrl: stationListenUrl, stationJSONUrl: stationJSONUrl)
    }
    
    init(id: Int, stationName: String, stationListenUrl: String, stationJSONUrl: String){
        self.id = id
        self.stationName = stationName
        self.stationListenUrl = stationListenUrl
        self.stationJSONUrl = stationJSONUrl
    }
}
