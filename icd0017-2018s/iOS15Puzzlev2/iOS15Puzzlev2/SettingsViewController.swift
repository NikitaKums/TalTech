//
//  SettingsViewController.swift
//  iOS15Puzzlev2
//
//  Created by user154645 on 5/5/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import UIKit

var boardXFinalSize: Int = 3
var boardYFinalSize: Int = 3

class SettingsViewController: UIViewController, UITextFieldDelegate {

    @IBOutlet weak var YTextField: UITextField!
    @IBOutlet weak var MessageTextField: UILabel!
    @IBOutlet weak var XTextField: UITextField!
    @IBOutlet var ColorButtons: [UIButton]!
    @IBOutlet var Switches: [UISwitch]!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        XTextField.text = String(boardXFinalSize)
        YTextField.text = String(boardYFinalSize)
        self.YTextField.delegate = self
        self.XTextField.delegate = self
        MessageTextField.text = "Current Board size -> X: \(boardXFinalSize) Y: \(boardYFinalSize)"
        for oneSwitch in Switches {
            if oneSwitch.tag == Theme.current.id {
                disableSwitches(exclude: oneSwitch)
            }
        }
        changeTheme()
        
        // Do any additional setup after loading the view.
    }
    @IBAction func XTextFieldChanged(_ sender: UITextField) {
        if (validateBoardSizeFrom(string: sender.text!)){
            boardXFinalSize = NumberFormatter().number(from: sender.text!)!.intValue
            MessageTextField.text = "Current Board size -> X: \(boardXFinalSize) Y: \(boardYFinalSize)"
        }
    }
    
    
    @IBAction func YTextFieldChanged(_ sender: UITextField) {
        if (validateBoardSizeFrom(string: sender.text!)){
            boardYFinalSize = NumberFormatter().number(from: sender.text!)!.intValue
            MessageTextField.text = "Current Board size -> X: \(boardXFinalSize) Y: \(boardYFinalSize)"
        }
    }
    
    @IBAction func ColorStuff(_ sender: UISwitch) {
        disableSwitches(exclude: sender)
        switch sender.tag {
        case 1:
            print("Light Mode")
            Theme.current = ThemeLight()
            changeTheme()
        case 2:
            print("Dark Mode")
            Theme.current = ThemeDark()
            changeTheme()
        case 3:
            print("Purple Mode")
            Theme.current = ThemePurple()
            changeTheme()
        case 4:
            print("Pink-Blue Mode")
            Theme.current = ThemePinkBlue()
            changeTheme()
        default:
            break
        }
    }
    
    func changeTheme(){
        self.view.backgroundColor = Theme.current.background
        UIButton.appearance().setTitleColor(Theme.current.buttonTextColor, for: .normal)
    }
    
    func disableSwitches(exclude senderSwitch: UISwitch){
        for oneSwitch in Switches {
            if oneSwitch != senderSwitch {
                oneSwitch.isOn = false
            }
        }
    }
    
    func validateBoardSizeFrom(string sizeString: String) -> Bool{
        if let myNumber = NumberFormatter().number(from: sizeString){
            let myInt = myNumber.intValue
            if (myInt < 3 || myInt > 10) {
                MessageTextField.text = "Board size must be in range of 3 to 10!"
                return false
            } else {
                MessageTextField.text = "Current Board size -> X: \(boardXFinalSize) Y: \(boardYFinalSize)"
                return true
            }
        }
        MessageTextField.text = "Board size must be an integer"
        return false
    }
    
    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        self.view.endEditing(true)
    }
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        YTextField.resignFirstResponder()
        XTextField.resignFirstResponder()
        return true
    }
    
    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
    }
    */

}
