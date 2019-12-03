//
//  ViewController.swift
//  Radio_iOS
//
//  Created by user154645 on 5/11/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import UIKit
import AVFoundation
import MediaPlayer
import CoreData

var currentStation: Station!

class ViewController: UIViewController, UIPickerViewDelegate, UIPickerViewDataSource {

    var stations: [Station] = []
    
    var stationRepository: StationRepository!
    var songRepository: SongRepository!
    var artistRepository: ArtistRepository!
    
    var container: NSPersistentContainer!
    
    @IBOutlet weak var controlButton: UIButton!
    
    var audioStatus = AudioStatus.stopped
    var audioPlayer: AVPlayer!
    
    var previousSongTitle = ""
    
    var currentStationListenUrl = ""
    var currentStationJSONUrl = ""
    
    var jsonAPI = JsonAPI()
    
    var requestJson = false
    
    var artists: [Artist] = []
    
    @IBOutlet weak var artistLabel: UILabel!
    @IBOutlet weak var titleLabel: UILabel!
    
    @IBOutlet weak var stationPicker: UIPickerView!
    
    var timer: Timer?
    var timeLeft = 15
    
    
    @IBAction func controlButtonTouchUpInside(_ sender: Any) {
        switch audioStatus{
        case .buffering, .playing:
            audioStop()
        case .stopped:
            audioPrepareForPlaying()
        }
    }
    
    
    private func updateInfo(success: Bool, artist: String?, title: String?){
        if success {
            print("success")
            artists = artistRepository.allByStation(station: currentStation) ?? []
            if artist != "", title != "", previousSongTitle != title {
                print("in")
                previousSongTitle = title ?? ""
                let song = Song(context: songRepository.context)
                song.songTitle = title!
                song.timesPlayed = 1
                var appended = false
                for artistInArtists in artists {
                    print("for loop")
                    if artistInArtists.artistName == artist {
                        var increasedSongTimesPlayed = false
                        for artistSong in artistInArtists.songs ?? [] {
                            if let artistSongg = artistSong as? Song, artistSongg.songTitle == title {
                                artistSongg.timesPlayed = artistSongg.timesPlayed + 1
                                do {
                                    try songRepository.update(song: artistSongg)
                                } catch {
                                    break
                                }
                                increasedSongTimesPlayed = true
                                appended = true
                                break
                            }
                        }
                        if appended == true {
                            break
                        }
                        if increasedSongTimesPlayed == false {
                            song.artist = artistInArtists
                            artistInArtists.addToSongs(song)
                                //try songRepository.insert(song: song)
                            appended = true
                            break
                        }
                    }
                }
                if appended == false {
                    let newArtist = Artist(context: artistRepository.context)
                    song.artist = newArtist
                    newArtist.artistName = artist!
                    newArtist.station = currentStation
                    do {
                        try artistRepository.insert(artist: newArtist)
                    } catch {
                    }
                    newArtist.addToSongs(song)
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
        if requestJson {
            jsonAPI.getCurrentSong(jsonUrl: currentStationJSONUrl, completionHandler: updateInfo)
        }
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        let appDelegate = UIApplication.shared.delegate as! AppDelegate
        container = appDelegate.persistentContainer
        
        stationRepository = StationRepository(withContainer: container)
        songRepository = SongRepository(withContainer: container)
        artistRepository = ArtistRepository(withContainer: container)
        
        do {
            stations = try stationRepository.all()
            if stations.count == 0 {
                try stationRepository.insertStationsFirstTime()
                stations = try stationRepository.all()
            }
        } catch let error {
            print(error)
        }
        
        stationPicker.delegate = self
        stationPicker.dataSource = self
        do {
            try AVAudioSession.sharedInstance().setCategory(AVAudioSession.Category(rawValue: "AVAudioSessionCategoryPlayback"))
            try AVAudioSession.sharedInstance().setActive(true)
        } catch {
            print("error in audio session \(error)")
        }
        currentStationListenUrl = stations.first!.stationListenUrl!
        currentStationJSONUrl = stations.first!.stationJSONUrl!
        currentStation = stations.first!
        Timer.scheduledTimer(timeInterval: 1.0, target: self, selector: #selector(onTimerFires), userInfo: nil, repeats: true)
    }
    
    
    
    func numberOfComponents(in pickerView: UIPickerView) -> Int {
        return 1;
    }
    
    func pickerView(_ pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        return stations.count
    }
    
    
    
    func pickerView(_ pickerView: UIPickerView, attributedTitleForRow row: Int, forComponent component: Int) -> NSAttributedString? {
        let color: UIColor = #colorLiteral(red: 0.9764705882, green: 0.8274509804, blue: 0.2588235294, alpha: 1)
        let attributedString = NSAttributedString(string: stations[row].stationName!, attributes: [NSAttributedString.Key.foregroundColor : color])
        
        return attributedString
    }
    func pickerView(_ pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        currentStation = stationRepository.getStation(withName: stations[row].stationName!)
        currentStationListenUrl = currentStation.stationListenUrl!
        currentStationJSONUrl = currentStation.stationJSONUrl!
        audioPrepareForPlaying()
    }
    
    func audioPrepareForPlaying(){
        controlButton.setTitle( "BUFFERING", for: .normal)
        audioStatus = AudioStatus.buffering
        audioPlayer = AVPlayer(url: URL(string: currentStationListenUrl)!)
        audioPlayer.addObserver(self, forKeyPath: "status", options: [], context: nil)
    }
    
    func audioStop(){
        controlButton.setTitle( "PLAY", for: .normal)
        audioStatus = AudioStatus.stopped
        requestJson = false
        self.artistLabel.text = "----"
        self.titleLabel.text = "----"
        if audioPlayer != nil {
            audioPlayer.pause()
            audioPlayer.removeObserver(self, forKeyPath: "status")
            audioPlayer = nil
        }
    }
    
    func audioPlay(){
        controlButton.setTitle( "STOP", for: .normal)
        requestJson = true
        requestInfoUpdate()
        audioStatus = AudioStatus.playing
        audioPlayer.play()
    }
    
    override func observeValue(forKeyPath keyPath: String?, of object: Any?, change: [NSKeyValueChangeKey : Any]?, context: UnsafeMutableRawPointer?) {
        if object is AVPlayer{
            switch audioPlayer.status{
            case .failed:
                print("audioplayer failed")
            case .readyToPlay:
                print("audioplayer ready to play")
                audioPlay()
            case .unknown:
                print("audioplayer unknown status")
            @unknown default:
                print("💩")
            }
        }
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


