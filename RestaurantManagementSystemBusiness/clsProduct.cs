using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystemDataAccess;

namespace RestaurantManagementSystemBusiness
{
    public class clsProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }

        enum enMode { AddNew = 1, Update };
        enMode Mode = enMode.AddNew;
        private clsProduct(int productID, string productName, float price, string category)
        {
            ProductID = productID;
            ProductName = productName;
            Price = price;
            Category = category;
            Mode = enMode.Update;
        }
        public clsProduct()
        {
            ProductID = -1;
            ProductName = string.Empty;
            Price = 0;
            Category = "";
            Mode = enMode.AddNew;
        }

        private bool _AddNewProduct()
        {
            int CategoryID = clsCategoriesDataAccess.GetCategoryID(Category);
            ProductID = clsProductsDataAccess.AddNewProduct(ProductName, Price, CategoryID);
            return (ProductID != -1);
        }

        private bool _UpdateProduct()
        {
            int CategoryID = clsCategoriesDataAccess.GetCategoryID(Category);
            return clsProductsDataAccess.UpdateProduct(ProductID, ProductName, Price, CategoryID);
        }

        public static clsProduct Find(int productID)
        {
            string productName = "";
            float price = 0;
            int categoryID = -1;
            string CategoryName = "";
            clsProductsDataAccess.GetProductByID(productID, ref productName, ref price, ref CategoryName);
            return new clsProduct(productID, productName, price, CategoryName);
        }

        public static DataTable GetAllProducts()
        {
            return clsProductsDataAccess.GetAllProducts();
        }

        public static bool DeleteProduct(int productID)
        {
            return clsProductsDataAccess.DeleteProduct(productID);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if (_AddNewProduct())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            if (Mode == enMode.Update)
            {
                return _UpdateProduct();

            }
            return false;

        }

        public static bool IsProductExists(string productName)
        {
            return clsProductsDataAccess.isProductExistsByName(productName);
        }

    }
}
