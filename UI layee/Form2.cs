using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using RestaurantManagementSystemBusiness;

namespace ResturantManagmentSystemUILayer
{
    public partial class Form2 : Form
    {
        public clsOrder Order = new clsOrder();
        DataTable Categories = new DataTable();
        List<clsProduct> Products = new List<clsProduct>();
        DataTable ProductsDT = new DataTable();
        List<UCProductPar> ProductPars = new List<UCProductPar>();
        public Form2()
        {
            this.Cursor = Cursors.Default;
            InitializeComponent();    
        }

         
        void FillProductsFromTable()
        {
           ProductsDT = clsProduct.GetAllProducts();
            foreach (DataRow row in ProductsDT.Rows)
            {
                // Assuming the first column contains the product name
                int ProductID = Convert.ToInt32(row["ProductID"]);
                // Assuming the second column contains the product price
               
                clsProduct product = clsProduct.Find(ProductID);
                Products.Add(product);
            }
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            // مثلاً تحرك الزر حسب حجم الفورم
            cbCategories.Left = this.ClientSize.Width - cbCategories.Width - 10;
            cbCategories.Top =  cbCategories.Height - 10;
        }

        void FillCategoriescbox()
        {
            // Clear the existing items in the ComboBox
            foreach(DataRow row in Categories.Rows)
            {
                // Assuming the first column contains the category name
                string categoryName = row["CategoryName"].ToString();
                cbCategories.Items.Add(categoryName);
            }
        }

       
        void ShowProductsbars()
        {
            foreach (clsProduct product in Products)
            {
                UCProductPar productPar = new UCProductPar(product);
                ProductPars.Add(productPar);
                productPar.Dock = DockStyle.Top;
                pProducts.Controls.Add(productPar);
                productPar.Show();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            cbCategories.SelectedIndex = 0;
            Categories = clsCategory.GetAllCategoriesinTable();
            FillCategoriescbox();
            FillProductsFromTable();
            ShowProductsbars();

        }

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
