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

        // Need to look up double pointer technique?
        // Idx boundaries/starts are messed up?

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
                    var adjIdxs = schematic.GetAdjacentIndexes(i, j);
                    var uniqNums = new HashSet<int>();
                    foreach (var idx in adjIdxs)
                    {
                        var number = schematic.ExtractNumberAt(idx.Item1, idx.Item2);
                        uniqNums.Add(number);
                    }
                    foreach (var uniqNum in uniqNums)
                    {
                        numbers.Add(uniqNum);
                    }
                }
            }
        }
        return numbers.Sum().ToString();
    }

    public static string SolvePart2(string input)
    {
        return "";
    }

    public static void Main()
    {
        var input = File.ReadAllText("./input.txt");
        var answerPart1 = Day3.SolvePart1(input);
        Console.WriteLine($"Day 3 part 1: {answerPart1}");
    }
}