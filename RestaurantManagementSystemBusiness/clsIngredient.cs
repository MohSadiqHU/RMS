using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystemDataAccess;

namespace RestaurantManagementSystemBusiness
{
    public class clsIngredient
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }
        public int RecentAmount { get; set; }
        private enMode _Mode;
        enum enMode { AddNew = 1, Update };
        public clsIngredient()
        {
            IngredientID = -1;
            IngredientName = string.Empty;
            RecentAmount = 0;
            _Mode = enMode.AddNew;
        }

        public clsIngredient(int ingredientID, string ingredientName, int recentAmount)
        {
            IngredientID = ingredientID;
            IngredientName = ingredientName;
            RecentAmount = recentAmount;
            _Mode = enMode.Update;
        }
        static public clsIngredient Find(int ingredientID)
        {
            string ingredientName = string.Empty;
            int recentAmount = 0;
            clsIngredientDataAccess.GetIngredientByID(ingredientID, ref ingredientName, ref recentAmount);
            return new clsIngredient(ingredientID, ingredientName, recentAmount);
        }
        private bool _AddNewIngredient()
        {
            IngredientID = clsIngredientDataAccess.AddNewIngredient(IngredientName, RecentAmount);
            return IngredientID != -1;
        }
        private bool _UpdateIngredient()
        {
            return clsIngredientDataAccess.UpdateIngredient(IngredientID, IngredientName, RecentAmount);
        }

        static public bool _DeleteIngredient(int ingredientID)
        {
            return clsIngredientDataAccess.DeleteIngredient(ingredientID);
        }
        public bool Save()
        {

            if (_Mode == enMode.AddNew)
            {
                if (_AddNewIngredient())
                {
                    _Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (_Mode == enMode.Update)
            {
                return _UpdateIngredient();
            }
            return false;

        }

        public bool DecreaseIngredient(int amount)
        {
            if (RecentAmount - amount >= 0)
            {
                RecentAmount -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IncreaseIngredient(int amount)
        {
            if (amount > 0)
            {
                RecentAmount += amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsValid()
        {
            if (IngredientName.Length < 3)
            {
                return false;
            }
            if (RecentAmount < 0)
            {
                return false;
            }
            return true;
        }

        public static DataTable GetAllIngredients()
        {
            return clsIngredientDataAccess.GetAllIngredients();
        }

        
    }
}
    