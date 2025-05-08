using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystemDataAccess;

namespace RestaurantManagementSystemBusiness
{
    public class clsPurchaseIngredients
    {
        public int PurchaseID { get; set; }
        public int IngredientID { get; set; }
        public int TotalAmount { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice { get; set; }
        enMode _Mode;
        enum enMode { AddNew = 1, Update };
        public clsPurchaseIngredients()
        {
            PurchaseID = -1;
            IngredientID = -1;
            TotalAmount = 0;
            UnitPrice = 0;
            TotalPrice = 0;
            _Mode = enMode.AddNew;
        }
        public clsPurchaseIngredients(int purchaseID, int ingredientID, int totalAmount, float unitPrice, float totalPrice)
        {
            PurchaseID = purchaseID;
            IngredientID = ingredientID;
            TotalAmount = totalAmount;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            _Mode = enMode.Update;
        }

        private bool _AddNewPurchaseIngredients()
        {
            return clsPurchaseDataAccess.AddNewPurchaseIngredient(PurchaseID, IngredientID, TotalAmount, UnitPrice, TotalPrice);

        }
        private bool _UpdatePurchaseIngredients()
        {
            return clsPurchaseDataAccess.UpdatePurchaseIngredient(PurchaseID, IngredientID, TotalAmount, UnitPrice, TotalPrice);
        }
        static public bool _DeletePurchaseIngredients(int purchaseID, int ingredientID)
        {
            return clsPurchaseDataAccess.DeletePurchaseIngredient(purchaseID, ingredientID);
        }
        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                if (_AddNewPurchaseIngredients())
                {
                    _Mode = enMode.Update;
                    return true;
                }
            }
            else
            {
                return _UpdatePurchaseIngredients();

            }
            return false;
        }
        static public clsPurchaseIngredients ConvertFromStructToClass(clsPurchaseDataAccess.PurchaseIngredient purchaseIngredient)
        {
            return new clsPurchaseIngredients(purchaseIngredient.PurchaseID, purchaseIngredient.IngredientID, purchaseIngredient.TotalAmount, purchaseIngredient.PriceForEachUnit, purchaseIngredient.TotalPrice);
        }
        static public List<clsPurchaseIngredients> GetPurchaseIngredients(int purchaseID)
        {
            List<clsPurchaseIngredients> purchaseIngredients = new List<clsPurchaseIngredients>();
            List<clsPurchaseDataAccess.PurchaseIngredient> purchaseIngredientArray = clsPurchaseDataAccess.GetPurchaseIngredients(purchaseID);
            foreach (clsPurchaseDataAccess.PurchaseIngredient purchaseIngredient in purchaseIngredientArray)
            {
                purchaseIngredients.Add(ConvertFromStructToClass(purchaseIngredient));
            }
            return purchaseIngredients;

        }
        static public void DeletePurchaseIngredients(int purchaseID)
        {
            clsPurchaseDataAccess.DeletePurchaseIngredients(purchaseID);
        }
        
        

    }
}
