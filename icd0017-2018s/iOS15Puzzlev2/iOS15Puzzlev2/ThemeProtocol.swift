//
//  ThemeProtocol.swift
//  iOS15Puzzlev2
//
//  Created by user154645 on 5/5/19.
//  Copyright © 2019 user154645. All rights reserved.
//

import UIKit

protocol ThemeProtocol {
    var buttonColor: UIColor { get }
    var background: UIColor { get }
    var disabledTitleColor: UIColor { get }
    var titleColor: UIColor { get }
    var buttonTextColor: UIColor { get }
    var id: Int { get }
}
