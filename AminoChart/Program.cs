using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Business_Logic;

class Program
{
    static void Main()
    {
        clsFood Chickpeas = new clsFood("Chickpeas (Cicer arietinum)");//   64.188995%
        clsFood SweatLupini = new clsFood("Sweet Lupini (Lupinus albus)");//  64.188995%
        clsFood GreenPeas = new clsFood("Green Peas (Pisum sativum)");//  67.13503 %
        clsFood Oats = new clsFood("Oats (Avena sativa)");     //    65.66902 %
        clsFood Egg = new clsFood("Egg (whole, hard-boiled)");      //76.14811 %

        clsFood[] FoodsList = {Chickpeas, SweatLupini, GreenPeas, Oats};
        Dictionary<string, float> Amounts = clsMain.Initilize_Amounts(FoodsList);

        clsMix Mix = new clsMix(FoodsList, Amounts);

        Amounts = clsMain.WhatIsTheBestMix(FoodsList);
        clsMix NewMix = new clsMix(FoodsList, Amounts);

        Console.WriteLine($"Chickpeas' Similarity Score: {Chickpeas.Get_Similarity_Score()}");
        Console.WriteLine($"Equal splits: {Mix.Get_Similarity_Score()}");
        Console.WriteLine($"Optimized splits: {NewMix.Get_Similarity_Score()}");

        float normalized = 0f;
        foreach (string Name in Amounts.Keys)
        {
            Console.WriteLine(Name + " : " + Amounts[Name]);
            normalized += Amounts[Name];
        }

        //To encsure normalization must show 1
        Console.WriteLine(normalized.ToString());

        //Addional Features:
        //Highlighting the lack of AAs in the food
        //Generating the best mix of foods to get the missing AAs
    }
}


////clsFood SweatLupini = new clsFood("Chickpeas (Cicer arietinum)");   64.188995%
////clsFood SweatLupini = new clsFood("Sweet Lupini (Lupinus albus)");  64.188995%
////clsFood SweatLupini = new clsFood("Green Peas (Pisum sativum)");//  67.13503 %
////clsFood SweatLupini = new clsFood("Oats (Avena sativa)");     //    65.66902 %
//clsFood SweatLupini = new clsFood("Egg (whole, hard-boiled)");      //76.14811 %

//Console.WriteLine("Food Name: " + SweatLupini.Name);
//Console.WriteLine("Protein Percentage: " + SweatLupini.Protein_Percentage);
//Console.WriteLine("Similarity Score: " + SweatLupini.Get_Similarity_Score());