using System.Collections.Generic;
using Domain;

namespace MenuSystem
{
    public static class ApplicationMenu
    {
        public static readonly Menu GameMenu = new Menu()
        {
            Title = "Game Menu",
            IsGameMenu = true,
            MenuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    IsDefaultChoice = true,
                    Description = "Start Game",
                    Shortcut = "A",
                },
                new MenuItem()
                {
                    Description = "Saves",
                    Shortcut = "B"
                },
            }
        };
        
        public static readonly Menu SettingsMenu = new Menu()
        {
            Title = "Settings Menu",
            IsSettingMenu = true,
            MenuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Description = "Set default options",
                    Shortcut = "A",
                },
                new MenuItem()
                {
                    Description = "Board size",
                    Shortcut = "B"
                },
                new MenuItem()
                {
                    Description = "Can touch",
                    Shortcut = "C"
                },
                new MenuItem()
                {
                    Description = "Carrier amount",
                    Shortcut = "D"
                },
                new MenuItem()
                {
                    Description = "Battleship amount",
                    Shortcut = "E"
                },
                new MenuItem()
                {
                    Description = "Submarine amount",
                    Shortcut = "F"
                },
                new MenuItem()
                {
                    Description = "Cruiser amount",
                    Shortcut = "G"
                },
                new MenuItem()
                {
                    Description = "Patrol amount",
                    Shortcut = "H"
                },
            }
        };
        
        public static readonly Menu MainMenu = new Menu()
        {
            Title = "Main Menu",
            IsMainMenu = true,
            MenuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    IsDefaultChoice = true,
                    Description = "Game",
                    Shortcut = "A",
                    CommandToExecute = GameMenu.RunMenu
                }, 
                new MenuItem()
                {
                    Description = "Settings",
                    Shortcut = "B",
                    CommandToExecute = SettingsMenu.RunMenu
                }
            } // MenuItems
        }; // MainMenu
        
    }
}