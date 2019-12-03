//
//  Song+CoreDataProperties.swift
//  Radio_iOS
//
//  Created by user153854 on 5/18/19.
//  Copyright © 2019 user154645. All rights reserved.
//
//

import Foundation
import CoreData


extension Song {

    @nonobjc public class func fetchRequest() -> NSFetchRequest<Song> {
        return NSFetchRequest<Song>(entityName: "Song")
    }

    @NSManaged public var songTitle: String?
    @NSManaged public var timesPlayed: Int32
    @NSManaged public var artist: Artist?

}
