using System;
using Microsoft.Data.SqlClient;

namespace Data_Access
{
    public class clsFoodData
    {
        public static bool Get_AAs_Profile(string FoodName,
                                  ref float Alanine,
                                  ref float Arginine,
                                  ref float Asparagine,
                                  ref float Aspartic_Acid,
                                  ref float Cysteine,
                                  ref float Glutamic_Acid,
                                  ref float Glutamine,
                                  ref float Glycine,
                                  ref float Histidine,
                                  ref float Isoleucine,
                                  ref float Leucine,
                                  ref float Lysine,
                                  ref float Methionine,
                                  ref float Phenylalanine,
                                  ref float Proline,
                                  ref float Serine,
                                  ref float Selenocysteine,
                                  ref float Threonine,
                                  ref float Tryptophan,
                                  ref float Tyrosine,
                                  ref float Valine)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT
                                Food.Name AS FoodName,
                                AAs_Profile.*
                            FROM 
                                Food Food
                            INNER JOIN 
                                AAs_Profile AAs_Profile ON Food.AAs_Profile_ID = AAs_Profile.ID
                            Where Food.Name = @Name;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", FoodName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    Alanine = reader["Alanine"] != DBNull.Value ? Convert.ToSingle(reader["Alanine"]) : 0f;
                    Arginine = reader["Arginine"] != DBNull.Value ? Convert.ToSingle(reader["Arginine"]) : 0f;
                    Asparagine = reader["Asparagine"] != DBNull.Value ? Convert.ToSingle(reader["Asparagine"]) : 0f;
                    Aspartic_Acid = reader["Aspartic_Acid"] != DBNull.Value ? Convert.ToSingle(reader["Aspartic_Acid"]) : 0f;
                    Cysteine = reader["Cysteine"] != DBNull.Value ? Convert.ToSingle(reader["Cysteine"]) : 0f;
                    Glutamic_Acid = reader["Glutamic_Acid"] != DBNull.Value ? Convert.ToSingle(reader["Glutamic_Acid"]) : 0f;
                    Glutamine = reader["Glutamine"] != DBNull.Value ? Convert.ToSingle(reader["Glutamine"]) : 0f;
                    Glycine = reader["Glycine"] != DBNull.Value ? Convert.ToSingle(reader["Glycine"]) : 0f;
                    Histidine = reader["Histidine"] != DBNull.Value ? Convert.ToSingle(reader["Histidine"]) : 0f;
                    Isoleucine = reader["Isoleucine"] != DBNull.Value ? Convert.ToSingle(reader["Isoleucine"]) : 0f;
                    Leucine = reader["Leucine"] != DBNull.Value ? Convert.ToSingle(reader["Leucine"]) : 0f;
                    Lysine = reader["Lysine"] != DBNull.Value ? Convert.ToSingle(reader["Lysine"]) : 0f;
                    Methionine = reader["Methionine"] != DBNull.Value ? Convert.ToSingle(reader["Methionine"]) : 0f;
                    Phenylalanine = reader["Phenylalanine"] != DBNull.Value ? Convert.ToSingle(reader["Phenylalanine"]) : 0f;
                    Proline = reader["Proline"] != DBNull.Value ? Convert.ToSingle(reader["Proline"]) : 0f;
                    Serine = reader["Serine"] != DBNull.Value ? Convert.ToSingle(reader["Serine"]) : 0f;
                    Selenocysteine = reader["Selenocysteine"] != DBNull.Value ? Convert.ToSingle(reader["Selenocysteine"]) : 0f;
                    Threonine = reader["Threonine"] != DBNull.Value ? Convert.ToSingle(reader["Threonine"]) : 0f;
                    Tryptophan = reader["Tryptophan"] != DBNull.Value ? Convert.ToSingle(reader["Tryptophan"]) : 0f;
                    Tyrosine = reader["Tyrosine"] != DBNull.Value ? Convert.ToSingle(reader["Tyrosine"]) : 0f;
                    Valine = reader["Valine"] != DBNull.Value ? Convert.ToSingle(reader["Valine"]) : 0f;
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static float Get_Protein_Percentage(string FoodName)
        {
            float proteinPercentage = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Protein_Percentage
                            FROM Food
                            WHERE Name = @Name;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", FoodName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    object rawValue = reader.GetValue(0);
                    float floatValue;

                    if (rawValue is decimal decimalValue)
                        floatValue = (float)decimalValue;
                    else
                        floatValue = Convert.ToSingle(rawValue);


                    proteinPercentage = floatValue;
                    
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return proteinPercentage;
        }
    }
}