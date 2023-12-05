namespace AdventOfCode2023;

public record GameState
{
    public int id;

    public List<Dictionary<string, int>> colorTotals = new List<Dictionary<string, int>>();

    public GameState(string line)
    {
        var split = line.Split(": ");
        this.id = Convert.ToInt32(split[0].Split(" ")[1]);
        var bagValues = split[1];
        string[] bags = bagValues.Split(";");
        foreach (var bag in bags)
        {
            var items = bag.Trim().Split(", ");
            var colorTotal = new Dictionary<string, int>();
            foreach (var item in items)
            {
                var pair = item.Split(" ");
                var key = pair[1];
                var val = Convert.ToInt32(pair[0]);
                colorTotal[key] = val;
            }
            this.colorTotals.Add(colorTotal);
        }
    }

    public bool IsPossible(Dictionary<string, int> maxPossibleColorTotals)
    {
        foreach (var colorTotal in this.colorTotals)
        {
            foreach (var maxColor in maxPossibleColorTotals)
            {
                if (colorTotal.ContainsKey(maxColor.Key))
                {
                    if (colorTotal[maxColor.Key] > maxColor.Value)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}

class Day2
{
    private static Dictionary<string, int> maxPossibleColorTotals = new Dictionary<string, int>{
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };
    public static string SolvePart1(string input)
    {

        var lines = input.Split(Environment.NewLine);
        var validGameIds = lines.Select(line =>
        {
            var gameState = new GameState(line);
            if (gameState.IsPossible(maxPossibleColorTotals))
            {
                return gameState.id;
            }
            else
            {
                return 0;
            }
        });
        return validGameIds.Sum().ToString();
    }

    public static void Main()
    {
        // var line = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";
        // var line = "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red";
        // var gameState = new GameState(line);
        // var maxPossibleColorTotals = new Dictionary<string, int>{
        //     {"red", 12},
        //     {"green", 13},
        //     {"blue", 14}
        // };
        // Console.WriteLine(gameState.IsPossible(maxPossibleColorTotals));
        var input = File.ReadAllText("./input.txt");
        var answerPart1 = Day2.SolvePart1(input);
        Console.WriteLine($"Day 2 part 1: {answerPart1}");
    }
}