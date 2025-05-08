using RestaurantManagementSystemDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace RestaurantManagementSystemBusiness
{
    public class clsOrder
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public float TotalPrice { get; private set; }
        public DateTime OrderDate { get; set; }

        private List<clsOrderProduct> _OrderProducts = new List<clsOrderProduct> ();

        public List<clsOrderProduct> OrderProducts { get { return _OrderProducts; } }
        enum enMode { AddNew = 1, SetOrderProducts, Update };
        enMode Mode = enMode.AddNew;
        private clsOrder(int orderID, int customerID, int employeeID, float totalPrice, DateTime orderDate, List<clsOrderProduct> orderProducts)
        {
            OrderID = orderID;
            CustomerID = customerID;
            EmployeeID = employeeID;
            TotalPrice = totalPrice;
            OrderDate = orderDate;
            Mode = enMode.Update;
            _OrderProducts = orderProducts;

        }
        public clsOrder()
        {
            OrderID = -1;
            CustomerID = -1;
            EmployeeID = -1;
            TotalPrice = 0;
            OrderDate = DateTime.Now;
            Mode = enMode.AddNew;
        }

        private bool _AddNewOrder()
        {
            OrderID = clsOrdersDataAccess.AddNewOrder(CustomerID, OrderDate, TotalPrice, EmployeeID);
            //foreach (clsOrderProduct product in OrderProducts)
            //{
            //    clsOrdersDataAccess.AddNewOrderProduct(product.OrderID, product.ProductID, product.TotalAmount, product.TotalPrice);
            //}
            return OrderID != -1;
        }

        private clsOrderProduct _SetOrderProduct(clsProduct product, int TotalAmount)
        {
            clsOrderProduct orderProduct = new clsOrderProduct();
            orderProduct.ProductID = product.ProductID;
            orderProduct.OrderID = this.OrderID;
            orderProduct.TotalAmount = TotalAmount;
            orderProduct.TotalPrice = TotalAmount * product.Price;
            return orderProduct;
        }

        public bool AddProductToOrder(clsOrderProduct orderProduct)
        {
            if (orderProduct != null)
            {
                _OrderProducts.Add(orderProduct);
                return true;
            }
            return false;
        }

        public bool AddProductToOrder(clsProduct product, int TotalAmount)
        {
            return AddProductToOrder(_SetOrderProduct(product, TotalAmount));
        }

        private bool _UpdateOrder()
        {
            if (clsOrdersDataAccess.UpdateOrder(OrderID, CustomerID, OrderDate, TotalPrice, EmployeeID))
                return clsOrdersDataAccess.UpdateOrderProducts(OrderID, ConvertOrderProductListToStruct());
            else
                return false;
        }

        public void UpdateTotalPrice()
        {
            float totalPrice = 0;
            foreach (clsOrderProduct orderProduct in OrderProducts)
            {
                totalPrice += orderProduct.TotalPrice;
            }
            TotalPrice = totalPrice;
            // Add methods for adding and updating orders here
        }

        public static clsOrder Find(int orderID)
        {
            int customerID = -1;
            int employeeID = -1;
            float totalPrice = 0;
            DateTime orderDate = DateTime.Now;
            List<clsOrderProduct> orderProducts = new List<clsOrderProduct>();
            clsOrdersDataAccess.GetOrderByID(orderID, ref customerID, ref employeeID, ref orderDate, ref totalPrice);
            orderProducts = clsOrderProduct.GetAllOrderProductsInList(orderID);
            return new clsOrder(orderID, customerID, employeeID, totalPrice, orderDate, orderProducts);
        }

        public static DataTable GetAllOrders()
        {
            return clsOrdersDataAccess.GetAllOrders();
        }

        private List<clsOrdersDataAccess.stOrderProducts> ConvertOrderProductListToStruct()
        {
            List<clsOrdersDataAccess.stOrderProducts> stOrderProducts = new List<clsOrdersDataAccess.stOrderProducts> ();
            foreach (clsOrderProduct orderProduct in OrderProducts)
            {
                clsOrdersDataAccess.stOrderProducts stOrderProduct = new clsOrdersDataAccess.stOrderProducts();
                stOrderProduct.ProductID = orderProduct.ProductID;
                stOrderProduct.OrderID = orderProduct.OrderID;
                stOrderProduct.TotalAmount = orderProduct.TotalAmount;
                stOrderProduct.TotaPrice = orderProduct.TotalPrice;

                stOrderProducts.Add(stOrderProduct);
            }
            return stOrderProducts;
        }

        private bool _AddOrderProducts()
        {
            UpdateTotalPrice();
            if (clsOrdersDataAccess.UpdateOrder(OrderID, CustomerID, OrderDate, TotalPrice, EmployeeID))
            {
                List<clsOrdersDataAccess.stOrderProducts> stOrderProducts = ConvertOrderProductListToStruct();
                clsOrdersDataAccess.AddOrderProducts(stOrderProducts);
                return true;
            }
            //foreach (clsOrderProduct orderProduct in OrderProducts)
            //{
            //    clsOrdersDataAccess.AddNewOrderProduct(OrderID, orderProduct.ProductID, orderProduct.TotalAmount, orderProduct.TotalPrice);
            //}
            return false;
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if(_AddNewOrder())
                {
                    Mode = enMode.SetOrderProducts;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (Mode == enMode.SetOrderProducts)
            {
                if (_AddOrderProducts())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if(Mode == enMode.Update)
            {
                return _UpdateOrder();
            }
            return false;
        }

        public bool IsProductsExistInOrderProducts(int ProductID)
        {
           foreach(clsOrderProduct orderProduct in OrderProducts)
            {
                if(orderProduct.ProductID == ProductID)
                    return true;
            }
            return false;
        }

        public void Clear()
        {
            TotalPrice = 0;
            _OrderProducts.Clear();
        }

    }
}