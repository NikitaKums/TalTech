//
//  SongRepository.swift
//  Radio_iOS
//
//  Created by user153854 on 5/18/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import Foundation
import CoreData

class SongRepository {
    
    var container: NSPersistentContainer!
    
    var context: NSManagedObjectContext{
        return container.viewContext
    }
    
    init(withContainer container: NSPersistentContainer){
        self.container = container
    }
    
    func all() throws -> [Song] {
        let songs = try context.fetch(Song.fetchRequest() as NSFetchRequest<Song>)
        return songs
    }
    
    func insert(song: Song) throws {
        context.insert(song)
        try context.save()
    }
    
    func update(song: Song) throws {
        try context.save()
    }
    
}

