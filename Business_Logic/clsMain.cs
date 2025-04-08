using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    public class clsMain
    {
        public float[] WhatIsTheBestMix(clsFood[] FoodsList)
        {
            //Initilize the return list
            float[] Amounts = new float[FoodsList.Length];

            //Calculate the similarity score of each element
            float[] Similarity_Scores = new float[FoodsList.Length];

            for(int i = 0; i < FoodsList.Length; i++)
            {
                Similarity_Scores[i] = FoodsList[i].Get_Similarity_Score();
            }

            //Fiugre out the right amount of each element
            

            //Return Amounts List
            return Amounts;
        }
    }
}
