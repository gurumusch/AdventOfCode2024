namespace AdventOfCode2024;

public static class Day2
{
    public static void Run()
    {
        Part1();
        Part2();
    }

    private static void Part1()
    {
        var reports = LoadReports();

        var numberOfSafeReports = reports
            .Select(CheckReport)
            .Count(result => result);
        Console.WriteLine($"Day2 Part1 | numberOfSafeReports: {numberOfSafeReports}");
    }

    private static void Part2()
    {
        var reports = LoadReports();

        var numberOfSafeReports = reports
            .Select(CheckReportWithTolerance)
            .Count(result => result);
        Console.WriteLine($"Day2 Part2 | numberOfSafeReports: {numberOfSafeReports}");
    }

    private static List<List<int>> LoadReports()
    {
        var lines = File.ReadAllLines($"inputs/day2.txt");
        var reports = lines
            .Select(l => l
                .Split(" ")
                .Select(int.Parse)
                .ToList())
            .ToList();
        return reports;
    }
        private static bool CheckReportWithTolerance(List<int> report)
        {
            if (CheckReport(report))
            {
                return true;
            }

            return report
                .Select((_, i) => report
                    .Where((_, i2) => i2!=i)
                    .ToList()
                )
                .Any(CheckReport);
        }
    
        private static bool CheckReport(List<int> report)
        {
            return report
                .Aggregate((Safe: true, Previous: int.MinValue, Order: 0), (tuple, level) =>
                {
                    // first entry
                    if (tuple is { Order: 0, Previous: int.MinValue }) return (Safe: true, Previous: level, Order: 0);
                    // pass unsafe values
                    if (tuple is { Safe: false }) return (Safe: false, Previous: level, Order: 0);

                    int difference = level - tuple.Previous;
                    var safe = tuple.Safe &&
                               1 <= Math.Abs(difference) && Math.Abs(difference) <= 3;
                    var order = !safe ? 0 : difference / Math.Abs(difference);
                    // second entry with same level
                    if (tuple is { Order: 0 }) return (Safe: safe, Previous: level, Order: order);

                    safe &= order == tuple.Order;
                    return (Safe: safe, Previous: level, tuple.Order);
                }).Safe;
        }
    }    
