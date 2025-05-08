using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RestaurantManagementSystemDataAccess
{
    static public class clsCategoriesDataAccess
    {
        public static int AddNewCategory(string CategoryName)
        {
            int CategoryID = -1;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"INSERT INTO Categories 
                             values (@CategoryName) 
                              SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Qurey,connection);
            command.Parameters.AddWithValue("@CategoryName", CategoryName);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    CategoryID = InsertedID;
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

            return CategoryID;

            
        }

        public static List<string> GetAllCategories()
        {
            List<string> Categories = new List<string>();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT CategoryName FROM Categories";
            SqlCommand command = new SqlCommand(Qurey, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Categories.Add(reader["CategoryName"].ToString());
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
            return Categories;
        }

        public static DataTable GetAllCategoriesWithID()
        {
            DataTable Categories = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT * FROM Categories";
            SqlCommand command = new SqlCommand(Qurey, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Categories.Load(reader);
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return Categories;
        }

        public static bool GetCategoryByID(int CategoryID, out string CategoryName)
        {
            bool IsFound = false;
            CategoryName = string.Empty;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT CategoryName FROM Categories WHERE CategoryID=@CategoryID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    CategoryName = reader["CategoryName"].ToString();
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
            return IsFound;
        }

        public static bool UpdateCategory(int CategoryID, string CategoryName)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"UPDATE Categories SET CategoryName=@CategoryName WHERE CategoryID=@CategoryID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            command.Parameters.AddWithValue("@CategoryName", CategoryName);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    IsUpdated = true;
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
            return IsUpdated;
        }

        public static bool DeleteCategory(int CategoryID)
        {
            bool IsDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"DELETE FROM Categories WHERE CategoryID=@CategoryID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    IsDeleted = true;
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
            return IsDeleted;
        }

        public static bool IsCategoryNameExists(string CategoryName)
        {
            bool IsExists = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT COUNT(*) FROM Categories WHERE CategoryName=@CategoryName";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@CategoryName", CategoryName);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    IsExists = true;
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
            return IsExists;
        }

        public static bool IsCategoryIDExists(int CategoryID)
        {
            bool IsExists = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT COUNT(*) FROM Categories WHERE CategoryID=@CategoryID";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    IsExists = true;
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
            return IsExists;
        }

        public static int GetCategoryID(string CategoryName)
        {
            int CategoryID = 0;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"SELECT CategoryID FROM Categories WHERE CategoryName=@CategoryName";
            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@CategoryName", CategoryName);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                  CategoryID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                CategoryID = -1;
            }
            finally
            {
                connection.Close();
            }
            return CategoryID;
        }


    }
}
