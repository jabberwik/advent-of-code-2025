namespace day_07.Tests;

public class ProgramTests
{
    private const string TestInput = """
                                     .......S.......
                                     ...............
                                     .......^.......
                                     ...............
                                     ......^.^......
                                     ...............
                                     .....^.^.^.....
                                     ...............
                                     ....^.^...^....
                                     ...............
                                     ...^.^...^.^...
                                     ...............
                                     ..^...^.....^..
                                     ...............
                                     .^.^.^.^.^...^.
                                     ...............
                                     """;

    [Fact]
    public void CountSplits_ShouldReturnCorrectNumberOfSplits()
    {
        var map = TestInput.Split('\n').Select(line => line.ToCharArray()).ToArray();

        var startingX = map[0].IndexOf('S');
        
        var result = Program.CountSplits(map, startingX, 1);
        
        Assert.Equal(21, result);
    }

    [Fact]
    public void CountPaths_ShouldReturnCorrectNumberOfPaths()
    {
        var map = TestInput.Split('\n').Select(line => line.ToCharArray()).ToArray();

        var startingX = map[0].IndexOf('S');
        
        var result = Program.CountPaths(map, startingX, 1);
        
        Assert.Equal(40, result);
    }
}