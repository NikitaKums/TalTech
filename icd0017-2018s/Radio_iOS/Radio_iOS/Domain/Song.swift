//
//  Song.swift
//  Radio_iOS
//
//  Created by user154645 on 5/11/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import Foundation

class Song {
    var id: Int
    var songTitle: String
    var timesPlayed: Int
    
    convenience init(songTitle: String, timesPlayed: Int){
        self.init(id: 0, songTitle: songTitle, timesPlayed: timesPlayed)
    }
    
    init(id: Int, songTitle: String, timesPlayed: Int){
        self.id = id
        self.songTitle = songTitle
        self.timesPlayed = timesPlayed
    }
    
}
