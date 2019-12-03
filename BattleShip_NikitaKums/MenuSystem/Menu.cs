using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using GameSystem;
using Helper;

namespace MenuSystem
{
    public class Menu
    {
        public string Title { get; set; }
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        private MenuItem GoBackItem { get; set; } = new MenuItem()
        {
            Shortcut = "X",
            Description = "Go back!"
        };
        
        private MenuItem QuitToMainItem { get; set; } = new MenuItem()
        {
            Shortcut = "Q",
            Description = "Quit to main menu!"
        };

        public bool DisplayQuitToMainMenu { get; set; } = false;
        public bool IsMainMenu { get; set; } = false;
        public bool IsGameMenu { get; set; } = false;
        public bool IsSettingMenu { get; set; } = false;

        private void PrintMenu()
        {
            var defaultMenuChoice = MenuItems.FirstOrDefault(m => m.IsDefaultChoice == true);

            Console.Clear();
            Console.WriteLine($"=========={Title}==========");
            var titleCharCount = Title.Length;
            var sb = new StringBuilder();
            sb.Insert(0, "=", titleCharCount + 20);
            
            foreach (var menuItem in MenuItems)
            {
                if (menuItem.IsDefaultChoice)
                {
                    Console.ForegroundColor =
                        ConsoleColor.Red;
                    Console.WriteLine(menuItem);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(menuItem);
                }
            }
            Console.WriteLine(sb.ToString());
            
            Console.WriteLine(GoBackItem);
            
            if (DisplayQuitToMainMenu)
            {
                Console.WriteLine(QuitToMainItem);
            }

            Console.Write(
                defaultMenuChoice == null ? ">" : "[" + defaultMenuChoice.Shortcut + "]>"
            );
        }
        
        public string RunMenu()
        {
            var done = true;
            string input;
            do
            {
                done = false;

                PrintMenu();
                input = Console.ReadLine().ToUpper().Trim();

                // shall we exit from this menu
                if (input == GoBackItem.Shortcut)
                {
                    break; // jump out of the loop
                }
                if (DisplayQuitToMainMenu && input == QuitToMainItem.Shortcut)
                {
                    break; // jump out of the loop
                }

                MenuItem item = null;
                item = string.IsNullOrWhiteSpace(input)
                    ? MenuItems.FirstOrDefault(m => m.IsDefaultChoice == true)
                    : MenuItems.FirstOrDefault(m => m.Shortcut == input);

                if (item == null)
                {
                    Console.WriteLine("Not found in the list of commands!");
                    HelperMethods.WaitForUser();
                    continue; // jump back to the start of loop
                }

                if (IsSettingMenu)
                {
                    if (item.Shortcut.Equals("A"))
                    {
                        Options.SetDefaultOptions();
                    }
                    else
                    {
                        OptionChangingFromMenu(item.Description);
                    }
                    Console.WriteLine("Option changed");
                    HelperMethods.WaitForUser();
                    continue;
                }
                
                if (IsGameMenu)
                {
                    if (item.Shortcut.Equals("A"))
                    {
                        Game.FullGame();
                        continue;
                    }
                    if (item.Shortcut.Equals("B"))
                    {
                        Game.SelectGameAndStart();
                        continue;
                    }
                }
                
                // execute the command specified in the menu item
                if (item.CommandToExecute == null)
                {
                    Console.WriteLine(input + " has no command assigned to it!");
                    HelperMethods.WaitForUser();
                    continue; // jump back to the start of loop
                }

                // everything should be ok now, lets run it!
                var chosenCommand = item.CommandToExecute();
                input = chosenCommand;

                if (IsMainMenu == false && chosenCommand == QuitToMainItem.Shortcut)
                {
                    break;
                }

                if (chosenCommand != GoBackItem.Shortcut && chosenCommand != QuitToMainItem.Shortcut) HelperMethods.WaitForUser();;
            } while (done != true);


            return input;
        }
        

        private static void OptionChangingFromMenu(string option)
        {
            Console.Clear();
            Console.WriteLine("Max ship amount is 20");
            Console.WriteLine("Board size at least 5 and not bigger than 100");
            Console.WriteLine("0 - ships cannot touch, 1 - ships can touch\n");
            Console.WriteLine("----------------------------------------------\n");
            while (true)
            {
                Console.WriteLine($"Changing option: {option} | Current value: {Options.OPTIONS[option]}");
                Console.WriteLine("Enter new value: ");
                var value = Console.ReadLine();
                if (!int.TryParse(value, out var newValue))
                {
                    Console.WriteLine("Try again.\n");
                    continue;
                }
                
                if (!Options.ChangeOption(option, newValue))
                {
                    Console.WriteLine("Try again.\n");
                    continue;
                }
                break;
            }
        }
        
    }
}
