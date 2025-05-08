using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystemDataAccess;


namespace RestaurantManagementSystemBusiness
{
    public class clsOrderProduct
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        public int TotalAmount { get; set; }
        public float TotalPrice { get; set; }
        public clsOrderProduct(int orderID, int productID, int totalAmount, float totalPrice)
        {
            OrderID = orderID;
            ProductID = productID;
            TotalAmount = totalAmount;
            TotalPrice = totalPrice;
        }

        public clsOrderProduct()
        {
            OrderID = -1;
            ProductID = -1;
            TotalAmount = 0;
            TotalPrice = 0;
        }

        public bool AddNewOrderProduct()
        {
            return clsOrdersDataAccess.AddNewOrderProduct(OrderID, ProductID, TotalAmount, TotalPrice);
        }
        public static DataTable GetAllOrderProducts(int orderID)
        {
            return clsOrdersDataAccess.GetAllOrderProducts(orderID);
        }

        public static List<clsOrderProduct> GetAllOrderProductsInList(int ID)
        {
            DataTable dt = clsOrdersDataAccess.GetAllOrderProducts(ID);
            List<clsOrderProduct> orderProducts = new List<clsOrderProduct>();
            foreach (DataRow row in dt.Rows)
            {
                orderProducts.Add(new clsOrderProduct(
                    Convert.ToInt32(row["OrderID"]),
                    Convert.ToInt32(row["ProductID"]),
                    Convert.ToInt32(row["TotalAmount"]),
                    Convert.ToSingle(row["TotalPrice"])
                ));
            }
            return orderProducts;
        }

        

        public static clsOrderProduct _SetOrderProduct(clsProduct product, int TotalAmount,int orderID)
        {
            clsOrderProduct orderProduct = new clsOrderProduct();
            orderProduct.ProductID = product.ProductID;
            orderProduct.OrderID = orderID;
            orderProduct.TotalAmount = TotalAmount;
            orderProduct.TotalPrice = TotalAmount * product.Price;
            return orderProduct;
        }



        
    }
}
