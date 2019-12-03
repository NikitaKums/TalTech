//
//  Station+CoreDataProperties.swift
//  Radio_iOS
//
//  Created by user153854 on 5/18/19.
//  Copyright © 2019 user154645. All rights reserved.
//
//

import Foundation
import CoreData


extension Station {

    @nonobjc public class func fetchRequest() -> NSFetchRequest<Station> {
        return NSFetchRequest<Station>(entityName: "Station")
    }

    @NSManaged public var stationJSONUrl: String?
    @NSManaged public var stationListenUrl: String?
    @NSManaged public var stationName: String?
    @NSManaged public var artists: NSSet?

}

// MARK: Generated accessors for artists
extension Station {

    @objc(addArtistsObject:)
    @NSManaged public func addToArtists(_ value: Artist)

    @objc(removeArtistsObject:)
    @NSManaged public func removeFromArtists(_ value: Artist)

    @objc(addArtists:)
    @NSManaged public func addToArtists(_ values: NSSet)

    @objc(removeArtists:)
    @NSManaged public func removeFromArtists(_ values: NSSet)

}
