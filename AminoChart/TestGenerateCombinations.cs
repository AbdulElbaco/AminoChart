using System;
using System.Collections.Generic;

class TestGenerateCombinations
{
    public static void Test()
    {
        // Test parameters
        int n = 3; // Number of elements
        int total = 100; // Total sum
        int step = 5; // Step size

        // List to store generated combinations
        List<List<float>> combinations = new List<List<float>>();

        // Call GenerateCombinations
        GenerateCombinations(n, total, step, (combo) =>
        {
            combinations.Add(new List<float>(combo));
        });

        // Print the combinations
        foreach (var combo in combinations)
        {
            Console.WriteLine(string.Join(", ", combo));
        }

        Console.WriteLine($"Total combinations: {combinations.Count}");
    }

    private static void GenerateCombinations(int n, int total, int step, Action<List<float>> onCombo, List<float> current = null)
    {
        current ??= new List<float>();

        if (current.Count == n - 1)
        {
            float last = total - current.Sum();
            if (last >= 0 && last % step == 0)
            {
                current.Add(last);
                onCombo(current);
                current.RemoveAt(current.Count - 1);
            }
            return;
        }

        for (int i = 0; i <= total; i += step)
        {
            current.Add(i);
            GenerateCombinations(n, total, step, onCombo, current);
            current.RemoveAt(current.Count - 1);
        }
    }
}