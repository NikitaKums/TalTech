//
//  Artist.swift
//  Radio_iOS
//
//  Created by user154645 on 5/11/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import Foundation

class Artist {
    var id: Int
    var artistName: String
    var songs: [Song]
    
    convenience init(artistName: String){
        self.init(id: 0, artistName: artistName)
    }
    
    init(id: Int, artistName: String, songs: [Song]){
        self.id = id
        self.artistName = artistName
        self.songs = songs
    }
    
    init(id: Int, artistName: String){
        self.id = id
        self.artistName = artistName
        self.songs = []
    }
}
