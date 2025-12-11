namespace day_06.Tests;

public class ProgramTests
{
    [Fact]
    public void TestParserPart1()
    {
        string[] input =
        [
            "123 328  51 64 ",
            " 45 64  387 23 ",
            "  6 98  215 314",
            "*   +   *   +  "
        ];

        var result = Program.ParseProblemsPart1(input).ToArray();
        
        Assert.Equal(4, result.Length);
        Assert.Collection(result,
            x =>
            {
                Assert.Equal('*', x.Operation);
                Assert.Equal([123, 45, 6], x.Operands);
            },
            x =>
            {
                Assert.Equal('+', x.Operation);
                Assert.Equal([328, 64, 98], x.Operands);
            },
            x =>
            {
                Assert.Equal('*', x.Operation);
                Assert.Equal([51, 387, 215], x.Operands);
            },
            x =>
            {
                Assert.Equal('+', x.Operation);
                Assert.Equal([64, 23, 314], x.Operands);
            });
        }

    [Fact]
    public void TestParserPart2()
    {
        string[] input =
        [
            "123 328  51 64 ",
            " 45 64  387 23 ",
            "  6 98  215 314",
            "*   +   *   +  "
        ];

        var result = Program.ParseProblemsPart2(input).ToArray();

        Assert.Equal(4, result.Length);
        Assert.Collection(result,
            x =>
            {
                Assert.Equal('+', x.Operation);
                Assert.Equal([4, 431, 623], x.Operands);
            },
            x =>
            {
                Assert.Equal('*', x.Operation);
                Assert.Equal([175, 581, 32], x.Operands);
            },
            x =>
            {
                Assert.Equal('+', x.Operation);
                Assert.Equal([8, 248, 369], x.Operands);
            },
            x =>
            {
                Assert.Equal('*', x.Operation);
                Assert.Equal([356, 24, 1], x.Operands);
            });
    }
}

public class ProblemTests
{
    [Fact]
    public void Test_Addition()
    {
        var subject = new Problem('+', [3, 5]);
        
        var result = subject.Solve();
        
        Assert.Equal(8, result);
    }

    [Fact]
    public void Test_Multiplication()
    {
        var subject = new Problem('*', [3, 5]);
        
        var result = subject.Solve();
        
        Assert.Equal(15, result);
    }
    
    [Fact]
    public void Test_InvalidOperation()
    {
        var subject = new Problem('?', [3, 5]);
        
        Assert.Throws<ArgumentOutOfRangeException>(() => subject.Solve());
    }
}