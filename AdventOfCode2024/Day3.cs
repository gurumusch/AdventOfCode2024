using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public static class Day3
{
    public static void Run()
    {
        Part1();
        Part2();
    }

    private static void Part1()
    {
        var input = File.ReadAllText("inputs/day3.txt");
        var mulDetector = new Regex(@"(mul\(\d{1,3},\d{1,3}\))");
        var sumOfMultiplications = mulDetector
            .Matches(input)
            .Select(m => m.Value)
            .Select(DoMultiplication)
            .Sum();
        
        Console.WriteLine($"Day3 Part1 | sumOfMultiplications: {sumOfMultiplications}");
    }

    private static void Part2()
    {
        var input = File.ReadAllText("inputs/day3.txt");
        var tokenDetector = new Regex(@"(mul\(\d{1,3},\d{1,3}\))|(do\(\))|(don't\(\))");
        var sumOfMultiplications = tokenDetector
            .Matches(input)
            .Select(m => m.Value)
            .Aggregate((Active: true, Result: 0), (tuple, term) => term switch
            {
                "do()" => (Active: true, Result: tuple.Result),
                "don't()" => (Active: false, Result: tuple.Result),
                _ => (tuple.Active, Result: tuple.Active ? tuple.Result + DoMultiplication(term)  : tuple.Result),
            })
            .Result;

        Console.WriteLine($"Day3 Part2 | sumOfMultiplications: {sumOfMultiplications}");
    }

    private static int DoMultiplication(string mulClause)
    {
        return mulClause
            .Replace("mul(", "")
            .Replace(")", "")
            .Split(",")
            .Select(int.Parse)
            .Aggregate(1, (x, y) => x * y);
    }
}