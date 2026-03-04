namespace ResultDotNet.Examples;

public static class CountLinesExample
{
    public static void Run(string filePath)
    {
        ReadFileLines(filePath)
            .Map(x => x.Count())
            .Match(
                value => Console.WriteLine($"The '{filePath}' file has {value} lines."),
                error => Console.WriteLine($"Error counting lines: {error}"));
    }

    private static Result<IEnumerable<string>, string> ReadFileLines(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return Result<IEnumerable<string>, string>.FromError($"File '{filePath}' does not exist.");
        }

        var lines = File.ReadLines(filePath);
        return Result<IEnumerable<string>, string>.FromValue(lines);
    }
}