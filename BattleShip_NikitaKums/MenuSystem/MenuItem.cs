using System;

namespace MenuSystem
{
    public class MenuItem
    {
        public string Shortcut { get; set; }
        public string Description { get; set; }

        public Func<string> CommandToExecute { get; set; }

        public bool IsDefaultChoice { get; set; } = false;
        
        public override string ToString()
        {
            return Shortcut + ") " + Description;
        }

    }
}