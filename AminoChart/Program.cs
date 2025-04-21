using Business_Logic;

class Program
{
    static void Main()
    {
        //Testing
        //TestGenerateCombinations.Test();



        //Decleare and intitilize some foods to test

        clsFood Chickpeas = new clsFood("Chickpeas (Cicer arietinum)");      //64.188995%
        clsFood SweatLupini = new clsFood("Sweet Lupini (Lupinus albus)");  //64.188995%
        clsFood GreenPeas = new clsFood("Green Peas (Pisum sativum)");     //67.13503 %
        clsFood Oats = new clsFood("Oats (Avena sativa)");                //65.66902 %
        clsFood Egg = new clsFood("Egg (whole, hard-boiled)");           //76.14811 %

        //Group the foods in an array
        clsFood[] FoodsList = {Chickpeas, SweatLupini, GreenPeas, Oats};

        Dictionary<string, float> Amounts = new Dictionary<string, float>();

        foreach(var food in FoodsList)
        {
            Amounts[food.Name] = 0.25f;
        }

        clsMix Mix = new clsMix(FoodsList, Amounts);

        Amounts = clsOptimizer.FindBestMix(FoodsList, Amounts);

        //Print all similarity scores
        for (int i = 0;  i < FoodsList.Length; i++)
        {
            Console.WriteLine($"{FoodsList[i].Name}'s Similarity Score: {FoodsList[i].Get_Similarity_Score()}");
        }
        // print similarity score of mix
        Console.WriteLine($"Mix's Similarity Score: {Mix.Get_Similarity_Score()}");

        //Optimized Mix
        Amounts = clsOptimizer.FindBestMix(FoodsList, Amounts, 100, 5);
        Mix = new clsMix(FoodsList, Amounts);

        Console.WriteLine($"\nOptimized Mix' Similarity Socre: {Mix.Get_Similarity_Score()}");

        //Addional Features:
        //Highlighting the lack of EAAs in the food
        //Generating the best mix of foods to get the missing AAs
    }
}