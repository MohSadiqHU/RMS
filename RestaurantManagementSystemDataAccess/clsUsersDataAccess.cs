using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystemDataAccess
{
    public class clsUsersDataAccess
    {
        static public bool GetUserByID(int UserID, ref string UserName, ref int Permission, ref int EmployeeID, ref string PasswordHash, ref string PasswordSalt)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserName = reader["UserName"].ToString();

                    Permission = (int)reader["Permission"];
                    EmployeeID = (int)reader["EmployeeID"];
                    PasswordHash = (string)reader["PasswordHash"];
                    PasswordSalt = (string)reader["PasswordSalt"];
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                ////Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool GetUserByName(string UserName, ref int UserID, ref string PasswordHash, ref string PasswordSalt, ref int Permission, ref int EmployeeID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Users where UserName = @UserName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    PasswordHash = reader["PasswordHash"].ToString();
                    PasswordSalt = reader["PasswordSalt"].ToString();
                    Permission = (int)reader["Permission"];
                    EmployeeID = (int)reader["EmloyeeID"];
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                ////Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        //static public bool GetUserByUserNameandPassWord(string UserName, ref int UserID, ref int Permission, ref int EmployeeID)
        //{
        //    bool isFound = false;
        //    SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
        //    string Query = @"SELECT * FROM Users where UserName = @UserName ";
        //    SqlCommand command = new SqlCommand(Query, connection);
        //    command.Parameters.AddWithValue("@UserName", UserName);

        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            UserID = (int)reader["UserID"];
        //            Permission = (int)reader["Permission"];
        //            EmployeeID = (int)reader["EmloyeeID"];
        //            isFound = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine(ex.Message);
        //        isFound = false;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return isFound;
        //}

        static public bool GetPasswordHashAndSaltByID(int UserID, ref string PasswordHash, ref string PasswordSalt)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"Select PasswordHash, PasswordSalt FROM Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PasswordHash = reader["PasswordHash"].ToString();
                    PasswordSalt = reader["PasswordSalt"].ToString();
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }
        static public bool GetPasswordHashAndSaltByUserName(string UserName, ref string PasswordHash, ref string PasswordSalt)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"Select PasswordHash, PasswordSalt FROM Users where UserName = @UserName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PasswordHash = reader["PasswordHash"].ToString();
                    PasswordSalt = reader["PasswordSalt"].ToString();
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public int AddNewUser(string UserName, string PasswordHash, string PasswordSalt, int Permission, int EmployeeID)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"INSERT INTO Users (UserName, PasswordHash, PasswordSalt, Permission, EmployeeID) 
                            VALUES (@UserName, @PasswordHash, @PasswordSalt, @Permission, @EmployeeID) 
                                SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            command.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            command.Parameters.AddWithValue("@Permission", Permission);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    UserID = InsertedID;
                }

            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return UserID;
        }
        static public bool DeleteUser(int UserID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"DELETE FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
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
                //Console.WriteLine(ex.Message);
                isDeleted = false;
            }
            finally
            {
                connection.Close();
            }
            return isDeleted;
        }
        static public bool UpdateUser(int UserID, string UserName, string PasswordHash, string PasswordSalt, int Permission, int EmployeeID)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"UPDATE Users SET UserName = @UserName, PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt , Permission = @Permission, EmployeeID = @EmployeeID WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            command.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            command.Parameters.AddWithValue("@Permission", Permission);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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
                //Console.WriteLine(ex.Message);
                isUpdated = false;
            }
            finally
            {
                connection.Close();
            }
            return isUpdated;
        }
        static public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Users";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
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
        static public bool isUserExist(int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public bool isUserExist(string UserName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Users where UserName = @UserName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        static public bool isUserExist(string UserName, string Password)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Users where UserName = @UserName and Password = @Password";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

    }
}
