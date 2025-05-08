using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystemBusiness
{
    public class clsProductIngredient
    {
        public int ProductID { get; set; }
        public int IngredientID { get; set; }
        public int TotalAmountConsumed {  get; set; }
        public DateTime TheDateConsumed { get; set; }



    }
}
