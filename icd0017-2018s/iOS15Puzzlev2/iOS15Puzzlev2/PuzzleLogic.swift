//
//  PuzzleLogic.swift
//  iOS15Puzzlev2
//
//  Created by user154645 on 5/5/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import Foundation

class PuzzleLogic {
    var boardXAxis : Int
    var boardYAxis : Int
    
    var buttonsDictionary = [String: String]()
    
    
    init(boardXAxis: Int, boardYAxis: Int) {
        self.boardXAxis = boardXAxis
        self.boardYAxis = boardYAxis
    }
    
    func setButtonText(for buttons: [TransferButton]){
        var XPart: Int = 1
        var YPart: Int = 1
        var Index: Int = 1
        for button in buttons {
            if (XPart*YPart == boardXAxis*boardYAxis){
                button.title = " "
                button.accessibilityidentifier = "\(XPart)-\(YPart)-Empty"
                buttonsDictionary["\(XPart)-\(YPart)"] = "Empty"

            } else {
                if (YPart > boardYAxis){
                    YPart = 1
                    XPart = XPart + 1
                }
                button.title = "\(Index)"
                button.accessibilityidentifier = "\(XPart)-\(YPart)-\(Index)"
                buttonsDictionary["\(XPart)-\(YPart)"] = "\(Index)"
                YPart = YPart + 1
                Index = Index + 1
            }
        }
    }
    
    func findEmptyButton(in buttons: [TransferButton]) -> TransferButton? {
        for button in buttons {
            if (button.title == " "){
                return button
            }
        }
        return nil
    }
    
    func getButtonCoords(for button: TransferButton) -> [String]{
        let result = button.accessibilityidentifier.components(separatedBy: "-")
        return result
    }
    
    func findButtonByIdentifier(in buttons: [TransferButton], withIdentifier identifier: String) -> TransferButton? {
        for button in buttons {
            if button.accessibilityidentifier == identifier {
                return button
            }
        }
        return nil
    }
    
    func parseMove(whenClicked button: TransferButton, in buttons: [TransferButton]) -> Bool {
        var emptyButtonCoords: [String]
        var clickedButtonCoords: [String]
        var buttonByIdentifier: TransferButton
        
        emptyButtonCoords = getButtonCoords(for: findEmptyButton(in: buttons)!)
        let emptyButtonXCoord = Int(emptyButtonCoords[0])
        let emptyButtonYCoord = Int(emptyButtonCoords[1])
        
        clickedButtonCoords = getButtonCoords(for: button)
        let clickedButtonXCoord = Int(clickedButtonCoords[0])
        let clickedButtonYCoord = Int(clickedButtonCoords[1])
        
        var row = abs(emptyButtonXCoord! - clickedButtonXCoord!)
        var column = abs(emptyButtonYCoord! - clickedButtonYCoord!)
        
        if (row != 0 && column != 0){
            return false
        }
        if (clickedButtonXCoord == emptyButtonXCoord && clickedButtonYCoord == emptyButtonYCoord){
            return false
        }
        
        var clickedButtonText = button.title
        var temp = 1
        
        if (column == 0){
            while (row != 0){
                if (emptyButtonXCoord! > clickedButtonXCoord!){
                    buttonByIdentifier = findButtonByIdentifier(in: buttons, withIdentifier: createIdentifier(x: Int(getButtonCoords(for: button)[0])! + temp, y: Int(getButtonCoords(for: button)[1])!))!
                } else {
                    buttonByIdentifier = findButtonByIdentifier(in: buttons, withIdentifier: createIdentifier(x: Int(getButtonCoords(for: button)[0])! - temp, y: Int(getButtonCoords(for: button)[1])!))!
                }
                
                let replacingButtonText = buttonByIdentifier.title
                buttonByIdentifier.title = clickedButtonText
                clickedButtonText = replacingButtonText
                
                temp = temp + 1
                row = row - 1
            }
            button.title = " "
            return true
        }
        
        
        if (row == 0){
            while (column != 0){
                
                if (emptyButtonYCoord! > clickedButtonYCoord!){
                    buttonByIdentifier = findButtonByIdentifier(in: buttons, withIdentifier: createIdentifier(
                        x: Int(getButtonCoords(for: button)[0])!,
                        y: Int(getButtonCoords(for: button)[1])! + temp))!
                    
                } else {
                    buttonByIdentifier = findButtonByIdentifier(in: buttons, withIdentifier: createIdentifier(
                        x: Int(getButtonCoords(for: button)[0])!,
                        y: Int(getButtonCoords(for: button)[1])! - temp))!
                }
                
                let replacingButtonText = buttonByIdentifier.title
                buttonByIdentifier.title = clickedButtonText
                clickedButtonText = replacingButtonText
                
                temp = temp + 1
                column = column - 1
            }
            button.title = " "
            return true
        }
        
        return false
        
    }
    
    func isSolved(_ buttons: [TransferButton]) -> Bool{
        var temp = 1
        if (findEmptyButton(in: buttons)?.accessibilityidentifier != "\(boardXAxis)-\(boardYAxis)-Empty"){
            return false
        }
        
        for button in buttons {
            if (temp == boardXAxis * boardYAxis){
                return true
            }
            if (button.title != getButtonCoords(for: button)[2]){
                return false
            }
            temp = temp + 1
        }
        return false
    }
    
    
    func shuffleGameButtons(_ buttons: [TransferButton]){
        var emptyButtonX: Int
        var emptyButtonY: Int
        var newIdentifier: String
        var emptyButton: TransferButton
        
        for _ in 1...100 {
            emptyButton = findEmptyButton(in: buttons)!
            emptyButtonX = Int(getButtonCoords(for: emptyButton)[0])!
            emptyButtonY = Int(getButtonCoords(for: emptyButton)[1])!
            
            let randomInt = randomNumber(from: 0, till: 1)
            
            if randomInt == 0 {
                newIdentifier = createIdentifier(x: randomNumber(from: 1, till: boardXAxis), y: emptyButtonY)
            } else {
                newIdentifier = createIdentifier(x: emptyButtonX, y: randomNumber(from: 1, till: boardYAxis))
            }
            if (newIdentifier != "false") {
                _ = parseMove(whenClicked: findButtonByIdentifier(in: buttons, withIdentifier: newIdentifier)!, in: buttons)
            }
        }
        
    }
    
    func randomNumber(from start: Int, till end: Int) -> Int {
        return Int.random(in: start ... end)
    }
    
    func createIdentifier(x: Int, y: Int) -> String {
        let searchString = "\(x)-\(y)"
        if let index = buttonsDictionary[searchString] {
            return "\(x)-\(y)-" + index
        }
        return "false"
    }
}

