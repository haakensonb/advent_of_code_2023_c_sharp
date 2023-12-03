using AdventOfCode2023;

public class Day1UnitTest
{

    readonly string input1 = @"1abc2
        pqr3stu8vwx
        a1b2c3d4e5f
        treb7uchet";

    readonly string input2 = @"two1nine
        eightwothree
        abcone2threexyz
        xtwone3four
        4nineeightseven2
        zoneight234
        7pqrstsixteen";

    readonly string input3 = @"eighthree
        sevenine";

    [Fact]
    public void TestSolvePart1()
    {
        string answer = Day1.SolvePart1(this.input1);
        Assert.Equal("142", answer);
    }

    [Fact]
    public void TestSolvePart2()
    {
        string answer = Day1.SolvePart2(this.input2);
        Assert.Equal("281", answer);
    }

    [Fact]
    public void TestSolvePart2EdgeCase()
    {
        string answer = Day1.SolvePart2(this.input3);
        // 83 + 79 = 162
        Assert.Equal("162", answer);
    }
}