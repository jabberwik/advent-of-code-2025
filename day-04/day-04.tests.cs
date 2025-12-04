namespace day_04.Tests;

public class ProgramTests
{
    [Theory]
    [InlineData("""
    ..@@.@@@@.
    @@@.@.@.@@
    @@@@@.@.@@
    @.@@@@..@.
    @@.@@@@.@@
    .@@@@@@@.@
    .@.@.@.@@@
    @.@@@.@@@@
    .@@@@@@@@.
    @.@.@@@.@.
    """, 13)]
    public void FindRemovablePaper_ReturnsExpectedCount(string input, int expected)
    {
        // Arrange
        var map = input.Split('\n').Select(row => row.ToCharArray()).ToArray();

        // Act
        var result = Program.FindRemovablePaper(map);

        // Assert
        Assert.Equal(expected, result.Count());
    }
}