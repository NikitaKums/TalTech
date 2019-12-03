using System;

namespace Helper
{
    public class HelperMethods
    {
        public static void WaitForUser()
        {
            Console.Write("\nPress any key to continue!");
            Console.ReadKey();
        }

        /*public static bool WaitForUserChoice()
        {
            Console.WriteLine("Press Q to quit or any other key to continue;");
            var choice = Console.ReadLine();
            return choice != null && choice.ToLower().Equals("q");
        }*/
    }
}