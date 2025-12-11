namespace day_08;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputFilePath = args.Length > 0 ? args[0] : "input.txt";

        var points = File.ReadAllLines(inputFilePath).ParsePoints();

        var closestPairs = GetAllPairwiseDistances(points);

        // Assign every point to a circuit containing only itself
        var circuits = points.Select(x => new List<Point3D> { x }).ToList();
        
        // Connect the closest 1000 pairs
        foreach (var (pointA, pointB, _) in closestPairs[..999])
        {
            ConnectCircuits(circuits, pointA, pointB);
        }
        
        // Multiply the lengths of the three largest circuits
        var part1 = circuits
            .OrderByDescending(c => c.Count)
            .Take(3)
            .Aggregate(1L, (total, next) => total * next.Count);
        Console.Out.WriteLine($"Part 1: {part1}");

        // Continue iterating until we only have one circuit
        using var iterator = closestPairs[1000..].GetEnumerator();
        while (circuits.Count > 1 && iterator.MoveNext())
        {
            var (pointA, pointB, _) = iterator.Current;
            ConnectCircuits(circuits, pointA, pointB);
        }
        
        // Multiply the X coordinates of the last two points we connected
        var part2 = iterator.Current.PointA.X * iterator.Current.PointB.X;
        Console.Out.WriteLine($"Part 2: {part2}");
    }

    /// <summary>
    /// Returns all pairwise distances between all points in the array, shortest first
    /// </summary>
    /// <remarks>Uses brute force, so it's best to only call this once</remarks>
    public static List<(Point3D PointA, Point3D PointB, double Distance)> GetAllPairwiseDistances(Point3D[] points)
    {
        var distances = new List<(Point3D, Point3D, double Distance)>();
        for (var i = 0; i < points.Length; i++)
        {
            for (var j = i + 1; j < points.Length; j++)
            {
                var distance = Point3D.Distance(points[i], points[j]);
                distances.Add((points[i], points[j], distance));
            }
        }
        return [.. distances.OrderBy(d => d.Distance)];
    }
    
    public static void ConnectCircuits(List<List<Point3D>> circuits, Point3D pointA, Point3D pointB)
    {
        // Find which circuit each point is currently in
        var pointACircuit = circuits.Single(c => c.Contains(pointA));
        var pointBCircuit = circuits.Single(c => c.Contains(pointB));

        // If they're already in the same circuit, nothing more to do!
        if (pointACircuit == pointBCircuit) return;
            
        // Otherwise, connect them. Move all points from point B's circuit into point A's circuit
        pointACircuit.AddRange(pointBCircuit);
        circuits.Remove(pointBCircuit);
    }
}

public static class FileExtensions
{
    /// <summary>
    /// Parse a comma-separated list of 3-digit numbers into a Point3D array, assuming X,Y,Z order
    /// </summary>
    public static Point3D[] ParsePoints(this string[] lines) =>
        lines.Select(line => line.Split(','))
            .Select(parts => new Point3D(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])))
            .ToArray();
}

public record Point3D(long X, long Y, long Z)
{
    /// <summary>
    /// Calculate Euclidean distance between two 3D points
    /// </summary>
    public static double Distance(Point3D pointA, Point3D pointB) =>
        Math.Sqrt(Math.Pow(pointA.X - pointB.X, 2) +
                  Math.Pow(pointA.Y - pointB.Y, 2) +
                  Math.Pow(pointA.Z - pointB.Z, 2));
}