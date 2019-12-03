//
//  ViewController.swift
//  15PuzzleiOS
//
//  Created by user153805 on 4/14/19.
//  Copyright © 2019 nikita. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    @IBOutlet weak var timeLabel: UILabel!
    
    @IBOutlet weak var resetButton: UIButton!
    
    @IBOutlet weak var startButton: UIButton!
    
    var timer: Timer?
    var timeLeft = 60
    
    var gameLogic: PuzzleLogic = PuzzleLogic()
    
    @IBOutlet var Buttons: [UIButton]!
    var TransferButtons: [TransferButton] = []
    
    @IBAction func GameButtonTouchUpInside(_ sender: UIButton) {
        let clickedTransferButton = getTransferButton(tag: sender.tag)
        if (gameLogic.parseMove(whenClicked: clickedTransferButton!, in: TransferButtons)){
            if (gameLogic.isSolved(TransferButtons)){
                print("solved")
                disableOrEnable(buttons: Buttons, enabled: false)
                stopTimer()
            }
            syncUIButtons()
        }
    }
    
    @IBAction func startButtonTouchUpInside(_ sender: UIButton) {
        print("start")
        syncUIButtons()
        disableOrEnable(buttons: Buttons, enabled: true)
        disableStartButton()
        enableResetButton()
        startTimer()
    }

    @IBAction func ResetButtonTouchUpInside(_ sender: UIButton) {
        print("reset")
        syncTransferButtons()
        gameLogic.shuffleGameButtons(TransferButtons)
        syncUIButtons()
        enableStartButton()
        disableResetButton()
        stopTimer()
        disableOrEnable(buttons: Buttons, enabled: false)
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        syncTransferButtons()
        gameLogic.shuffleGameButtons(TransferButtons)
        disableOrEnable(buttons: Buttons, enabled: false)
        for button in Buttons {
            button.setTitleColor(#colorLiteral(red: 0.501960814, green: 0.501960814, blue: 0.501960814, alpha: 1), for: .disabled)
        }
        startButton.setTitleColor(#colorLiteral(red: 0.501960814, green: 0.501960814, blue: 0.501960814, alpha: 1), for: .disabled)
        resetButton.setTitleColor(#colorLiteral(red: 0.501960814, green: 0.501960814, blue: 0.501960814, alpha: 1), for: .disabled)
        timeLabel.textColor = #colorLiteral(red: 1, green: 0.8773085456, blue: 0.5434256001, alpha: 1)
        disableResetButton()
    }
    
    override var preferredStatusBarStyle: UIStatusBarStyle {
        return .lightContent
    }
    
    func enableResetButton(){
        resetButton.isEnabled = true
    }
    func disableResetButton(){
        resetButton.isEnabled = false
    }
    func disableStartButton(){
        startButton.isEnabled = false
    }
    func enableStartButton(){
        startButton.isEnabled = true
    }
    
    func syncTransferButtons(){
        for button in Buttons {
            TransferButtons.append(TransferButton(title: button.title(for: .normal)!, tag: button.tag))
        }
    }
    
    func syncUIButtons(){
        for button in Buttons {
            let transferButtonForCurrentButton = getTransferButton(tag: button.tag)
            button.setTitle(transferButtonForCurrentButton?.title, for: .normal)
        }
    }
    
    func getTransferButton(tag: Int) -> TransferButton?{
        for transferButton in TransferButtons{
            if (transferButton.tag == tag){
                return transferButton
            }
        }
        return nil
    }
    
    func disableOrEnable(buttons: [UIButton], enabled: Bool){
        for button in buttons {
            button.isEnabled = enabled
        }
    }
    
    func startTimer(){
        timeLeft = 60
        timer = Timer.scheduledTimer(timeInterval: 1.0, target: self, selector: #selector(onTimerFires), userInfo: nil, repeats: true)
    }
    
    func stopTimer(){
        timeLeft = 0
    }
    
    @objc func onTimerFires()
    {
        timeLeft -= 1
        if (timeLeft >= 0){
            timeLabel.text = "\(timeLeft) seconds left"
        }
        
        if timeLeft <= 0 {
            timer!.invalidate()
            timer = nil
        }
    }
}

