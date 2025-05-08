using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantManagementSystemBusiness;

namespace ResturantManagmentSystemUILayer
{
    public partial class UCProductPar : UserControl
    {

        public clsProduct Product { get; set; }
        public int TotalAmount { get; set; }

        public delegate void BackProductToOrderProduct(clsProduct product, int TotalAmount);
        public BackProductToOrderProduct product1;

        public UCProductPar(clsProduct product)
        {
            InitializeComponent();
            this.Product = product;
            lblProductPrice.Text = Product.Price.ToString() + " $";
            lblCategory.Text = Product.Category;
            LableProducName.Text = Product.ProductName;

        }

        private void LableProduc_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void UCProductPar_Load(object sender, EventArgs e)
        {
            guna2NumericUpDown1.Value = 1;
            TotalAmount = int.Parse(guna2NumericUpDown1.Value.ToString());
        }

        private void btnAddToOrder_Click(object sender, EventArgs e)
        {

            product1?.Invoke(Product, TotalAmount);

        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            TotalAmount =int.Parse (guna2NumericUpDown1.Value.ToString());
        }
    }
}
