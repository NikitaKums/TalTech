//
//  ViewController.swift
//  Radio_iOS
//
//  Created by user154645 on 5/11/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import UIKit
var artists: [Artist] = []

class ViewController: UIViewController, UIPickerViewDelegate, UIPickerViewDataSource {
    
    let stations = [
        Station(stationName: "SKYPLUS", stationListenUrl: "http://sky.babahhcdn.com/SKYPLUS", stationJSONUrl: "http://dad.akaver.com/api/SongTitles/SP"),
        Station(stationName: "RRAP", stationListenUrl: "http://sky.babahhcdn.com/rrap", stationJSONUrl: "http://dad.akaver.com/api/SongTitles/RRAP"),
        Station(stationName: "NRJ", stationListenUrl: "http://sky.babahhcdn.com/NRJ", stationJSONUrl: "http://dad.akaver.com/api/SongTitles/NRJ"),
        Station(stationName: "RR", stationListenUrl: "http://sky.babahhcdn.com/RR", stationJSONUrl: "http://dad.akaver.com/api/SongTitles/RR"),
        Station(stationName: "SKY", stationListenUrl: "http://sky.babahhcdn.com/SKY", stationJSONUrl: "http://dad.akaver.com/api/SongTitles/SKY"),
        Station(stationName: "RETRO", stationListenUrl: "http://sky.babahhcdn.com/RETRO", stationJSONUrl: "http://dad.akaver.com/api/SongTitles/RETRO")
    ]
        
    var previousSongTitle = ""
    
    var currentStationListenUrl = ""
    var currentStationJSONUrl = ""
    
    var jsonAPI = JsonAPI()
    
    @IBOutlet weak var artistLabel: UILabel!
    @IBOutlet weak var titleLabel: UILabel!
    
    @IBOutlet weak var stationPicker: UIPickerView!
    
    var timer: Timer?
    var timeLeft = 15
    
    
    private func updateInfo(success: Bool, artist: String?, title: String?){
        if success {
            if artist != "", title != "", previousSongTitle != title {
                previousSongTitle = title ?? ""
                let song = Song(songTitle: title!, timesPlayed: 1)
                var appended = false
                for artistInArtists in artists {
                    if artistInArtists.artistName == artist {
                        var increasedSongTimesPlayed = false
                        for artistSongs in artistInArtists.songs {
                            if artistSongs.songTitle == title {
                                artistSongs.timesPlayed = artistSongs.timesPlayed + 1
                                increasedSongTimesPlayed = true
                                appended = true
                                break
                            }
                        }
                        if appended == true {
                            break
                        }
                        if increasedSongTimesPlayed == false {
                            artistInArtists.songs.append(song)
                            appended = true
                            break
                        }
                    }
                }
                if appended == false {
                    let newArtist = Artist(artistName: artist!)
                    newArtist.songs.append(song)
                    artists.append(newArtist)
                }
            }
            self.artistLabel.text = artist
            self.titleLabel.text = title
        } else {
            self.artistLabel.text = "Error in API"
            self.titleLabel.text = "Error in API"
        }
    }
    
    private func requestInfoUpdate(){
        jsonAPI.getCurrentSong(jsonUrl: currentStationJSONUrl, completionHandler: updateInfo)
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        stationPicker.delegate = self
        stationPicker.dataSource = self
        currentStationListenUrl = stations.first!.stationListenUrl
        currentStationJSONUrl = stations.first!.stationJSONUrl
        Timer.scheduledTimer(timeInterval: 1.0, target: self, selector: #selector(onTimerFires), userInfo: nil, repeats: true)
        requestInfoUpdate()
    }
    
    
    
    func numberOfComponents(in pickerView: UIPickerView) -> Int {
        return 1;
    }
    
    func pickerView(_ pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        return stations.count
    }
    
    
    
    func pickerView(_ pickerView: UIPickerView, attributedTitleForRow row: Int, forComponent component: Int) -> NSAttributedString? {
        let color: UIColor = #colorLiteral(red: 0.9764705882, green: 0.8274509804, blue: 0.2588235294, alpha: 1)
        let attributedString = NSAttributedString(string: stations[row].stationName, attributes: [NSAttributedString.Key.foregroundColor : color])
        return attributedString
    }
    func pickerView(_ pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        currentStationListenUrl = stations[row].stationListenUrl
        currentStationJSONUrl = stations[row].stationJSONUrl
        requestInfoUpdate()
    }
    
    @objc func onTimerFires()
    {
        timeLeft -= 1
        
        if timeLeft <= 0 {
            print("update info")
            requestInfoUpdate()
            timeLeft = 15
        }
    }
    
    override var preferredStatusBarStyle: UIStatusBarStyle {
        return .lightContent
    }
}


