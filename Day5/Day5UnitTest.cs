
using AdventOfCode2023;

public class Day5UnitTest
{

    private readonly string input = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

    [Fact]
    public void TestSolvePart1()
    {
        var answer = Day5.SolvePart1(this.input);
        Assert.Equal("35", answer);
    }

    [Fact]
    public void TestIsInRangeTrue()
    {
        var line = new List<long> { 50, 98, 2 };
        var result = Day5.IsInRange(99, line);
        Assert.True(result);
    }

    [Fact]
    public void TestIsInRangeFalse()
    {
        var line = new List<long> { 50, 98, 2 };
        var result = Day5.IsInRange(4, line);
        Assert.False(result);
    }

    [Fact]
    public void TestIsInRangeFalse2()
    {
        var line = new List<long> { 37, 52, 2 };
        var result = Day5.IsInRange(81, line);
        Assert.False(result);
    }

    [Fact]
    public void TestCalcMapping1()
    {
        var line = new List<long> { 50, 98, 2 };
        var result = Day5.CalcMapping(99, line);
        Assert.Equal(51, result);
    }

    [Fact]
    public void TestCalcMapping2()
    {
        var line = new List<long> { 52, 50, 48 };
        var result = Day5.CalcMapping(50, line);
        Assert.Equal(52, result);
    }
}