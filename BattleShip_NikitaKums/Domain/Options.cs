using System;
using System.Collections.Generic;

namespace Domain
{
    public static class Options
    {
        /*public static bool ChangeOption(int optionAtIndex, int newOptionValue)
        {
            var optionKey = OPTIONS.ElementAt(optionAtIndex).Key;
            if (optionKey == "Board size" && newOptionValue < 1)
            {
                Console.WriteLine("Board size cannot be smaller than 1. No option was changed.");
                return false;
            }
            OPTIONS[optionKey] = newOptionValue;
            return true;
        }*/
        public static bool ChangeOption(string option, int newOptionValue)
        {
            if (option.Contains("amount") && (newOptionValue > 20 || newOptionValue < 0))
            {
                //Console.WriteLine("More than 20 or negative number of ships is not allowed. No options were changed.");
                return false;
            }
            switch (option)
            {
                case "Board size" when newOptionValue < 5:
                    //Console.WriteLine("Not allowing board with size less than 10. No option were changed.");
                    return false;
                case "Board size" when newOptionValue > 100:
                    //Console.WriteLine("Biggest supported size is 100. No options were changed.");
                    return false;
                case "Can touch" when newOptionValue != 0 && newOptionValue != 1:
                    return false;
                default:
                    OPTIONS[option] = newOptionValue;
                    return true;
            }
        }

        public static void SetDefaultOptions()
        {
            OPTIONS["Board size"] = 10;
            OPTIONS["Can touch"] = 0;
            OPTIONS["Carrier amount"] = 1;
            OPTIONS["Battleship amount"] = 1;
            OPTIONS["Submarine amount"] = 1;
            OPTIONS["Cruiser amount"] = 1;
            OPTIONS["Patrol amount"] = 1;
        }
        
        public static Dictionary<string, int> OPTIONS = new Dictionary<string, int>()
        {
            {"Board size", 10}, //limit to a square for now
            {"Can touch", 0}, //0 = can not touch | 1 = can touch
            {"Carrier amount", 1},
            {"Battleship amount", 1},
            {"Submarine amount", 1},
            {"Cruiser amount", 1},
            {"Patrol amount", 1},
            //{"Board width", 10},
            //{"Board height", 10}
        };
    }
}
