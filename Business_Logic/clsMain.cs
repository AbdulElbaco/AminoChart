using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Business_Logic
{
    public static class clsMain
    {
        static public Dictionary<string, float> Initilize_Amounts(clsFood[] Foods)
        {
            Dictionary<string, float> Amounts = new Dictionary<string, float>();
            float equalPercentage = 1.0f;
            equalPercentage /= Foods.Length;
            for (int i = 0; i < Foods.Length; i++)
            {
                Amounts[Foods[i].Name] = equalPercentage;
            }

            Amounts = Amounts.OrderByDescending(pair => pair.Value).ToDictionary();
            return Amounts;
        }
        
        static private void Optimize_Element(clsFood[] FoodsList, ref Dictionary<string, float> Amounts, string ElementName)
        {
            ///
        }
        static private Dictionary<string, float> IncreaseElement(clsFood[] FoodsList, 
            Dictionary<string, float> Amounts, 
            string ElementName, 
            float IncreaseAmount)
        {
            var newAmounts = new Dictionary<string, float>(Amounts);
            float totalRedistribute = IncreaseAmount;
            int elementsToAdjust = newAmounts.Count - 1;

            foreach(string element in newAmounts.Keys.Where(k => k != ElementName))
            {
                float maxReduction = newAmounts[element] - 0.01f; // Minimum 1%
                float reduction = Math.Min(maxReduction, totalRedistribute/elementsToAdjust);
                
                newAmounts[element] -= reduction;
                totalRedistribute -= reduction;
            }

            newAmounts[ElementName] += IncreaseAmount - totalRedistribute;
            return newAmounts;
        }

        static private void Optimize_Amounts(clsFood[] FoodsList, ref Dictionary<string, float> Amounts)
        {
            foreach(clsFood Food in FoodsList)
            {
                Optimize_Element(FoodsList, ref Amounts, Food.Name);
            }
        }
        static public Dictionary<string, float> WhatIsTheBestMix(clsFood[] FoodsList)
        {
            //Initilize Amounts with equal amounts
            Dictionary<string, float> Amounts = Initilize_Amounts(FoodsList);

            //Find out the right amount of each element
            Optimize_Amounts(FoodsList, ref Amounts);

            //Return Amounts List
            return Amounts;
        }

    }
}