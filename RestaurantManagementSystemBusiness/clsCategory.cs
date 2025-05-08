using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystemDataAccess;

namespace RestaurantManagementSystemBusiness
{
    public class clsCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        enMode Mode = enMode.AddNew;
        enum enMode { AddNew = 1, Update };
        private clsCategory(int categoryID, string categoryName)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
        }
        public clsCategory()
        {
            CategoryID = -1;
            CategoryName = string.Empty;
            Mode = enMode.AddNew;
        }
        private bool AddNewCategory()
        {
            CategoryID = clsCategoriesDataAccess.AddNewCategory(CategoryName);
            return (CategoryID != -1);
        }

        private bool UpdateCategory()
        {
            return clsCategoriesDataAccess.UpdateCategory(CategoryID, CategoryName);
        }

        public static clsCategory Find(int CategoryID)
        {
            string CategoryName = "";
            clsCategory category = new clsCategory(CategoryID, CategoryName);
            return category;
        }

        public static int GetCategoryID(string CategoryName)
        {
            return clsCategoriesDataAccess.GetCategoryID(CategoryName);
        }

        public static List<string> GetAllCategories()
        {
            return clsCategoriesDataAccess.GetAllCategories();
        }

        public static bool DeleteCategory(int CategoryID)
        {
            return clsCategoriesDataAccess.DeleteCategory(CategoryID);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if(AddNewCategory())
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
                return UpdateCategory();
            }
            return false;

        }

        public bool isCategoryExist(int CategoryID)
        {
            return clsCategoriesDataAccess.IsCategoryIDExists(CategoryID);
        }

        public static DataTable GetAllCategoriesinTable()
        {
            return clsCategoriesDataAccess.GetAllCategoriesWithID();
        }


    }
}
