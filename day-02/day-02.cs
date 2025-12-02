using System.Diagnostics;

namespace day_02;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputFilePath = args.Length > 0 ? args[0] : "input.txt";

        // Parse all the ranges from the input file
        var input = File.ReadAllText(inputFilePath);
        var ranges = input.Split(',').Select(rangeStr =>
        {
            var parts = rangeStr.Split('-');
            return (Start: long.Parse(parts[0]), End: long.Parse(parts[1]));
        }).ToList();
        
        // Part 1: Find all numbers with two repeating halves
        var sumPart1 = ranges.SelectMany(range => GetPatternsPart1(range.Start, range.End)).Sum();
        Console.Out.WriteLine($"Part 1: {sumPart1}");

        // Part 2: Find all numbers with any number of repeating parts
        var sumPart2 = ranges.SelectMany(range => GetPatternsPart2(range.Start, range.End)).Sum();
        Console.Out.WriteLine($"Part 2: {sumPart2}");
    }
    
    public static IEnumerable<long> GetPatternsPart1(long start, long end)
    {
        Debug.Assert(start <= end, "Start of range must be less than or equal to end of range");

        // Just go through the whole range and check each number
        for (var i = start; i <= end; i++)
        {
            var str = i.ToString();
            var length = str.Length;

            // If the length isn't even, it can't have two repeating halves
            if (length % 2 != 0)
            {
                continue;
            }

            // Split the string in half and compare
            var halfLength = length / 2;
            var firstHalf = str[..halfLength];
            var secondHalf = str.Substring(halfLength, halfLength);

            if (firstHalf == secondHalf)
            {
                yield return i;
            }
        }
    }
    
    public static IEnumerable<long> GetPatternsPart2(long start, long end)
    {
        for (var i = start; i <= end; i++)
        {
            var str = i.ToString();
            var length = str.Length;
            
            // Single digits can't repeat
            if (length == 1) continue;

            // Figure out in which ways we can split this string into equal parts
            foreach (var partCount in GetDivisors(length))
            {
                // Split the string into partCount parts of equal length
                var partLength = length / partCount;
                var parts = Enumerable.Range(0, partCount)
                    .Select(partIndex => str.Substring(partIndex * partLength, partLength))
                    .ToList();

                // If the parts are all the same, it's a match
                if (parts.All(part => part == parts[0]))
                {
                    yield return i;
                    break; // Don't need to check further divisors
                }
            }
        }
    }

    /// <summary>
    /// Returns all divisors of n greater than 1
    /// </summary>
    public static IEnumerable<int> GetDivisors(int n)
    {
        for (var i = 2; i <= n / 2; i++)
        {
            if (n % i == 0)
            {
                yield return i;
            }
        }
        yield return n;
    } 
}