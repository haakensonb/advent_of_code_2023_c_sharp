using System.Text.RegularExpressions;

namespace AdventOfCode2023;

class Day5
{
    public static bool IsInRange(long num, List<long> line)
    {
        var src = line[1];
        var len = line[2];
        return (num >= src) && (num < (src + len));
    }

    public static long CalcMapping(long num, List<long> line)
    {
        var src = line[1];
        var dest = line[0];
        var diff = Math.Abs(num - src);
        return dest + diff;
    }

    public static long ExecuteMap(long num, List<List<long>> map)
    {
        foreach (var line in map)
        {
            if (IsInRange(num, line))
            {
                return CalcMapping(num, line);
            }
        }
        return num;
    }

    public static string SolvePart1(string input)
    {
        var split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var seeds = split[0].Split("seeds: ")[1].Split(" ").Select(num => Int64.Parse(num)).ToList();
        var pattern = @"map:\n(\d+\s\d+\s\d+\n?)*";
        MatchCollection matches = Regex.Matches(input, pattern);

        var maps = new List<List<List<long>>>();

        foreach (Match match in matches)
        {
            var rawMapVals = match.Value
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(line => line.Split(" ").Select(x => Int64.Parse(x)).ToList()).ToList();
            maps.Add(rawMapVals);
        }

        // Copy of original values where each value is mutated by a mapping
        var results = new List<long>(seeds);
        for (int i = 0; i < results.Count; i++)
        {
            foreach (var map in maps)
            {
                var result = ExecuteMap(results[i], map);
                results[i] = result;
            }
        }

        return results.Min().ToString();
    }

    public static void Main()
    {
        var input = File.ReadAllText("./input.txt");
        var answerPart1 = Day5.SolvePart1(input);
        Console.WriteLine($"Day5 Part 1: {answerPart1}");
    }
}