using System.Runtime.InteropServices;

namespace day_02.Tests;

public class ProgramTests
{
    [Theory]
    [InlineData(11, 22, new long[] { 11, 22 })]
    [InlineData(95, 115, new long[] { 99 })]
    [InlineData(998, 1012, new long[] { 1010 })]
    [InlineData(1188511880, 1188511890, new long[] { 1188511885 })]
    [InlineData(222220, 222224, new long[] { 222222 })]
    [InlineData(1698522, 1698528, new long[] { })]
    [InlineData(565653, 565659, new long[] { })]
    [InlineData(1, 9, new long[] { })]
    public void TestPart1Patterns(long rangeStart, long rangeEnd, long[] expectedPatterns)
    {
        var result = Program.GetPatternsPart1(rangeStart, rangeEnd);
        Assert.Equal(expectedPatterns, result);
    }
    
    [Theory]
    [InlineData(11, 22, new long[] { 11, 22 })]
    [InlineData(95, 115, new long[] { 99, 111 })]
    [InlineData(998, 1012, new long[] { 999, 1010 })]
    [InlineData(1188511880, 1188511890, new long[] { 1188511885 })]
    [InlineData(222220, 222224, new long[] { 222222 })]
    [InlineData(1698522, 1698528, new long[] { })]
    [InlineData(565653, 565659, new long[] { 565656 })]
    [InlineData(1, 9, new long[] { })]
    public void TestPart2Patterns(long rangeStart, long rangeEnd, long[] expectedPatterns)
    {
        var result = Program.GetPatternsPart2(rangeStart, rangeEnd);
        Assert.Equal(expectedPatterns, result);
    }
}