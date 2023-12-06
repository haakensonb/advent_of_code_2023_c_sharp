namespace AdventOfCode2023;

public record Card
{
    public HashSet<int> winningNumbers;

    public HashSet<int> givenNumbers;

    public Card(string input)
    {
        var split = input.Split(" | ");
        this.winningNumbers = split[0]
            .Split(": ")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(num => Int32.Parse(num))
            .ToHashSet();
        this.givenNumbers = split[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(num => Int32.Parse(num))
            .ToHashSet();
    }

    public IEnumerable<int> GetIntersectingNumbers()
    {
        // Determine how many of the given numbers are winning numbers
        return this.winningNumbers.Intersect(this.givenNumbers);
    }

    public int CalcPoints()
    {
        var intersectNums = this.GetIntersectingNumbers();
        var n = intersectNums.Count();
        if (n > 0)
        {
            return (int)Math.Pow(2, n - 1);
        }
        return 0;
    }
}

class Day4
{
    public static string SolvePart1(string input)
    {
        var sum = 0;
        var lines = input.Split(Environment.NewLine);
        foreach (var line in lines)
        {
            var card = new Card(line);
            sum += card.CalcPoints();
        }
        return sum.ToString();
    }

    public static void Main()
    {
        var input = File.ReadAllText("./input.txt");
        var answerPart1 = Day4.SolvePart1(input);
        Console.WriteLine($"Day 4, part 1: {answerPart1}");
    }
}