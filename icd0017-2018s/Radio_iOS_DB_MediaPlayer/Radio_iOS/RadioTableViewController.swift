//
//  RadioTableViewController.swift
//  Radio_iOS
//
//  Created by user154645 on 5/11/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import UIKit

class RadioTableViewController: UITableViewController, UIPickerViewDelegate, UIPickerViewDataSource {
    
    @IBOutlet weak var stationPicker: UIPickerView!
    
    var artistRepository: ArtistRepository!
    var stationRepository: StationRepository!
    
    var artists: [Artist] = []
    var stations: [Station] = []
    
    var currentStationForData: Station!

    override func viewDidLoad() {
        super.viewDidLoad()
        self.tableView.backgroundColor = #colorLiteral(red: 0, green: 0, blue: 0, alpha: 1)
        stationPicker.delegate = self
        stationPicker.dataSource = self
        let appDelegate = UIApplication.shared.delegate as! AppDelegate
        let container = appDelegate.persistentContainer
        artistRepository = ArtistRepository(withContainer: container)
        stationRepository = StationRepository(withContainer: container)

        // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
        // self.navigationItem.rightBarButtonItem = self.editButtonItem
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        currentStationForData = currentStation
        artists = artistRepository.allByStation(station: currentStationForData!) ?? []
        do {
            try stations = stationRepository.all()
        } catch {
        }
        self.tableView.reloadData()
    }

    // MARK: - Table view data source
    
    func numberOfComponents(in pickerView: UIPickerView) -> Int {
        return 1
    }
    
    func pickerView(_ pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        return stations.count
    }
    
    func pickerView(_ pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        currentStationForData = stationRepository.getStation(withName: stations[row].stationName!)
        artists = artistRepository.allByStation(station: currentStationForData!) ?? []
        self.tableView.reloadData()
    }
    
    func pickerView(_ pickerView: UIPickerView, attributedTitleForRow row: Int, forComponent component: Int) -> NSAttributedString? {
        let color: UIColor = #colorLiteral(red: 0.9764705882, green: 0.8274509804, blue: 0.2588235294, alpha: 1)
        let attributedString = NSAttributedString(string: stations[row].stationName!, attributes: [NSAttributedString.Key.foregroundColor : color])
        
        return attributedString
    }

    override func numberOfSections(in tableView: UITableView) -> Int {
        // #warning Incomplete implementation, return the number of sections
        return artists.count
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        // #warning Incomplete implementation, return the number of rows
        //return artists[section].songs.count
        return artists[section].songs?.count ?? 0
    }

    override func tableView(_ tableView: UITableView, willDisplayHeaderView view: UIView, forSection section: Int) {
        
        let header = view as? UITableViewHeaderFooterView
        header?.textLabel?.textColor = #colorLiteral(red: 0.9764705882, green: 0.8274509804, blue: 0.2588235294, alpha: 1)
        header?.textLabel?.font = UIFont.boldSystemFont(ofSize: 20)
    
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "ArtistCell", for: indexPath)
        
        if let songs = artists[indexPath.section].songs?.allObjects as? [Song]{
            cell.textLabel?.text = songs[indexPath.row].songTitle
            cell.detailTextLabel?.text = "Played \(songs[indexPath.row].timesPlayed) times"
        }
        return cell
    }
    
    override func tableView(_ tableView: UITableView, titleForHeaderInSection section: Int) -> String? {
        return artists[section].artistName
    }
    
    override var preferredStatusBarStyle: UIStatusBarStyle {
        return .lightContent
    }
 

    /*
    // Override to support conditional editing of the table view.
    override func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        // Return false if you do not want the specified item to be editable.
        return true
    }
    */

    /*
    // Override to support editing the table view.
    override func tableView(_ tableView: UITableView, commit editingStyle: UITableViewCell.EditingStyle, forRowAt indexPath: IndexPath) {
        if editingStyle == .delete {
            // Delete the row from the data source
            tableView.deleteRows(at: [indexPath], with: .fade)
        } else if editingStyle == .insert {
            // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view
        }    
    }
    */

    /*
    // Override to support rearranging the table view.
    override func tableView(_ tableView: UITableView, moveRowAt fromIndexPath: IndexPath, to: IndexPath) {

    }
    */

    /*
    // Override to support conditional rearranging of the table view.
    override func tableView(_ tableView: UITableView, canMoveRowAt indexPath: IndexPath) -> Bool {
        // Return false if you do not want the item to be re-orderable.
        return true
    }
    */

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
    }
    */

}
