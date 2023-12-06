using AdventOfCode2023;

public class Day3UnitTest
{
    private static readonly string input = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

    [Fact]
    public void TestSolvePart1()
    {
        var answer = Day3.SolvePart1(input);
        Assert.Equal("4361", answer);
    }

    [Fact]
    public void TestSolvePart2()
    {
        var answer = Day3.SolvePart2(input);
        Assert.Equal("467835", answer);
    }

    [Fact]
    public void TestExtractNumberAt()
    {
        var schematic = new Schematic(input);
        var number = schematic.ExtractNumberAt(0, 2);
        Assert.Equal(467, number);
    }

    [Fact]
    public void TestGetAdjacentIndexes()
    {
        var schematic = new Schematic(input);
        var i = 0;
        var j = 0;
        var adjIdxs = schematic.GetAdjacentIndexes(i, j);
        var expected = new List<(int, int)> {
            (0, 1),
            (1, 0),
            (1, 1)
        };
        expected.Sort();
        adjIdxs.Sort();
        Assert.Equal(expected, adjIdxs);

        i = 9;
        j = 9;
        adjIdxs = schematic.GetAdjacentIndexes(i, j);
        expected = new List<(int, int)> {
            (8, 8),
            (8, 9),
            (9, 8)
        };
        expected.Sort();
        adjIdxs.Sort();
        Assert.Equal(expected, adjIdxs);
    }
}