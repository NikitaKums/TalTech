//
//  AristRepository.swift
//  Radio_iOS
//
//  Created by user153854 on 5/18/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import Foundation
import CoreData

class ArtistRepository {
    
    var container: NSPersistentContainer!
    
    var context: NSManagedObjectContext{
        return container.viewContext
    }
    
    init(withContainer container: NSPersistentContainer){
        self.container = container
    }
    
    func all() throws -> [Artist] {
        let artists = try context.fetch(Artist.fetchRequest() as NSFetchRequest<Artist>)
        return artists
    }
    
    func allByStation(station: Station) -> [Artist]?{
        let request = NSFetchRequest<Artist>(entityName: "Artist")
        print(station.stationName!)
        request.predicate = NSPredicate(format: "station.stationName == %@", station.stationName!)
        do {
            let artists = try context.fetch(request)
            for artist in artists {
                print(artist.artistName!)
            }
            return artists
        } catch let error {
            print(error.localizedDescription)
            return nil
        }
    }
    
    func insert(artist: Artist) throws {
        context.insert(artist)
        try context.save()
    }
    
}
