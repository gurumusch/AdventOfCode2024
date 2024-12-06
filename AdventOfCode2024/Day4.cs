using System.Text;

namespace AdventOfCode2024;

public static class Day4
{
    public static void Run()
    {
        Part1();
        Part2();
    }

    private static void Part1()
    {
        var matrix = LoadCharMatrix();

        int count = 0;
        for (int row = 0; row < matrix.Length; row++)
        {
            for (int col = 0; col < matrix[row].Length; col++)
            {
                count += GetRays(matrix, row, col).Count(r => r is "XMAS" or "SAMX");
            }
        }
        
        Console.WriteLine($"Day4 Part1 | count: {count}");
    }

    private static List<string> GetRays(char[][] matrix, int row, int col)
    {
        var result = new List<string>();
        if (row <matrix.Length - 3)
            result.Add("" + matrix[row][col] + matrix[row+1][col] + matrix[row+2][col] + matrix[row+3][col]);
        if (col <matrix[row].Length - 3)
            result.Add("" + matrix[row][col] + matrix[row][col+1] + matrix[row][col+2] + matrix[row][col+3]);
        if (row < matrix.Length - 3 && col < matrix[row].Length - 3)
        {
            result.Add("" + matrix[row][col] + matrix[row + 1][col + 1] + matrix[row + 2][col + 2] + matrix[row + 3][col + 3]);
            result.Add("" + matrix[row+3][col] + matrix[row + 2][col + 1] + matrix[row + 1][col + 2] + matrix[row][col + 3]);
        }

        return result;
    }

    private static void Part2()
    {
        var matrix = LoadCharMatrix();
        
        int count = 0;
        for (int row = 0; row < matrix.Length-2; row++)
        {
            for (int col = 0; col < matrix[row].Length-2; col++)
            {
                count += IsXHit(matrix, row, col) ? 1 : 0;
            }
        }
        
        Console.WriteLine($"Day4 Part2 | count: {count}");
    }

    private static bool IsXHit(char[][] matrix, int row, int col)
    {
        List<string> diagonals = [
            "" + matrix[row][col+2] + matrix[row+1][col+1] + matrix[row+2][col],
            "" + matrix[row][col] + matrix[row+1][col+1] + matrix[row+2][col+2]
        ];
        
        return diagonals.All(diagonal => diagonal is "MAS" or "SAM");
    }
    
    private static char[][] LoadCharMatrix()
    {
        var input = File.ReadAllText("inputs/day4.txt");
        return input
            .Split('\n')
            .Select(line => line.ToCharArray())
            .ToArray();
    }
}