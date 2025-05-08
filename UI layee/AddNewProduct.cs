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
    public partial class AddNewProduct : Form
    {
        public AddNewProduct()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        void FillCategoriescbox()
        {
            // Clear the existing items in the ComboBox
            DataTable Categories = clsCategory.GetAllCategoriesinTable();
            foreach (DataRow row in Categories.Rows)
            {
                // Assuming the first column contains the category name
                string categoryName = row["CategoryName"].ToString();
                cbCategory.Items.Add(categoryName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You want To Save This Product", "Add Product", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsProduct.IsProductExists(txtProductName.Text))
                {
                    MessageBox.Show("This Product is Already Exists");
                    return;
                }
                clsProduct product = new clsProduct();
                product.ProductName = txtProductName.Text;
                product.Category = cbCategory.SelectedItem.ToString();
                product.Price = float.Parse(txtPrice.Text);

                if (product.Save())
                {
                    MessageBox.Show("The Product is Added Successfully");
                    this.Close();
                }
                    
                
            }
        }

        private void AddNewProduct_Load(object sender, EventArgs e)
        {
            FillCategoriescbox();
        }
    }
}