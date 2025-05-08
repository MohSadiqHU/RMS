using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace RestaurantManagementSystemDataAccess
{
    public class clsPurchaseDataAccess
    {
        public struct PurchaseIngredient
        {
            public int PurchaseID;
            public int IngredientID;
            public int TotalAmount;
            public float TotalPrice;
            public float PriceForEachUnit;
        }
        static public bool GetPurchaseByID(int PurchaseID, ref DateTime PurchaseDate, ref float TotalPrice)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT * FROM Purchase WHERE PurchaseID = @PurchaseID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PurchaseDate = (DateTime)reader["BuyDate"];
                    TotalPrice = (float)reader["TotalPrice"];
                    isFound = true;

                }

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public int AddNewPurchase(DateTime PurchaseDate, float TotalPrice)
        {
            int PurchaseID = -1;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "INSERT INTO Purchases ( BuyDate, TotalPrice) VALUES ( @BuyDate, @TotalPrice) SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@BuyDate", PurchaseDate);
            command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    PurchaseID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return PurchaseID;
        }
        static public bool UpdatePurchase(int PurchaseID, DateTime PurchaseDate, float TotalPrice)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "UPDATE Purchases SET BuyDate = @BuyDate, TotalPrice = @TotalPrice WHERE PurchaseID = @PurchaseID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            command.Parameters.AddWithValue("@BuyDate", PurchaseDate);
            command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                isUpdated = false;
            }
            finally
            {
                connection.Close();
            }
            return isUpdated;
        }
        static public bool DeletePurchase(int PurchaseID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "DELETE FROM Purchase where PurchaseID = @PurchaseID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                isDeleted = false;
            }
            finally
            {
                connection.Close();
            }
            return isDeleted;


        }
        static public DataTable GetAllPurchase()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT * FROM Purchases";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        static public DataTable GetAllPurchaseByDate(DateTime date)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT * FROM Purchases WHERE BuyDate = @BuyDate";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@BuyDate", date);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        static public DataTable GetAllPurchaseIngredientbyPurchaseID(int PurchaseID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT *FROM PurchaseIngredients WHERE PurchaseID = @PurchaseID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("PurchaseID", PurchaseID);
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
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        static public bool AddNewPurchaseIngredient(int PurchaseID, int IngredientID, int TotalAmount, float TotalPrice, float PriceForEachUnit)
        {
            bool isAdded = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "INSERT INTO PurchaseIngredients (PurchaseID, IngredientID, TotalAmount, TotalPrice, PriceForEachUnit) VALUES (@PurchaseID, @IngredientID, @TotalAmount, @TotalPrice, @PriceForEachUnit)";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            command.Parameters.AddWithValue("@TotalAmount", TotalAmount);
            command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
            command.Parameters.AddWithValue("@PriceForEachUnit", PriceForEachUnit);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isAdded = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                isAdded = false;
            }
            finally
            {
                connection.Close();
            }
            return isAdded;

        }
        static public bool UpdatePurchaseIngredient(int PurchaseID, int IngredientID, int TotalAmount, float TotalPrice, float PriceForEachUnit)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "UPDATE PurchaseIngredients SET TotalAmount = @TotalAmount, TotalPrice = @TotalPrice, PriceForEachUnit = @PriceForEachUnit WHERE PurchaseID = @PurchaseID AND IngredientID = @IngredientID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            command.Parameters.AddWithValue("@TotalAmount", TotalAmount);
            command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
            command.Parameters.AddWithValue("@PriceForEachUnit", PriceForEachUnit);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                isUpdated = false;
            }
            finally
            {
                connection.Close();
            }
            return isUpdated;
        }
        static public bool DeletePurchaseIngredient(int PurchaseID, int IngredientID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "DELETE FROM PurchaseIngredients WHERE PurchaseID = @PurchaseID AND IngredientID = @IngredientID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                isDeleted = false;
            }
            finally
            {
                connection.Close();
            }
            return isDeleted;
        }

        static public List<PurchaseIngredient> GetPurchaseIngredients(int PurchaseID)
        {
            List<PurchaseIngredient> purchaseIngredients = new List<PurchaseIngredient>();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT * FROM PurchaseIngredients WHERE PurchaseID = @PurchaseID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PurchaseIngredient purchaseIngredient = new PurchaseIngredient
                    {
                        PurchaseID = (int)reader["PurchaseID"],
                        IngredientID = (int)reader["IngredientID"],
                        TotalAmount = (int)reader["TotalAmount"],
                        TotalPrice = (float)reader["TotalPrice"],
                        PriceForEachUnit = (float)reader["PriceForEachUnit"]
                    };
                    purchaseIngredients.Add(purchaseIngredient);
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return purchaseIngredients;
        }
        static public bool DeletePurchaseIngredients(int PurchaseID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "DELETE FROM PurchaseIngredients WHERE PurchaseID = @PurchaseID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                isDeleted = false;
            }
            finally
            {
                connection.Close();
            }
            return isDeleted;
        }
    }
}
