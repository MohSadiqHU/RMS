using RestaurantManagementSystemBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResturantManagmentSystemUILayer
{
    public partial class UserControl1 : UserControl
    {
        DataTable Categories = new DataTable();
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            Categories = clsCategory.GetAllCategoriesinTable();
            FillCategoriescbox();
        }

       

        void FillCategoriescbox()
        {
            // Clear the existing items in the ComboBox
            foreach (DataRow row in Categories.Rows)
            {
                // Assuming the first column contains the category name
                string categoryName = row["CategoryName"].ToString();
                cbCategories.Items.Add(categoryName);
            }
        }

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UserControl1_Resize(object sender, EventArgs e)
        {
            cbCategories.Left = this.ClientSize.Width - cbCategories.Width - 10;
            cbCategories.Top = this.ClientSize.Height - cbCategories.Height - 10;
        }
    }
}
