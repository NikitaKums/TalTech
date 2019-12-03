using System;
using System.Collections.Generic;

namespace Domain
{
    public class GameInstance
    {
        public Dictionary<string, int> OptionsDictionary { get; set; }
        public DateTime Time { get; set; }
        public Player Player { get; set; }
        public Computer Computer { get; set; }

       
        public GameInstance(Player player, Computer computer)
        {
            Player = player;
            Computer = computer;
            Time = DateTime.Now;
            OptionsDictionary = new Dictionary<string, int>();
            foreach (var valueKeyPair in Options.OPTIONS)
            {
                OptionsDictionary.Add(valueKeyPair.Key, valueKeyPair.Value);
            }
        }
    }
}
