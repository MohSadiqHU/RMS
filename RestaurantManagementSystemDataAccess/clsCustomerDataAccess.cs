using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantManagementSystemDataAccess
{
    static public class clsCustomerDataAccess
    {
        static public bool GetPhonesByCustomerID(int CustomerID, ref string[] Phones)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "select phoneNumber from PhonesCustomer where CustomerID = @CustomerID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<string> phoneList = new List<string>();
                while (reader.Read())
                {
                    phoneList.Add(reader["phoneNumber"].ToString());
                }
                Phones = phoneList.ToArray();
                IsFound = true;
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }
            return IsFound;
        }

        static public bool GetCustomerByID(int CustomerID, ref string FirstName, 
            ref string MidName, ref string LastName, ref string Address, 
            ref string Email, ref string[] Phones)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    FirstName = reader["FirstName"].ToString();
                    MidName = reader["MidName"].ToString();
                    LastName = reader["LastName"].ToString();
                    Email = reader["Email"].ToString();
                    Address = reader["Address"].ToString();

                }
                isFound = true;
                reader.Close();

            }
            catch (Exception ex)
            {
                isFound = false;
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            GetPhonesByCustomerID(CustomerID, ref Phones);

            return isFound;

        }

        static public int AddNewCustomer(string FirstName, string MidName, string LastName, string Address, string Email, string[] Phones)
        {
            int CustomerID = -1;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"INSERT INTO [dbo].[Customers]
                               ([FirstName]
                               ,[MidName]
                               ,[LastName]
                               ,[Email]
                               ,[Address])
                         VALUES
                               (@FirstName
                               ,@MidName
                               ,@LastName
                               ,@Email
                               ,@Address);
                                SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@MidName", MidName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Address", Address);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    CustomerID = insertedID;
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
            AddPhoneNumbers(CustomerID, Phones);
            return CustomerID;
        }

        static public bool AddPhoneNumbers(int CustomerID, string[] Phones)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);

            try
            {
                connection.Open();
                foreach (string phone in Phones)
                {
                    string Query = "INSERT INTO PhonesCustomer (CustomerID, phoneNumber) VALUES (@CustomerID, @PhoneNumber)";
                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);
                    command.Parameters.AddWithValue("@PhoneNumber", phone);
                    command.ExecuteNonQuery();
                }
                result = true;
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                result = false;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        static public bool UpdateCustomer(int CustomerID, string FirstName, string MidName, string LastName, string Address, string Email, string[] Phones)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"UPDATE Customers
                             SET FirstName = @FirstName,
                                 MidName = @MidName,
                                 LastName = @LastName,
                                 Address = @Address,
                                 Email = @Email,
                             WHERE CustomerID = @CustomerID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@MidName", MidName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isUpdated = (rowsAffected > 0 && UpdatePhoneNumbers(CustomerID, Phones));
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
        static public bool DeleteCustomer(int CustomerID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            try
            {

                connection.Open();
                if (DeletePhoneNumbers(CustomerID))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    isDeleted = rowsAffected > 0;
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
        static public bool DeletePhoneNumber(int CustomerID, string PhoneNumber)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "DELETE FROM PhonesCustomer WHERE CustomerID = @CustomerID AND PhoneNumber = @PhoneNumber";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
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

        static public bool DeletePhoneNumbers(int CustomerID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "DELETE FROM PhonesCustomer WHERE CustomerID = @CustomerID ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
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

        static public bool UpdatePhoneNumbers(int CustomerID, string[] Phones)
        {
            if (DeletePhoneNumbers(CustomerID))
            {
                if (AddPhoneNumbers(CustomerID, Phones)) ;
                return true;
            }

            return false;
        }

        static public DataTable GetAllCustomers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT * FROM AllCustomersWithOnlyOnePhone";
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

        static public bool isCustomerExist(int CustomerID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT COUNT(*) FROM Customers WHERE CustomerID = @CustomerID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                isFound = count > 0;
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
    }
}
