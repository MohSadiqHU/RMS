using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystemDataAccess;

namespace RestaurantManagementSystemBusiness
{
    public class clsPurchase
    {
        public int PurchaseID { get; set; }

        public DateTime PurchaseDate { get; set; }
        public float TotalPrice { get; set; }
        public List<clsPurchaseIngredients> PurchaseIngredients { get; private set; } = new List<clsPurchaseIngredients>();

        private enMode _Mode;
        enum enMode { AddNew = 1, Update };
        public clsPurchase()
        {
            PurchaseID = -1;
            PurchaseDate = DateTime.Now;
            TotalPrice = 0;
            PurchaseIngredients = new List<clsPurchaseIngredients>();
            _Mode = enMode.AddNew;
        }
        public clsPurchase(int purchaseID, DateTime purchaseDate, float totalPrice)
        {
            PurchaseID = purchaseID;
            PurchaseDate = purchaseDate;
            TotalPrice = totalPrice;
            PurchaseIngredients = clsPurchaseIngredients.GetPurchaseIngredients(purchaseID);
            _Mode = enMode.Update;
        }
        static public clsPurchase Find(int purchaseID)
        {
            DateTime purchaseDate = DateTime.Now;
            float totalPrice = 0;
            clsPurchaseDataAccess.GetPurchaseByID(purchaseID, ref purchaseDate, ref totalPrice);
            return new clsPurchase(purchaseID, purchaseDate, totalPrice);
        }
        private bool _AddNewPurchase()
        {
            PurchaseID = clsPurchaseDataAccess.AddNewPurchase(PurchaseDate, TotalPrice);
            return PurchaseID != -1;
        }
        private bool _UpdatePurchase()
        {
            return clsPurchaseDataAccess.UpdatePurchase(PurchaseID, PurchaseDate, TotalPrice);
        }
        static public bool DeletePurchase(int purchaseID)
        {
            return clsPurchaseDataAccess.DeletePurchase(purchaseID);
        }
        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                if (_AddNewPurchase())
                {
                    SavePurchaseIngredients();
                    _Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if( _UpdatePurchase())
                {
                    if (DeletePurchaseIngredients())
                    {
                        SavePurchaseIngredients();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AddPurchaseIngredient(clsPurchaseIngredients purchaseIngredient)
        {
            if (purchaseIngredient != null)
            {
                PurchaseIngredients.Add(purchaseIngredient);
                return true;
            }
            return false;
        }
        public bool RemovePurchaseIngredient(clsPurchaseIngredients purchaseIngredient)
        {
            if (purchaseIngredient != null)
            {
                PurchaseIngredients.Remove(purchaseIngredient);
                return true;
            }
            return false;
        }

        public clsPurchaseIngredients GetPurchaseIngredient(int purchaseID, int ingredientID)
        {
            foreach (clsPurchaseIngredients purchaseIngredient in PurchaseIngredients)
            {
                if (purchaseIngredient.PurchaseID == purchaseID && purchaseIngredient.IngredientID == ingredientID)
                {
                    return purchaseIngredient;
                }
            }
            return null;
        }

        private void SavePurchaseIngredients()
        {
            foreach (clsPurchaseIngredients purchaseIngredient in PurchaseIngredients)
            {
                purchaseIngredient.Save();
            }
        }
        private bool DeletePurchaseIngredients()
        {
            return clsPurchaseDataAccess.DeletePurchaseIngredients(PurchaseID);
        }



    }
}
