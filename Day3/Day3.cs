namespace AdventOfCode2023;

class Schematic
{
    public List<char[]> board = new List<char[]>();

    public Schematic(string input)
    {
        var split = input.Split(Environment.NewLine);
        foreach (var line in split)
        {
            this.board.Add(line.ToCharArray());
        }
    }

    public List<(int, int)> GetAdjacentIndexes(int i, int j)
    {
        var indexes = new List<(int i, int j)> {
            (i-1, j),
            (i+1, j),
            (i, j-1),
            (i, j+1),
            (i-1, j-1),
            (i+1, j-1),
            (i-1, j+1),
            (i+1, j+1)
        };
        // Filter out out-of-bounds indexes
        return indexes.Where(idx =>
        {
            return (idx.i >= 0) &&
                (idx.j >= 0) &&
                (idx.i < this.board[0].Length) &&
                (idx.j < this.board[0].Length);
        }
        ).ToList();
    }

    public int ExtractNumberAt(int i, int j)
    {
        if (!Char.IsDigit(this.board[i][j]))
        {
            return 0;
        }

        // Have to seek in both directions to find a complete number based on an index
        int leftIdx = j;
        int rightIdx = j;

        // Move left
        while ((leftIdx > 0) && (Char.IsDigit(this.board[i][leftIdx - 1])))
        {
            leftIdx -= 1;
        }
        // Move right
        while ((rightIdx < this.board[0].Length - 1) && (Char.IsDigit(this.board[i][rightIdx + 1])))
        {
            rightIdx += 1;
        }
        var numberString = new String(this.board[i], leftIdx, (rightIdx - leftIdx) + 1);
        return Int32.Parse(numberString);
    }

    public List<int> GetUniqueNumbersAt(int i, int j)
    {
        var numbers = new List<int>();
        var adjIdxs = this.GetAdjacentIndexes(i, j);
        var uniqNums = new HashSet<int>();
        foreach (var idx in adjIdxs)
        {
            var number = this.ExtractNumberAt(idx.Item1, idx.Item2);
            uniqNums.Add(number);
        }
        foreach (var uniqNum in uniqNums)
        {
            numbers.Add(uniqNum);
        }
        // Need to filter out zeros so that there isn't an extra number being used when considering gears,
        // would be better if they didn't get added in the first place
        return numbers.Where(num => num != 0).ToList();
    }

    public int GetGearRatio(int i, int j)
    {
        // If not a special character, then consider gear ratio as zero
        if (this.board[i][j] != '*')
        {
            return 0;
        }

        var numbers = this.GetUniqueNumbersAt(i, j);
        // Considered a valid gear if it only has 2 unique adjacent numbers
        if (numbers.Count == 2)
        {
            var gearRatio = numbers[0] * numbers[1];
            return gearRatio;
        }

        return 0;
    }
}

class Day3
{
    public static string SolvePart1(string input)
    {

        var numbers = new List<int>();
        var schematic = new Schematic(input);
        for (int i = 0; i < schematic.board.Count; i++)
        {
            for (int j = 0; j < schematic.board[0].Length; j++)
            {
                var curVal = schematic.board[i][j];
                if ((curVal != '.') && (!Char.IsDigit(curVal)))
                {
                    var uniqueNums = schematic.GetUniqueNumbersAt(i, j);
                    foreach (var num in uniqueNums)
                    {
                        numbers.Add(num);
                    }
                }
            }
        }
        return numbers.Sum().ToString();
    }

    public static string SolvePart2(string input)
    {
        var gearRatios = new List<int>();
        var schematic = new Schematic(input);
        for (int i = 0; i < schematic.board.Count; i++)
        {
            for (int j = 0; j < schematic.board[0].Length; j++)
            {
                var gearRatio = schematic.GetGearRatio(i, j);
                gearRatios.Add(gearRatio);
            }
        }
        return gearRatios.Sum().ToString();
    }

    public static void Main()
    {
        var input = File.ReadAllText("./input.txt");
        var answerPart1 = Day3.SolvePart1(input);
        Console.WriteLine($"Day 3 part 1: {answerPart1}");
        var answerPart2 = Day3.SolvePart2(input);
        Console.WriteLine($"Day 3 part 2: {answerPart2}");
    }
}