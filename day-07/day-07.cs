namespace day_07;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputFilePath = args.Length > 0 ? args[0] : "input.txt";
        var map = File.ReadAllLines(inputFilePath).Select(line => line.ToCharArray()).ToArray();

        var startingX = map[0].IndexOf('S');
        
        var part1 = CountSplits(map, startingX, 1);
        Console.Out.WriteLine($"Part 1: {part1}");
        
        var part2 = CountPaths(map, startingX, 1);
        Console.Out.WriteLine($"Part 2: {part2}");
    }
    
    public static long CountSplits(char[][] map, int x, int y)
    {
        // Move down as long as there is a '.' (empty space)
        while (y < map.Length && map[y][x] == '.')
        {
            map[y][x] = '|'; // Mark the path
            y++;
        }
        
        // If we reached the bottom, there were no splits
        if (y == map.Length) return 0;
        
        switch (map[y][x])
        {
            // If we reached another beam, that path has already been counted
            case '|':
                return 0;
            
            // If we reached a splitter, count the paths to both sides
            case '^':
            {
                var splits = 1L;

                // If there is not already a beam to the left, follow that path
                if (map[y][x - 1] == '.')
                {
                    splits += CountSplits(map, x - 1, y);
                }

                // If there is not already a beam to the right, follow that path too
                if (map[y][x + 1] == '.')
                {
                    splits += CountSplits(map, x + 1, y);
                }

                return splits;
            }
            
            default:
                throw new InvalidOperationException($"Unexpected character {map[y][x]} encountered in the map.");
        }
    }

    // Memoize the results from each splitter so this doesn't take an eternity
    private static readonly Dictionary<(int, int), long> PathMemos = [];
    
    public static long CountPaths(char[][] map, int x, int y)
    {
        // Move down as long as we aren't hitting a splitter or the bottom
        while (y < map.Length && map[y][x] != '^') y++;
        
        // If we hit the bottom, this is the one path
        if (y == map.Length) return 1;

        // Otherwise, we hit a splitter. 
        // Check if we've already counted the paths from here
        if (PathMemos.TryGetValue((x, y), out var result)) return result;
        
        // Count the paths to both sides.
        var paths = CountPaths(map, x - 1, y) + CountPaths(map, x + 1, y);
        
        // Save the result and return it
        return PathMemos[(x, y)] = paths;
    }
}