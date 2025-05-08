using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystemDataAccess
{
    static public class clsIngredientDataAccess
    {
        static public bool GetIngredientByID(int IngredientID, ref string IngredientName, ref int RecentAmount)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Ingredients where IngredientID = @IngredientID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IngredientName = reader["Name"].ToString();
                    RecentAmount = (int)reader["RecentAmount"];
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        static public int AddNewIngredient(string IngredientName, int RecentAmount)
        {
            int IngredientID = -1;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"INSERT INTO [dbo].[Ingredients]
                                   ([Name]
                                   ,[RecentAmount])
                             VALUES
                                   (
	                        		@IngredientName
                                   ,@RecentAmount
	                        	   )
                                    SELECT @@IDENTITY";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IngredientName", IngredientName);
            command.Parameters.AddWithValue("@RecentAmount", RecentAmount);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (int.TryParse(result.ToString(), out int InsertedID))
                {
                    IngredientID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return IngredientID;
        }
        static public bool UpdateIngredient(int IngredientID, string IngredientName, int RecentAmount)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"UPDATE [dbo].[Ingredients]
                             SET [Name] = @IngredientName
                                ,[RecentAmount] = @RecentAmount
                             WHERE IngredientID = @IngredientID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            command.Parameters.AddWithValue("@IngredientName", IngredientName);
            command.Parameters.AddWithValue("@RecentAmount", RecentAmount);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    isUpdated = true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return isUpdated;
        }
        static public bool DeleteIngredient(int IngredientID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"DELETE FROM [dbo].[Ingredients]
                             WHERE IngredientID = @IngredientID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    isDeleted = true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isDeleted;
        }

        static public DataTable GetAllIngredients()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Ingredients";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        static public bool isIngredientExistByName(string IngredientName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Ingredients where Name = @IngredientName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IngredientName", IngredientName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        static public bool isIngredientExistByID(int IngredientID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Ingredients where IngredientID = @IngredientID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        static public bool isIngredientAvailable(int IngredientID, int RequiredAmount)
        {
            bool isAvailable = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Ingredients where IngredientID = @IngredientID and RecentAmount >= @RequiredAmount";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            command.Parameters.AddWithValue("@RequiredAmount", RequiredAmount);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isAvailable = true;
                }
            }
            catch (Exception ex)
            {
                isAvailable = false;
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isAvailable;
        }
        static public bool isIngredientAvailableByName(string IngredientName, int RequiredAmount)
        {
            bool isAvailable = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Ingredients where Name = @IngredientName and RecentAmount >= @RequiredAmount";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IngredientName", IngredientName);
            command.Parameters.AddWithValue("@RequiredAmount", RequiredAmount);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isAvailable = true;
                }
            }
            catch (Exception ex)
            {
                isAvailable = false;
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isAvailable;
        }

        

    }
}
