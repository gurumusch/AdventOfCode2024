using System.Data;

namespace AdventOfCode2024;

public static class Day5
{
    public static void Run()
    {
        Part1();
    }

    private static void Part1()
    {
        var (rules, updates) = LoadInput();

        var sumOfMiddlePageNumber = updates
            .Where(update =>
            {
                foreach (var rule in rules)
                {
                    var fulfills = update.FulfillsRule(rule);
                    if (!fulfills)
                    {
                        return false;
                    }
                }

                return true;
            })
            .Select(update => update.ToArray()[update.Count / 2])
            .Sum();
        
        Console.WriteLine($"Day5 Part1 | sumOfMiddlePageNumber: {sumOfMiddlePageNumber}");
    }
    
    
    private static (List<Tuple<int,int>> Rules, List<List<int>> Updates) LoadInput()
    {
        var input = File.ReadAllText("inputs/day5.txt");
        
        var lines = input.Split("\n").ToList();

        var rules = lines
            .TakeWhile(l => !string.IsNullOrWhiteSpace(l))
            .Select(line => line
                .Split("|")
                .Select(int.Parse)
                .ToArray()
            )
            .Select(values => new Tuple<int, int>(values[0], values[1]))
            .ToList();

        var updates = lines
            .SkipWhile(line => !string.IsNullOrWhiteSpace(line))
            .Skip(1)
            .Select(line => line.Split(",").Select(int.Parse).ToList())
            .ToList();
        
        return (Rules: rules, Updates: updates);
    }
}

public static class UpdateExtensions
{
    public static bool FulfillsRule(this List<int> update, Tuple<int, int> rule)
    {
        var lastA = -1;
        var firstB = -1;

        for (var i = 0; i < update.Count; i++)
        {
            if (update[i] == rule.Item1) lastA = i;
            if (update[i] == rule.Item2) firstB = firstB<0 ? i : firstB;
        }
        
        if(firstB == -1 ||  lastA == -1) return true;
        
        return lastA < firstB;
    }
}