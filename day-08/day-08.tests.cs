namespace day_08.Tests;

public class ProgramTests
{
    private const string TestInput = """
                                     162,817,812
                                     57,618,57
                                     906,360,560
                                     592,479,940
                                     352,342,300
                                     466,668,158
                                     542,29,236
                                     431,825,988
                                     739,650,466
                                     52,470,668
                                     216,146,977
                                     819,987,18
                                     117,168,530
                                     805,96,715
                                     346,949,466
                                     970,615,88
                                     941,993,340
                                     862,61,35
                                     984,92,344
                                     425,690,689
                                     """;

    [Fact]
    public void Test_ParsePoints()
    {
        string[] input = ["1,2,3", "4,5,6"];

        var result = input.ParsePoints();
        
        Assert.Equal(2, result.Length);
        Assert.Equal(new Point3D(1, 2, 3), result[0]);
        Assert.Equal(new Point3D(4, 5, 6), result[1]);
    }
    
    [Fact]
    public void Test_GetPairwiseDistances()
    {
        var input = TestInput.Split('\n').ParsePoints();

        var result = Program.GetAllPairwiseDistances(input);
        
        Assert.Equal(new Point3D(162, 817, 812), result[0].PointA);
        Assert.Equal(new Point3D(425, 690, 689), result[0].PointB);
        
        Assert.Equal(new Point3D(162, 817, 812), result[1].PointA);
        Assert.Equal(new Point3D(431, 825, 988), result[1].PointB);
        
        Assert.Equal(new Point3D(906, 360, 560), result[2].PointA);
        Assert.Equal(new Point3D(805,  96, 715), result[2].PointB);
    }

    [Fact]
    public void Test_ConnectCircuits_DifferentCircuits()
    {
        var pointA = new Point3D(1, 2, 3);
        var pointB = new Point3D(4, 5, 6);
        
        var testCircuits = new List<List<Point3D>>
        {
            new() {pointA},
            new() {pointB},
        };
        
        Program.ConnectCircuits(testCircuits, pointA, pointB);
        
        Assert.Single(testCircuits);
        Assert.Equal(2, testCircuits[0].Count);
        Assert.Contains(pointA, testCircuits[0]);
        Assert.Contains(pointB, testCircuits[0]);
    }
    
    [Fact]
    public void Test_ConnectCircuits_DifferentCircuitsExistingPoints()
    {
        var pointA = new Point3D(1, 2, 3);
        var pointB = new Point3D(4, 5, 6);
        var pointC = new Point3D(7, 8, 9);
        
        var testCircuits = new List<List<Point3D>>
        {
            new() {pointA, pointB},
            new() {pointC},
        };
        
        // Connect two points not in the same circuit
        Program.ConnectCircuits(testCircuits, pointA, pointC);
        
        Assert.Single(testCircuits);
        Assert.Equal(3, testCircuits[0].Count);
        Assert.Contains(pointA, testCircuits[0]);
        Assert.Contains(pointB, testCircuits[0]);
        Assert.Contains(pointC, testCircuits[0]);
    }
    
    [Fact]
    public void Test_ConnectCircuits_SameCircuit()
    {
        var pointA = new Point3D(1, 2, 3);
        var pointB = new Point3D(4, 5, 6);
        var pointC = new Point3D(7, 8, 9);
        
        var testCircuits = new List<List<Point3D>>
        {
            new() {pointA, pointB},
            new() {pointC},
        };
        
        // Connect two points already in the same circuit (should change nothing)
        Program.ConnectCircuits(testCircuits, pointA, pointB);
        
        Assert.Equal(2, testCircuits.Count);
        Assert.Equal(2, testCircuits[0].Count);
        Assert.Contains(pointA, testCircuits[0]);
        Assert.Contains(pointB, testCircuits[0]);
        Assert.Single(testCircuits[1]);
        Assert.Contains(pointC, testCircuits[1]);
    }
}