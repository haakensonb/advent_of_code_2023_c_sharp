using AdventOfCode2023;

public class Day2UnitTest
{

    private static readonly string input1 = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

    [Fact]
    public void TestSolvePart1()
    {
        var answer = Day2.SolvePart1(input1);
        Assert.Equal("8", answer);
    }

    [Fact]
    public void TestSolvePart2()
    {
        var answer = Day2.SolvePart2(input1);
        Assert.Equal("2286", answer);
    }
}