namespace AdventOfCode2023;
using System;

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

    public int PowerValue(List<string> cubeKeys)
    {
        // Get the "power" value of a Game as defined by problem
        // Find max value of each color across entire game state (aka. fewest number of cubes needed for each color)

        var cubeToMaxVal = new Dictionary<string, int>();
        foreach (var cubeKey in cubeKeys)
        {
            cubeToMaxVal[cubeKey] = 1;
        }

        foreach (var key in cubeKeys)
        {
            foreach (var colorTotal in this.colorTotals)
            {
                if (colorTotal.ContainsKey(key))
                {
                    cubeToMaxVal[key] = Math.Max(cubeToMaxVal[key], colorTotal[key]);
                }

            }
        }

        int power = 1;
        foreach (var cube in cubeToMaxVal)
        {
            power *= cube.Value;
        }
        return power;
    }
}

class Day2
{
    private static Dictionary<string, int> maxPossibleColorTotals = new Dictionary<string, int>{
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };

    private static List<string> cubeKeys = new List<string> { "red", "blue", "green" };

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

    public static string SolvePart2(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var gamePowers = lines.Select(line =>
        {
            var gameState = new GameState(line);
            return gameState.PowerValue(cubeKeys);
        });
        return gamePowers.Sum().ToString();
    }

    public static void Main()
    {
        var input = File.ReadAllText("./input.txt");
        var answerPart1 = Day2.SolvePart1(input);
        Console.WriteLine($"Day 2 part 1: {answerPart1}");
        var answerPart2 = Day2.SolvePart2(input);
        Console.WriteLine($"Day 2 part 2: {answerPart2}");
    }
}