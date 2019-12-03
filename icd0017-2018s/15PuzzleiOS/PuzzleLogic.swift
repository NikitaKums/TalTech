//
//  PuzzleLogic.swift
//  15PuzzleiOS
//
//  Created by user153805 on 4/14/19.
//  Copyright © 2019 nikita. All rights reserved.
//

import Foundation
import UIKit

class PuzzleLogic {
    func setButtonText(for buttons: [TransferButton]){
        var index: Int = 1
        for button in buttons {
            if (index == 16){
                button.title = " "
            } else {
                button.title = String(index)
                index = index + 1
            }
        }
    }
    
    func findEmptyButton(in buttons: [TransferButton]) -> TransferButton?{
        for button in buttons {
            if (button.title == " "){
                return button;
            }
        }
        return nil;
    }
    
    func getButtonCoords(for button: TransferButton) -> [String]{
        let result = String(button.tag).components(separatedBy: "9")
        return result
    }
    
    func findButtonByTag(in buttons: [TransferButton], withTag tag: Int) -> TransferButton?{
        for button in buttons {
            if (button.tag == tag){
                return button;
            }
        }
        return nil;
    }
    
    func parseMove(whenClicked button: TransferButton, in buttons: [TransferButton]) -> Bool{
        var emptyButtonCoords: [String]
        var clickedButtonCoords: [String]
        var buttonByTag: TransferButton
        
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
                    buttonByTag = findButtonByTag(in: buttons, withTag: createTag(x: Int(getButtonCoords(for: button)[0])! + temp, y: Int(getButtonCoords(for: button)[1])!)!)!
                } else {
                    buttonByTag = findButtonByTag(in: buttons, withTag: createTag(x: Int(getButtonCoords(for: button)[0])! - temp, y: Int(getButtonCoords(for: button)[1])!)!)!
                }
                
                let replacingButtonText = buttonByTag.title
                buttonByTag.title = clickedButtonText
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
                    buttonByTag = findButtonByTag(in: buttons, withTag: createTag(x: Int(getButtonCoords(for: button)[0])!, y: Int(getButtonCoords(for: button)[1])! + temp)!)!
                } else {
                    buttonByTag = findButtonByTag(in: buttons, withTag: createTag(x: Int(getButtonCoords(for: button)[0])!, y: Int(getButtonCoords(for: button)[1])! - temp)!)!
                }
                
                let replacingButtonText = buttonByTag.title
                buttonByTag.title = clickedButtonText
                clickedButtonText = replacingButtonText
                
                temp = temp + 1
                column = column - 1
            }
            button.title = " "
            return true
        }
    
        return false
    }
    
    func shuffleGameButtons(_ buttons: [TransferButton]){
        var emptyButtonX: Int
        var emptyButtonY: Int
        var newTag: Int
        var emptyButton: TransferButton
        for _ in 1...100 {
            emptyButton = findEmptyButton(in: buttons)!
            emptyButtonX = Int(getButtonCoords(for: emptyButton)[0])!
            emptyButtonY = Int(getButtonCoords(for: emptyButton)[1])!
            
            let randomInt = randomNumber(from: 0, till: 1)
            
            if (randomInt == 0){
                newTag = createTag(x: randomNumber(from: 1, till: 4), y: emptyButtonY)!
            } else {
                newTag = createTag(x: emptyButtonX, y: randomNumber(from: 1, till: 4))!
            }
            _ = parseMove(whenClicked: findButtonByTag(in: buttons, withTag: newTag)!, in: buttons)
        }
    }
    
    func randomNumber(from start: Int, till end: Int) -> Int {
        return Int.random(in: start ... end)
    }
    
    func createTag(x: Int, y: Int) -> Int? {
        return Int(String(x) + "9" + String(y))
    }
    
    func isSolved(_ buttons: [TransferButton]) -> Bool{
        var temp = 1
        if (findEmptyButton(in: buttons)?.tag != 494){
            return false
        }
        
        for button in buttons {
            print("\(button.title) \(button.tag)")
            if (temp == 16){
                return true
            }
            if (button.title != String(calculateButtonPosition(for: button))){
                return false
            }
            temp = temp + 1
        }
        return false
    }

    
    func calculateButtonPosition(for button: TransferButton) -> Int {
        let buttonYCoord = Int(getButtonCoords(for: button)[1])!
        let buttonXCoord = Int(getButtonCoords(for: button)[0])!
        if (buttonYCoord == 1){
            return buttonXCoord * 4 - 3
        }
        if (buttonYCoord == 2){
            return (buttonXCoord * buttonYCoord) + (buttonXCoord * buttonYCoord - 2)
        }
        if (buttonYCoord == 3){
            return (buttonXCoord * buttonYCoord) + (buttonXCoord - 1)
        }
        return buttonXCoord * buttonYCoord
    }
    
}
