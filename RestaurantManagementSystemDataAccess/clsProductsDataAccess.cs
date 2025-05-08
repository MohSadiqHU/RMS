using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RestaurantManagementSystemDataAccess
{
    static public class clsProductsDataAccess
    {
        public static int AddNewProduct(string ProductName, float Price, int CategoryID)
        {
            int ProductID = -1;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"INSERT INTO Products 
                             values (@ProductName, @Price, @CategoryID) 
                              SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@ProductName", ProductName);
            command.Parameters.AddWithValue("@Price", Price);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ProductID = InsertedID;
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
            return ProductID;
        }

        public static DataTable GetAllProducts()
        {
            DataTable Products = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT p.ProductID, p.ProductName, p.Price, c.CategoryName 
                             FROM Products p 
                             JOIN Categories c ON p.CategoryID = c.CategoryID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(Products);
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return Products;
        }

        public static bool GetProductByID(int ProductID , ref string ProductName, 
            ref float Price, ref string CategoryName)
        {
            bool ProductFound = false;
             
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT p.ProductID, p.ProductName, p.Price, c.CategoryName 
                             FROM Products p 
                             JOIN Categories c ON p.CategoryID = c.CategoryID
                             Where ProductID = @ProductID;";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@ProductID", ProductID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ProductName = reader["ProductName"].ToString();
                    Price = float.Parse(reader["Price"].ToString());
                    CategoryName = reader["CategoryName"].ToString();

                    ProductFound = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                ProductFound = false;
            }
            finally
            {
                connection.Close();
            }
            return ProductFound;
        }

        public static bool UpdateProduct(int ProductID, string ProductName, float Price, int CategoryID)
        {
            bool ProductUpdated = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"UPDATE Products 
                             SET ProductName = @ProductName, Price = @Price, CategoryID = @CategoryID 
                             WHERE ProductID = @ProductID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@ProductName", ProductName);
            command.Parameters.AddWithValue("@Price", Price);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            command.Parameters.AddWithValue("@ProductID", ProductID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    ProductUpdated = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                ProductUpdated = false;
            }
            finally
            {
                connection.Close();
            }
            return ProductUpdated;
        }

        public static bool DeleteProduct(int ProductID)
        {
            bool ProductDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"DELETE FROM Products 
                             WHERE ProductID = @ProductID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@ProductID", ProductID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    ProductDeleted = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                ProductDeleted = false;
            }
            finally
            {
                connection.Close();
            }

            return ProductDeleted;
        }

        public static DataTable GetProductsByCategoryID(int CategoryID)
        {
            DataTable Products = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT p.ProductID, p.ProductName, p.Price, c.CategoryName 
                             FROM Products p 
                             JOIN Categories c ON p.CategoryID = c.CategoryID
                             WHERE p.CategoryID = @CategoryID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(Products);
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return Products;
        }

        public static bool AddNewProductIngredient(int ProductID, int IngredientID, int TotalAmountConsumed, DateTime TheDateConsumed)
        {
            bool ProductIngredientAdded = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"INSERT INTO ProductIngredients 
                             values (@ProductID, @IngredientID, @TotalAmountConsumed, @TheDateConsumed)";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@ProductID", ProductID);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            command.Parameters.AddWithValue("@TotalAmountConsumed", TotalAmountConsumed);
            command.Parameters.AddWithValue("@TheDateConsumed", TheDateConsumed);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    ProductIngredientAdded = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                ProductIngredientAdded = false;
            }
            finally
            {
                connection.Close();
            }
            return ProductIngredientAdded;
        }
        public static bool UpdateProductIngredient(int ProductID, int IngredientID, int TotalAmountConsumed, DateTime TheDateConsumed)
        {
            bool ProductIngredientUpdated = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"UPDATE ProductIngredients 
                             SET TotalAmountConsumed = @TotalAmountConsumed, TheDateConsumed = @TheDateConsumed 
                             WHERE ProductID = @ProductID AND IngredientID = @IngredientID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@ProductID", ProductID);
            command.Parameters.AddWithValue("@IngredientID", IngredientID);
            command.Parameters.AddWithValue("@TotalAmountConsumed", TotalAmountConsumed);
            command.Parameters.AddWithValue("@TheDateConsumed", TheDateConsumed);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    ProductIngredientUpdated = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                ProductIngredientUpdated = false;
            }
            finally
            {
                connection.Close();
            }
            return ProductIngredientUpdated;
        }
        public static bool DeleteProductIngredient(int ProductID, int IngredientID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"DELETE FROM ProductIngredients 
                     WHERE ProductID = @ProductID AND IngredientID = @IngredientID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ProductID", ProductID);
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

        public static bool DeleteProductIngredient(int ProductID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"DELETE FROM ProductIngredients 
                 WHERE ProductID = @ProductID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ProductID", ProductID);
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

        public static DataTable GetAllProductIngredients()
        {
            DataTable ProductIngredients = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT Distinct p.ProductID, p.ProductName, i.IngredientID, i.Name
                             FROM ProductIngredients pi 
                             JOIN Products p ON pi.ProductID = p.ProductID 
                             JOIN Ingredients i ON pi.IngredientID = i.IngredientID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ProductIngredients);
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ProductIngredients;
        }
        public static DataTable GetAllProductIngredients(int ProductID)
        {
            DataTable ProductIngredients = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT pi.ProductID, pi.IngredientID, i.Name
                             FROM ProductIngredients pi 
                             JOIN Ingredients i ON pi.IngredientID = i.IngredientID
                             WHERE pi.ProductID = @ProductID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@ProductID", ProductID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    ProductIngredients.Load(reader);

            }
            catch (Exception ex) 
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ProductIngredients;
        }

        public static bool isProductExistsByName(string ProductName)
        {
            bool IsProductExists = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT COUNT(*) 
                             FROM Products 
                             WHERE ProductName = @ProductName";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@ProductName", ProductName);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    IsProductExists = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                IsProductExists = false;
            }
            finally
            {
                connection.Close();
            }
            return IsProductExists;
        }

    }
}
