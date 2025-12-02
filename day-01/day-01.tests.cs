namespace day_01.Tests;

public class ProgramTests
{
        [Theory]
        [InlineData(0, "L1", 99, 0)]
        [InlineData(0, "L100", 0, 0)]
        [InlineData(0, "R100", 0, 0)]
        [InlineData(1, "L100", 1, 1)]
        [InlineData(0, "L101", 99, 1)]
        [InlineData(99, "R1", 0, 0)]
        [InlineData(99, "R100", 99, 1)]
        [InlineData(1, "R100", 1, 1)]
        [InlineData(0, "R101", 1, 1)]
        [InlineData(50, "L68", 82, 1)]
        [InlineData(82, "L30", 52, 0)]
        [InlineData(52, "R48", 0, 0)]
        [InlineData(99, "L99", 0, 0)]
        [InlineData(1, "R99", 0, 0)]
        [InlineData(1, "L300", 1, 3)]
        [InlineData(1, "R300", 1, 3)]
        public void TestMoveDial(int start, string move, int expectedEndingPosition, int expectedZeroPasses)
        {
            var (endingPosition, zeroPasses) = Program.MoveDial(start, move);
            Assert.Equal(expectedEndingPosition, endingPosition);
            Assert.Equal(expectedZeroPasses, zeroPasses);
        }
}