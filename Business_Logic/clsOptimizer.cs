using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    public class clsOptimizer
    {
        public static Dictionary<string, float> FindBestMix(clsFood[] foods, Dictionary<string, float> initialAmounts, int totalAmount = 100, int step = 10)
        {
            int n = foods.Length;
            float bestScore = -1f;
            Dictionary<string, float> bestCombo = new Dictionary<string, float>();

            // Generate all combinations of amounts that sum to `totalAmount`
            GenerateCombinations(n, totalAmount, step, (amounts) =>
            {
                // Create a mix using the food objects and corresponding amounts
                var foodAmounts = new Dictionary<string, float>();
                for (int i = 0; i < n; i++)
                {
                    foodAmounts[foods[i].Name] = amounts[i];
                }

                clsMix mix = new clsMix(foods, foodAmounts);
                float score = mix.Get_Similarity_Score(); // Or .GetSimilarityScore()
                if (score > bestScore)
                {
                    bestScore = score;
                    bestCombo = new Dictionary<string, float>(foodAmounts);
                }
            });

            return bestCombo;
        }

        // Recursive generator of all combinations of `n` numbers summing to `total` in steps
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
}