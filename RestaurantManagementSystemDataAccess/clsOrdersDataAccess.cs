using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Policy;
using System.Runtime.InteropServices;


namespace RestaurantManagementSystemDataAccess
{
    static public class clsOrdersDataAccess
    {
        public struct stOrderProducts
        {
            public int OrderID;
            public int ProductID;
            public int TotalAmount;
            public float TotaPrice;
        }
        static public int AddNewOrder(int CustomerID, DateTime OrderDate, float TotalPrice, int EmployeeID)
        {
            SqlConnection connection = new  SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Qurey = @"INSERT INTO Orders (CustomerID, OrderDate, TotalPrice, EmployeeID) 
                             values (@CustomerID, @OrderDate, @TotalPrice, @EmployeeID) 
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Qurey, connection);
            command.Parameters.AddWithValue("@OrderDate", OrderDate);
            command.Parameters.AddWithValue("@TotalPrice", TotalPrice);

            if (EmployeeID > 0)
            {
                command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            }
            else
            {
                command.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
            }

            if (CustomerID > 0)
            {
                command.Parameters.AddWithValue("@CustomerID", CustomerID);
            }
            else
            {
                command.Parameters.AddWithValue("@CustomerID", DBNull.Value);
            }

            int OrderID = -1;

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    OrderID = InsertedID;
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
            return OrderID;
        }

        static public bool AddOrderProducts(List<stOrderProducts> orderProducts)
        {
            bool result = false;
            SqlConnection connection = new  SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            try
            {
                connection.Open();
                foreach (stOrderProducts item in orderProducts) {
                    string Query = @"INSERT INTO [dbo].[OrderProducts]
                            ([OrderID]
                           ,[ProductID]
                           ,[TotalAmount]
                           ,[TotalPrice])
                     VALUES
                           (@OrderID
                           ,@ProductID
                           ,@TotalAmount
                           ,@TotalPrice)";
                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@OrderID", item.OrderID);
                    command.Parameters.AddWithValue("@ProductID", item.ProductID);
                    command.Parameters.AddWithValue("@TotalAmount", item.TotalAmount);
                    command.Parameters.AddWithValue("@TotalPrice", item.TotaPrice);
                    command.ExecuteNonQuery();
                }
                result = true;
            }
            catch(Exception ex) 
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }
            return result;

        }

        static public bool AddNewOrderProduct(int OrderID, int ProductID, int TotalAmount, float TotalPrice)
        {
            bool result = false;
            SqlConnection connection = new  SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"INSERT INTO [dbo].[OrderProducts]
                           ([OrderID]
                           ,[ProductID]
                           ,[TotalAmount]
                           ,[TotalPrice])
                     VALUES
                           (@OrderID
                           ,@ProductID
                           ,@TotalAmount
                           ,@TotalPrice)";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@OrderID", OrderID);
            command.Parameters.AddWithValue("@ProductID", ProductID);
            command.Parameters.AddWithValue("@TotalAmount", TotalAmount);
            command.Parameters.AddWithValue("@TotalPrice", TotalPrice);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally 
            { 
                connection.Close();
            }
            return result;
        }

        static public DataTable GetAllOrderProducts(int OrderID)
        {
            DataTable OrderProducts = new DataTable();
            SqlConnection connection = new  SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM OrderProducts where OrderID = @OrderID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@OrderID", OrderID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                OrderProducts.Load(reader);

            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return OrderProducts;
        }

        static public DataTable GetAllOrders()
        {
            DataTable Orders = new DataTable();
            SqlConnection connection = new  SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Orderes";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Orders.Load(reader);
            }
            catch(Exception ex) { }
            finally 
            { 
                connection.Close(); 
            }
            return Orders;

        }

        static public bool GetOrderByID(int OrderID, ref int CustomerID, ref int EmployeeID, ref DateTime OrderDate, ref float TotalPrice)
        {
            bool OrderFound = false;
            SqlConnection connection = new  SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"SELECT * FROM Orders WHERE OrderID = @OrderID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@OrderID", OrderID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["CustomerID"] != DBNull.Value)
                    {
                        CustomerID = (int)reader["CustomerID"];
                    }
                    else
                    {
                        CustomerID = 0;
                    }
                    OrderDate = (DateTime)reader["OrderDate"];
                    TotalPrice = (float)reader["TotalPrice"];
                    if (reader["EmployeeID"] != DBNull.Value)
                    {
                        EmployeeID = (int)reader["EmployeeID"];
                    }
                    else
                    {
                        EmployeeID = 0;
                    }
                    OrderFound = true;
                }
            }
            catch (Exception ex) 
            { 
                OrderFound = false; 
            }
            finally 
            {
                connection.Close(); 
            }
            return OrderFound;
        }

        static public bool UpdateOrder(int OrderID, int CustomerID, DateTime OrderDate, float TotalPrice, int EmployeeID)
        {
            bool result = false;
            SqlConnection connection = new  SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"UPDATE Orders 
                             SET CustomerID = @CustomerID, 
                                 OrderDate = @OrderDate, 
                                 TotalPrice = @TotalPrice, 
                                 EmployeeID = @EmployeeID
                             WHERE OrderID = @OrderID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@OrderDate", OrderDate);
            command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
            command.Parameters.AddWithValue("@OrderID", OrderID);
            if (EmployeeID > 0)
            {
                command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            }
            else
            {
                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = DBNull.Value;

            }

            if (CustomerID > 0)
            {
                command.Parameters.AddWithValue("@CustomerID", CustomerID);
            }
            else
            {
                command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = DBNull.Value;

            }

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex) { result = false; }
            finally { connection.Close(); }
            return result;
        }

        static public bool DeleteOrderProducts(int OrderID)
        {
            bool result = false;
            SqlConnection connection = new  SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"Delete from OrderProducts where OrderID = @OrderID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@OrderID", OrderID);
            try
            {
                connection.Open(); 
                int rowEffected = command.ExecuteNonQuery();
                result = (rowEffected > 0);
            }
            catch(Exception ex) { result = false; }
            finally
            {
                connection.Close();
            }

            return false;
           
        }

        static public bool UpdateOrderProducts(int OrderID, List<stOrderProducts> orderProducts)
        {
            if(DeleteOrderProducts(OrderID))
            {
                return AddOrderProducts(orderProducts);
            }
            else
            {
                return false;
            }
           
        }

        public static bool UpdateOrder(int OrderID, int CustomerID,  int EmployeeID, float TotalPrice, DateTime OrderDate)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsResturantDataAccessSittings.ConnectionString);
            string Query = @"UPDATE Orders 
                             SET CustomerID = @CustomerID, 
                                 OrderDate = @OrderDate, 
                                 TotalPrice = @TotalPrice, 
                                 EmployeeID = @EmployeeID
                             WHERE OrderID = @OrderID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            command.Parameters.AddWithValue("@OrderDate", OrderDate);
            command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            command.Parameters.AddWithValue("@OrderID", OrderID);
            try
            {
                connection.Open();
                int RowEffected = command.ExecuteNonQuery();
                result = RowEffected > 0;

            }

            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}
