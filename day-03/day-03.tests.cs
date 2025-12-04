namespace day_03.Tests;

public class LinqExtensionsTests
{
    [Theory]
    [InlineData(new int[0], 2, new int[0])]
    [InlineData(new[] { 1 }, 2, new[] { 1 })]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1 }, 2, new[] { 9, 8 })]
    [InlineData(new[] { 8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9 }, 2, new[] { 8, 9 })]
    [InlineData(new[] { 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 7, 8 }, 2, new[] { 7, 8 })]
    [InlineData(new[] { 8, 1, 8, 1, 8, 1, 9, 1, 1, 1, 1, 2, 1, 1, 1 }, 2, new[] { 9, 2 })]
    [InlineData(new[] { 8, 1, 9, 1, 8, 1, 9, 1, 1, 1, 1, 2, 1, 1, 1 }, 2, new[] { 9, 9 })]
    public void LargestSequence_ReturnsExpectedElements(int[] source, int count, int[] expected)
    {
        // Act
        var result = source.FormLargestSequence(count);
        
        // Assert
        Assert.Equal(expected, result);
    }
}