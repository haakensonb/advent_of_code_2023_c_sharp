using System.Text;

namespace AdventOfCode2023
{

    class Day1
    {

        private readonly static Dictionary<string, string> wordToDigitMap = new Dictionary<string, string>{
            {"one", "1"},
            {"two", "2"},
            {"three", "3"},
            {"four", "4"},
            {"five", "5"},
            {"six", "6"},
            {"seven", "7"},
            {"eight", "8"},
            {"nine", "9"}
        };

        private static string[] MapWordsToDigits(string[] lines)
        {
            return lines.Select(line =>
            {
                var sb = new StringBuilder();
                // TODO: make more efficient
                foreach (char c in line)
                {
                    sb.Append(c);
                    foreach (var item in wordToDigitMap)
                    {
                        if (sb.ToString().Contains(item.Key))
                        {
                            sb.Replace(item.Key, item.Value);
                            // Need to add back in last character since replacements can overlap by 1 char
                            sb.Append(c);
                        }
                    }
                }
                return sb.ToString();
            }).ToArray();
        }

        private static List<string> FilterToDigitsOnly(string[] lines)
        {
            List<string> digitsOnly = lines.Select(line =>
            {
                return new String(line.Where(c => Char.IsDigit(c)).ToArray());
            }).ToList();
            return digitsOnly;
        }

        private static int CalibrationValuesSum(List<string> digitsOnly)
        {
            List<int> selectedDigits = new List<int>();
            foreach (string line in digitsOnly)
            {
                var firstDigit = line[0].ToString();
                var lastDigit = line[line.Length - 1].ToString();
                var combined = $"{firstDigit}{lastDigit}";
                selectedDigits.Add(Convert.ToInt32(combined));
            }
            return selectedDigits.Sum();
        }

        public static string SolvePart1(string input)
        {
            var inputLines = input.Split(Environment.NewLine);
            var digitsOnly = Day1.FilterToDigitsOnly(inputLines);
            return Day1.CalibrationValuesSum(digitsOnly).ToString();
        }

        public static string SolvePart2(string input)
        {
            var inputLines = input.Split(Environment.NewLine);
            var wordsToDigits = Day1.MapWordsToDigits(inputLines);
            var digitsOnly = Day1.FilterToDigitsOnly(wordsToDigits);
            return Day1.CalibrationValuesSum(digitsOnly).ToString();
        }

        public static void Main()
        {
            var input = File.ReadAllText("./input.txt");
            var answerPart1 = Day1.SolvePart1(input);
            Console.WriteLine($"Day1 part 1: {answerPart1}");
            var answerPart2 = Day1.SolvePart2(input);
            Console.WriteLine($"Day1 part 2: {answerPart2}");
        }
    }

}