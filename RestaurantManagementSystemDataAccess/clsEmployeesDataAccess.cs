using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RestaurantManagementSystemDataAccess
{
    static public class clsEmployeesDataAccess
    {
        // This class will contain methods to interact with the database for employee-related operations.
        // For example, methods to add, update, delete, and retrieve employee records.
        // The actual implementation will depend on the specific database technology being used (e.g., SQL Server, MySQL, etc.).
        // Here is a placeholder for the class definition.

        static public bool GetPhonesByEmployeeID(int EmployeeID, ref string[] Phones)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "select phoneNumber from PhonesEmployee where EmployeeID = @EmployeeID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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

        static public bool GetEmployeeByID(int EmployeeID, ref string FirstName, ref string MidName, ref string LastName, ref string Position, ref string Email, ref string[] Phones, ref float Salary)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);

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
                    Position = reader["Position"].ToString();
                    Salary = float.Parse (reader["Salary"].ToString());

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
            GetPhonesByEmployeeID(EmployeeID, ref Phones);

            return isFound;

        }

        static public int AddNewEmployee( string FirstName, string MidName, string LastName, string Position, string Email, string[] Phones, float Salary)
        {
            int EmployeeID = -1;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"INSERT INTO [dbo].[Employees]
                               ([FirstName]
                               ,[MidName]
                               ,[LastName]
                               ,[Email]
                               ,[Position]
                               ,[Salary])
                         VALUES
                               (@FirstName
                               ,@MidName
                               ,@LastName
                               ,@Email
                               ,@Position
                               ,@Salary);
                                SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@MidName", MidName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Position", Position);
            command.Parameters.AddWithValue("@Salary", Salary);
           
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    EmployeeID = insertedID;
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
            AddPhoneNumbers(EmployeeID, Phones);
            return EmployeeID;
        }

        static public bool AddPhoneNumbers(int EmployeeID, string[] Phones)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            
            try
            {
                connection.Open();
                foreach (string phone in Phones)
                {
                    string Query = "INSERT INTO PhonesEmployee (EmployeeID, phoneNumber) VALUES (@EmployeeID, @PhoneNumber)";
                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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

        static public bool UpdateEmployee(int EmployeeID, string FirstName, string MidName, string LastName, string Position, string Email, float Salary, string[] Phones)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"UPDATE Employees
                             SET FirstName = @FirstName,
                                 MidName = @MidName,
                                 LastName = @LastName,
                                 Position = @Position,
                                 Email = @Email,
                                 Salary = @Salary
                             WHERE EmployeeID = @EmployeeID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@MidName", MidName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Position", Position);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Salary", Salary);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isUpdated = (rowsAffected > 0 && UpdatePhoneNumbers(EmployeeID,Phones)) ;
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
        static public bool DeleteEmployee(int EmployeeID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            try
            {
                
                connection.Open();
                if(DeletePhoneNumbers(EmployeeID))
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
        static public bool DeletePhoneNumber(int EmployeeID, string PhoneNumber)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "DELETE FROM PhonesEmployee WHERE EmployeeID = @EmployeeID AND PhoneNumber = @PhoneNumber";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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

        static public bool DeletePhoneNumbers(int EmployeeID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "DELETE FROM PhonesEmployee WHERE EmployeeID = @EmployeeID ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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

        static public bool UpdatePhoneNumbers(int EmployeeID, string[] Phones)
        {
            if (DeletePhoneNumbers(EmployeeID))
            {
                if(AddPhoneNumbers(EmployeeID, Phones));
                return true;
            }

            return false;
        }

        static public DataTable GetAllEmployees()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT * FROM AllEmployeesWithOnlyOnePhone";
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

        static public bool isEmployeeExist(int EmployeeID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @EmployeeID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
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
