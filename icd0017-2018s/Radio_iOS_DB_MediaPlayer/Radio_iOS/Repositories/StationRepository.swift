//
//  StationRepository.swift
//  Radio_iOS
//
//  Created by user153854 on 5/18/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import Foundation
import CoreData

class StationRepository {
    
    var container: NSPersistentContainer!
    
    var context: NSManagedObjectContext{
        return container.viewContext
    }
    
    init(withContainer container: NSPersistentContainer){
        self.container = container
    }
    
    func all() throws -> [Station] {
        let stations = try context.fetch(Station.fetchRequest() as NSFetchRequest<Station>)
        return stations
    }
    
    func insert(station: Station) throws {
        context.insert(station)
        try context.save()
    }
    
    func getStation(withName stationName: String) -> Station? {
        let request = NSFetchRequest<Station>(entityName: "Station")
        request.predicate = NSPredicate(format: "stationName == %@", stationName)
        do {
            let person = try context.fetch(request)
            return person.first!
        } catch let error {
            print(error.localizedDescription)
            return nil
        }
    }
    
    func insertStationsFirstTime() throws {
        let stations = ["SKYPLUS-http://sky.babahhcdn.com/SKYPLUS-http://dad.akaver.com/api/SongTitles/SP",
                        "RRAP-http://sky.babahhcdn.com/rrap-http://dad.akaver.com/api/SongTitles/RRAP",
                        "NRJ-http://sky.babahhcdn.com/NRJ-http://dad.akaver.com/api/SongTitles/NRJ",
                        "RR-http://sky.babahhcdn.com/RR-http://dad.akaver.com/api/SongTitles/RR",
                        "SKY-http://sky.babahhcdn.com/SKY-http://dad.akaver.com/api/SongTitles/SKY",
                        "RETRO-http://sky.babahhcdn.com/RETRO-http://dad.akaver.com/api/SongTitles/RETRO"]
        for station in stations {
            var stationInfo = station.split(separator: "-")
            let stationToInsert = Station(context: context)
            stationToInsert.stationName = String(stationInfo[0])
            stationToInsert.stationListenUrl = String(stationInfo[1])
            stationToInsert.stationJSONUrl = String(stationInfo[2])
            try insert(station: stationToInsert)
        }
    }
    
}
