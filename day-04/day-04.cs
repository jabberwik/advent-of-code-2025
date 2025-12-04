namespace day_04;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputFilePath = args.Length > 0 ? args[0] : "input.txt";
        
        var map = File.ReadAllLines(inputFilePath).Select(row => row.ToCharArray()).ToArray();
        
        var part1 = FindRemovablePaper(map).ToArray();
        Console.Out.WriteLine($"Part 1: {part1.Count()}");
        
        var part2 = 0;
        var removablePaper = part1;
        
        while (removablePaper.Length > 0)
        {
            foreach (var (x, y) in removablePaper)
            {
                map[y][x] = '.';
                part2++;
            }
            
            removablePaper = FindRemovablePaper(map).ToArray();
        }
        Console.Out.WriteLine($"Part 2: {part2}");
    }
    
    private const char PaperIndicator = '@';

    /// <summary>
    /// Searches through a map and returns a list of map locations where the paper can be removed.
    /// </summary>
    public static IEnumerable<(int X, int Y)> FindRemovablePaper(char[][] map, int threshold = 4)
    {
        foreach (var (rowIndex, row) in map.Index())
        {
            foreach (var (colIndex, cell) in row.Index())
            {
                if (cell != PaperIndicator) continue; // Looking only for rolls of paper
                var adjacentCount = CountAdjacent(PaperIndicator, map, rowIndex, colIndex);
                if (adjacentCount < threshold) yield return (colIndex, rowIndex);
            }
        }
    }

    private static readonly (int X, int Y)[] SearchDirections = 
    [
        (-1, -1), // Up-Left
        (0, -1),  // Up
        (1, -1),  // Up-Right
        (-1, 0),  // Left
        (1, 0),   // Right
        (-1, 1),  // Down-Left
        (0, 1),   // Down
        (1, 1)    // Down-Right
    ];

    /// <summary>
    /// Searches the eight locations adjacent to a 2D array location for a given element and returns the number found
    /// </summary>
    public static int CountAdjacent<TElement>(TElement needle, TElement[][] haystack, int rowIndex, int colIndex)
        where TElement : IEquatable<TElement>
    {
        var count = 0;
        // Search in each defined direction.
        foreach (var (DX, DY) in SearchDirections)
        {
            var (searchCol, searchRow) = (colIndex + DX, rowIndex + DY);

            // If the location is in-bounds and we found the thing there, increment the count.
            if (searchCol >= 0 &&
                searchRow >= 0 &&
                searchCol < haystack.Length &&
                searchRow < haystack[searchCol].Length &&
                haystack[searchRow][searchCol].Equals(needle))
            {
                count++;
            }
        }
        return count;
    }
}