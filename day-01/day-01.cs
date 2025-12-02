namespace day_01;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputFilePath = args.Length > 0 ? args[0] : "input.txt";

        int dialPosition = 50;
        int homes = 0;
        int zeroPasses = 0;

        foreach (var line in File.ReadAllLines(inputFilePath))
        {
            (dialPosition, var newZeroPasses) = MoveDial(dialPosition, line);
            zeroPasses += newZeroPasses;

            if (dialPosition == 0)
            {
                homes++;
            }
        }

        Console.Out.WriteLine("Part 1: " + homes);
        Console.Out.WriteLine("Part 2: " + (homes + zeroPasses));
    }

    public static (int EndingPosition, int ZeroPasses) MoveDial(int startingPosition, string move)
    {
        int position = startingPosition;
        int zeroPasses = 0;
        int moveAmount = int.Parse(move[1..]);

        if (move[0] == 'L')
        {
            position -= moveAmount;
            if (position < 0)
            {
                zeroPasses += (-position / 100) + 1;
                if (startingPosition == 0)
                {
                    zeroPasses--;
                }
                position = (100 + position % 100) % 100;
                if (position == 0)
                {
                    zeroPasses--;
                }
            }
        }
        else if (move[0] == 'R')
        {
            position += moveAmount;
            if (position > 99)
            {
                zeroPasses += (position - 1) / 100;
                position = position % 100;
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(move), move, "Move must start with 'L' or 'R'");
        }

        return (position, zeroPasses);
    }
}