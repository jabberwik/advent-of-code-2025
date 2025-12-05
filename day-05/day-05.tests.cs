namespace day_05.Tests;

public class ProgramTests
{
    [Fact]
    public void TestTrimRanges_OverlappingInner()
    {
        InclusiveRange[] input =
        [
            new(10, 20),
            new(15, 18)
        ];

        var result = Program.TrimRanges(input.ToList());

        InclusiveRange[] expected =
        [
            new(10, 20)
        ];

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestTrimRanges_OverlappingExact()
    {
        InclusiveRange[] input =
        [
            new(10, 20),
            new(10, 20)
        ];

        var result = Program.TrimRanges(input.ToList());

        InclusiveRange[] expected =
        [
            new(10, 20)
        ];

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestTrimRanges_OverlappingOuter()
    {
        InclusiveRange[] input =
        [
            new(10, 20),
            new(8, 22)
        ];

        var result = Program.TrimRanges(input.ToList());

        InclusiveRange[] expected =
        [
            new(8, 22)
        ];

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestTrimRanges_OverlappingStart()
    {
        InclusiveRange[] input =
        [
            new(10, 20),
            new(18, 22) // Start overlaps the previous range end
        ];

        var result = Program.TrimRanges(input.ToList());

        InclusiveRange[] expected =
        [
            new(10, 20),
            new(21, 22)
        ];

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestTrimRanges_OverlappingStartExact()
    {
        InclusiveRange[] input =
        [
            new(10, 20),
            new(20, 22) // Start overlaps the previous range
        ];

        var result = Program.TrimRanges(input.ToList());

        InclusiveRange[] expected =
        [
            new(10, 20),
            new(21, 22)
        ];

        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestTrimRanges_OverlappingEnd()
    {
        InclusiveRange[] input =
        [
            new(10, 20),
            new(8, 12) // End overlaps the previous range
        ];

        var result = Program.TrimRanges(input.ToList());

        InclusiveRange[] expected =
        [
            new(10, 20),
            new(8, 9)
        ];

        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestTrimRanges_OverlappingEndExact()
    {
        InclusiveRange[] input =
        [
            new(10, 20),
            new(8, 10) // End overlaps the previous range
        ];

        var result = Program.TrimRanges(input.ToList());

        InclusiveRange[] expected =
        [
            new(10, 20),
            new(8, 9)
        ];

        Assert.Equal(expected, result);
    }
}
