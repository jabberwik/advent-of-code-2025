namespace day_06;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputFilePath = args.Length > 0 ? args[0] : "input.txt";

        var lines = File.ReadAllLines(inputFilePath);
        
        var problemsPart1 = ParseProblemsPart1(lines).ToArray();
        var part1 = problemsPart1.Sum(problem => problem.Solve());
        Console.Out.WriteLine($"Part 1: {part1}");
        
        var problemsPart2 = ParseProblemsPart2(lines).ToArray();
        var part2 = problemsPart2.Sum(problem => problem.Solve());
        Console.Out.WriteLine($"Part 2: {part2}");
    }

    public static IEnumerable<Problem> ParseProblemsPart1(string[] lines)
    {
        // On every line, split on spaces and trim the results
        var cells = lines.Select(line =>
            line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)).ToArray();

        // Make sure we ended up with the same number of cells on each line
        if (cells.Select(row => row.Length).Distinct().Count() > 1)
            throw new Exception("Lines must have the same number of entries");
        
        // Loop through each "column"
        for (var i = 0; i < cells[0].Length; i++)
        {
            // The operation is a single character on the last line
            var operation = cells.Last()[i][0];
            
            // Parse ints on all but the last line
            var operands = cells.Take(cells.Length - 1).Select(row => long.Parse(row[i])).ToArray();

            yield return new Problem(operation, operands);
        }
    }

    public static IEnumerable<Problem> ParseProblemsPart2(string[] lines)
    {
        // All lines must be the same length
        if (lines.Select(line => line.Length).Distinct().Count() > 1)
            throw new Exception("Lines must be the same length");
        
        var operands = new List<long>();
        
        // Go through each column starting at the end and work back until we're past the end
        for (var col = lines[0].Length - 1; col >= -1; col--)
        {
            // If we have a blank column, or we have read all the columns (col is -1)
            if (col == -1 || lines.All(line => line[col] == ' '))
            {
                // The operation is on the last line of the next column
                var operation = lines.Last()[col + 1];
                
                // The operands have all been parsed and we can return the Problem
                yield return new Problem(operation, operands.ToArray());
                operands.Clear();
                continue;
            }
            
            // Read down the column, except for the last line
            var chars = lines.Take(lines.Length - 1).Select(line => line[col]).ToArray();
            var operand = long.Parse(chars);
            operands.Add(operand);
        }
    }
}

public record Problem(char Operation, long[] Operands)
{
    public long Solve() => Operation switch
    {
        '+' => Operands.Sum(),
        '*' => Operands.Aggregate(1L, (total, next) => next * total),
        _ => throw new ArgumentOutOfRangeException(nameof(Operation), Operation, "Operation must be '+' or '*'")
    };
}