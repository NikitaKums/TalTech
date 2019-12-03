//
//  GameViewController.swift
//  iOS15Puzzlev2
//
//  Created by user154645 on 5/5/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import UIKit

class GameViewController: UIViewController {

    var boardXSize: Int = 0
    var boardYSize: Int = 0
    
    @IBOutlet weak var timeLabel: UILabel!
    @IBOutlet weak var resetButton: UIButton!
    @IBOutlet weak var startButton: UIButton!
    
    var Buttons: [UIButton] = []
    var TransferButtons: [TransferButton] = []
    
    var isIPad: Bool = false
    
    var gameBoard = UIStackView()
    
    var timer: Timer?
    var timeLeft = 60
    
    var gameLogic: PuzzleLogic = PuzzleLogic(boardXAxis: boardXFinalSize, boardYAxis: boardYFinalSize)
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        checkForDevice()
        
        if let stackViewNeeded = self.view.viewWithTag(7777777) as? UIStackView {
            gameBoard = stackViewNeeded
            createBoard()
        }
        syncTransferButtons()
        gameLogic.setButtonText(for: TransferButtons)
        disableOrEnable(buttons: Buttons, enabled: false)
        for button in Buttons {
            button.setTitleColor(#colorLiteral(red: 0.501960814, green: 0.501960814, blue: 0.501960814, alpha: 1), for: .disabled)
        }
        startButton.setTitleColor(Theme.current.disabledTitleColor, for: .disabled)
        resetButton.setTitleColor(Theme.current.disabledTitleColor, for: .disabled)
        startButton.setTitleColor(Theme.current.titleColor, for: .normal)
        resetButton.setTitleColor(Theme.current.titleColor, for: .normal)
        scaleForIPad(button: startButton)
        scaleForIPad(button: resetButton)

        syncUIButtons()
        gameLogic.shuffleGameButtons(TransferButtons)
        syncUIButtons()
        disableResetButton()
        self.view.backgroundColor = Theme.current.background
    }
    
    func checkForDevice(){
        switch (UIScreen.main.traitCollection.horizontalSizeClass, UIScreen.main.traitCollection.verticalSizeClass){
            
        case (UIUserInterfaceSizeClass.regular, UIUserInterfaceSizeClass.regular):
            isIPad = true
        default:
            break
        }
    }
    
    func scaleForIPad(button: UIButton){
        if isIPad {
            button.titleLabel?.font = .systemFont(ofSize: 50)
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
    
    
    @objc func GameButtonTouchUpInside(_ sender: UIButton) {
        let clickedTransferButton = getTransferButton(identifier: sender.accessibilityIdentifier!)
        if (gameLogic.parseMove(whenClicked: clickedTransferButton!, in: TransferButtons)){
            if (gameLogic.isSolved(TransferButtons)){
                disableOrEnable(buttons: Buttons, enabled: false)
                stopTimer()
            }
            syncUIButtons()
        }
    }
    
    
    func createBoard(){
        var i = 0
        repeat {
            
            if gameBoard.arrangedSubviews.count == 0 {
                let columnStack = UIStackView()
                columnStack.axis = .vertical
                columnStack.alignment = .fill
                columnStack.distribution = .fillEqually
                columnStack.spacing = 8.0
                gameBoard.addArrangedSubview(columnStack)
            }
            
            for subView in gameBoard.arrangedSubviews {
                if let columnStack = subView as? UIStackView {
                    var j = 0
                    let rowStack = UIStackView()
                    repeat {
                        rowStack.axis = .horizontal
                        rowStack.alignment = .fill
                        rowStack.distribution = .fillEqually
                        rowStack.spacing = 8.0
                        let button = UIButton()
                        button.addTarget(self, action: #selector(GameButtonTouchUpInside), for: .touchUpInside)
                        button.backgroundColor = Theme.current.buttonColor
                        scaleForIPad(button: button)
                        rowStack.addArrangedSubview(button)
                        Buttons.append(button)
                        j = j + 1
                    } while j < boardYSize
                    columnStack.addArrangedSubview(rowStack)
                }
            }
            i = i + 1
        } while i < boardXSize
    }
    
    func disableOrEnable(buttons: [UIButton], enabled: Bool){
        for button in buttons {
            button.isEnabled = enabled
        }
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
            if button.title(for: .normal) != nil{
                TransferButtons.append(TransferButton(title: button.title(for: .normal)!, accessibilityidentifier: button.accessibilityIdentifier!))
            } else {
                TransferButtons.append(TransferButton(title: "", accessibilityidentifier: ""))
            }
        }
    }
    
    func syncUIButtons(){
        var i = 0
        for button in Buttons {
            button.setTitle(TransferButtons[i].title, for: .normal)
            button.accessibilityIdentifier = TransferButtons[i].accessibilityidentifier
            i = i + 1
        }
    }
    
    func getTransferButton(identifier: String) -> TransferButton?{
        for transferButton in TransferButtons{
            if (transferButton.accessibilityidentifier == identifier){
                return transferButton
            }
        }
        return nil
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
