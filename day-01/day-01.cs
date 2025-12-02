namespace day_01;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputFilePath = args.Length > 0 ? args[0] : "input.txt";

        int dialPosition = 50; // From the puzzle text

        int homes = 0; // Counts how many times we land on home (position 0)
        int zeroPasses = 0; // Counts how many times we pass over home (position 0)

        // Execute each move instruction in sequence
        foreach (var line in File.ReadAllLines(inputFilePath))
        {
            (dialPosition, var newZeroPasses) = MoveDial(dialPosition, line);

            // Keep track of home landings and zero passes
            if (dialPosition == 0) homes++;
            zeroPasses += newZeroPasses;
        }

        Console.Out.WriteLine("Part 1: " + homes);
        Console.Out.WriteLine("Part 2: " + (homes + zeroPasses));
    }

    // Moves the dial from the starting position according to the move instruction.
    // Returns the new position on the dial range and the number of times we passed over position 0.
    // Does not include starting or landing on 0 as passing over.
    public static (int EndingPosition, int ZeroPasses) MoveDial(int startingPosition, string move)
    {
        int position = startingPosition;
        int zeroPasses = 0;

        char moveDirection = move[0];
        int moveAmount = int.Parse(move[1..]);

        if (moveDirection == 'L')
        {
            position -= moveAmount;
            
            if (position < 0) // We wrapped around
            {
                // Figure out how many times we wrapped around
                zeroPasses = (-position / 100) + 1;

                // Wrap back into range
                position = (100 + position % 100) % 100;
                
                // The formula above will overshoot if we started and/or ended exactly at 0
                if (startingPosition == 0) zeroPasses--;
                if (position == 0) zeroPasses--;
            }
        }
        else if (moveDirection == 'R')
        {
            position += moveAmount;

            if (position > 99) // Wrap around
            {
                // Figure out how many times we wrapped around
                zeroPasses = (position - 1) / 100;
                //
                // Wrap back into range
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