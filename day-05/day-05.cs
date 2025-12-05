namespace day_05;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputFilePath = args.Length > 0 ? args[0] : "input.txt";

        using var input = File.OpenText(inputFilePath);

        // Parse the ranges
        var ranges = new List<InclusiveRange>();

        var line = input.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            var ends = line.Split('-');
            ranges.Add(new (long.Parse(ends[0]), long.Parse(ends[1])));
            
            line = input.ReadLine();
        }
        
        // Parse the IDs to check
        var idsToCheck = new List<long>();

        line = input.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            idsToCheck.Add(long.Parse(line));
            line = input.ReadLine();
        }

        var part1 = idsToCheck.Count(id => ranges.Any(range => range.Contains(id)));
        Console.Out.WriteLine($"Part 1: {part1}");
        
        var trimmedRanges = TrimRanges(ranges).ToList();
        var part2 = trimmedRanges.Sum(range => range.SizeInclusive);
        Console.Out.WriteLine($"Part 2: {part2}");
    }
    
    /// <summary>
    /// Removes overlaps in ranges from a list of inclusive ranges.
    /// If one range completely covers another, the smaller one is removed.
    /// </summary>
    public static IEnumerable<InclusiveRange> TrimRanges(List<InclusiveRange> ranges)
    {
        var i = 0;
        while (i < ranges.Count)
        {
            var currentRange = ranges[i];
            var currentRangeDeleted = false;
            
            // Go through the remaining ranges and remove any overlaps with the current range
            var j = i + 1;
            while (j < ranges.Count)
            {
                var otherRange = ranges[j];
                
                // If the other range is completely inside this one, delete the other one
                // i |--------|
                // j  |------|
                // j' (deleted)
                if (otherRange.Start >= currentRange.Start && otherRange.End <= currentRange.End)
                {
                    ranges.RemoveAt(j);
                }
                
                // If the other range completely covers this one, delete this one
                // i     |-----|
                // j   |---------|
                // i'    (deleted)
                else if (otherRange.Start < currentRange.Start && otherRange.End > currentRange.End)
                {
                    ranges.RemoveAt(i);
                    currentRangeDeleted = true;
                    break;
                }
                
                // The start of the other range overlaps the end of this one.
                // Trim the other range to start after this one.
                // i |-----------|
                // j           |-------|
                // j'             |----|
                else if (otherRange.Start <= currentRange.End && otherRange.End > currentRange.End)
                {
                    otherRange.Start = ranges[i].End + 1;
                    ranges[j] = otherRange;
                    j++;
                }
                
                // The end of the other range overlaps the start of this one.
                // Trim the other range to end before this one.
                // i             |---------|
                // j   |----------|
                // j'  |--------|
                else if (otherRange.End >= ranges[i].Start && otherRange.Start < ranges[i].Start)
                {
                    otherRange.End = ranges[i].Start - 1;
                    ranges[j] = otherRange;
                    j++;
                }

                // No overlaps!
                else
                {
                    j++;
                }
            }

            if (currentRangeDeleted) continue;
            
            yield return ranges[i];
            i++;
        }
    }
}

public record struct InclusiveRange(long Start, long End)
{
    public long SizeInclusive => End - Start + 1;
    public bool Contains(long value) => value >= Start && value <= End;
}