namespace AdventOfCode2024;

public static class Day1
{
    public static void Run()
    {
        Part1();
        Part2();
    }

    private static void Part1()
    {
        var (leftList, rightList) = LeftList();

        leftList.Sort();
        rightList.Sort();

        var left = leftList.ToArray();
        var right = rightList.ToArray();

        var distance = 0;
        for (var i = 0; i < Math.Min(left.Length, right.Length); i++)
        {
            distance += Math.Abs(left[i] - right[i]);
        }

        Console.WriteLine($"Day1 Part1 | distance: {distance}");
    }

    private static void Part2()
    {
        var (leftList, rightList) = LeftList();

        leftList.Sort();
        var amountsFromRight = rightList.GroupBy(x=>x).ToDictionary(x => x.Key, x => x.Count());

        var similarity = leftList
            .Where(fromLeft => amountsFromRight.ContainsKey(fromLeft))
            .Select(fromLeft => amountsFromRight[fromLeft] * fromLeft)
            .Sum();

        Console.WriteLine($"Day1 Part2 | similarity: {similarity}");
    }

    private static (List<int> leftList, List<int> rightList) LeftList()
    {
        var rawData = File.ReadAllText("inputs/day1.txt");
        var rawLines = rawData.Split('\n');
        var (leftList, rightList) = rawLines
            .Select(line =>
                line
                    .Split(" ")
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => int.Parse(s))
                    .ToArray()
            )
            .Aggregate((left: new List<int>(), right: new List<int>()),
                (acc, cur) =>
                {
                    acc.left.Add(cur[0]);
                    acc.right.Add(cur[1]);
                    return acc;
                });
        return (leftList, rightList);
    }
}