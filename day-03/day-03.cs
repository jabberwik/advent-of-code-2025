namespace day_03;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputFilePath = args.Length > 0 ? args[0] : "input.txt";
        
        var banks = File.ReadAllLines(inputFilePath);
        
        var part1 = banks.Select(line => new string(line.FormLargestSequence(2))).Sum(int.Parse);
        Console.Out.WriteLine($"Part 1: {part1}");

        var part2 = banks.Select(line => new string(line.FormLargestSequence(12))).Sum(long.Parse);
        Console.Out.WriteLine($"Part 2: {part2}");
    }
}

public static class LinqExtensions
{
    /// <summary>
    /// Forms the largest sequence result from the input, assuming big-endian ordering of the source.
    /// </summary>
    public static TElement[] FormLargestSequence<TElement>(this IEnumerable<TElement> input, int scale)
        where TElement : IComparable<TElement>
    {
        if (scale < 0) throw new ArgumentOutOfRangeException(nameof(scale), "Scale must be non-negative.");
        if (scale == 0) return []; // Sort of a recursion base case
        
        var source = input.ToArray(); // Avoid multiple enumerations
        if (source.Length <= scale) return source;

        // Fill the buffer with the last elements of the input
        var buffer = source[^scale..];
        
        // Work backwards through the rest of the input
        foreach (var item in source.Reverse().Skip(scale))
        {
            // If the new item is smaller than the most significant buffer item, it can't increase the result.
            if (item.CompareTo(buffer[0]) < 0) continue;

            // If the new item is greater than or equal to the most significant buffer item, we want to include it.
            // We do this by placing it at the front of the buffer, and then getting the largest sequence from the buffer elements.
            buffer = [item, ..buffer.FormLargestSequence(buffer.Length - 1)];
        }
        
        return buffer;
    }
}