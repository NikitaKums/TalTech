//
//  ViewController.swift
//  iOS15Puzzlev2
//
//  Created by user154645 on 5/5/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import UIKit

class MasterViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        //self.view.backgroundColor = Theme.current.background
        //UIButton.appearance().setTitleColor(Theme.current.buttonTextColor, for: .normal)
        // Do any additional setup after loading the view.
    }

    
     // MARK: - Navigation
     
     // In a storyboard-based application, you will often want to do a little preparation before navigation
     override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
     // Get the new view controller using segue.destination.
     // Pass the selected object to the new view controller.
        print(segue.identifier ?? "no identifier")

        if let segueId = segue.identifier {
            switch segueId {
                case "ShowGame":
                    if let gvc = segue.destination as? GameViewController {
                        gvc.boardXSize = boardXFinalSize
                        gvc.boardYSize = boardYFinalSize
                    }
                case "ShowSettings":
                    print("")
                default:
                    break
            }
        }
     }
    
}

