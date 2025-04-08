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
        //clsFood SweatLupini = new clsFood("Chickpeas (Cicer arietinum)");   64.188995%
        //clsFood SweatLupini = new clsFood("Sweet Lupini (Lupinus albus)");  64.188995%
        //clsFood SweatLupini = new clsFood("Green Peas (Pisum sativum)");//  67.13503 %
        //clsFood SweatLupini = new clsFood("Oats (Avena sativa)");     //    65.66902 %
        clsFood SweatLupini = new clsFood("Egg (whole, hard-boiled)");      //76.14811 %

        Console.WriteLine("Food Name: " + SweatLupini.Name);
        Console.WriteLine("Protein Percentage: " + SweatLupini.Protein_Percentage);
        Console.WriteLine("Similarity Score: " + SweatLupini.Get_Similarity_Score());


        //Addional Features:
        //Highlighting the lack of AAs in the food
        //Generating the best mix of foods to get the missing AAs
    }
}