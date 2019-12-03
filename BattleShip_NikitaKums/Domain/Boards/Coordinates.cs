using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Boards
{
    public class Coordinates
    {
        public int XCord;
        public string YCord;
        private string AlphabetLetters = "abcdefghijklmnopqrstuvwxyz";

        public Coordinates(int verticalX, string horizontalY)
        {
            XCord = verticalX;
            YCord = horizontalY.ToLower();
        }

        public int GetX()
        {
            return XCord;
        }

        // Returns the index of the given letter e.g. a = 0, b = 1 ...
        public int GetY()
        {
            var alphabet = AlphabetLetters.ToList();
            if (YCord.Length == 1)
            {
                return alphabet.FindIndex(letter => letter.ToString() == YCord);
            }
            var helper = Regex.Split(YCord, "[z]");
            helper = helper.Skip(1).ToArray();
            
            return 25 + (Convert.ToInt32(helper[0]) - 10);
        }

        public static bool ValidateCoordinates(string x, string y)
        {
            try
            {
                var xCordFlag = false;
                var yCordFlag = false;
                var rangeMax = Options.OPTIONS["Board size"];
                if (y.ToLower().Equals("z")) return false;
                if (!int.TryParse(x, out var newX) || x.Trim().Length == 0)
                {
                    return false;
                }

                if (int.TryParse(y, out _) || y.Trim().Length == 0)
                {
                    return false;
                }
            
                if (Enumerable.Range(0, rangeMax).Contains(newX))
                {
                    xCordFlag = true;
                }
            
                var helper = Regex.Split(y, "([a-z])");
                helper = helper.Skip(1).ToArray();
            
                if (helper[0].ToLower() == "z" && Enumerable.Range(10, rangeMax - 26 + 1).Contains(Convert.ToInt32(helper[1])))
                {
                    yCordFlag = true;
                }
            
                else if (y.Length == 1 && Enumerable.Range('a', rangeMax).Contains(Convert.ToChar(y)))
                {
                    yCordFlag = true;
                }

                return xCordFlag && yCordFlag;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string IntToYCoordinate(int coordinate)
        {
            var sb = new StringBuilder();
            if (coordinate < 25)
            {
                sb.Append(char.ConvertFromUtf32(65 + coordinate).ToLower());
                return sb.ToString();
            }

            sb.Append("z");
            sb.Append(coordinate - 15);

            return sb.ToString();
        }
    }
}