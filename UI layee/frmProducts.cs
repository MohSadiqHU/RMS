using RestaurantManagementSystemBusiness;
using RestaurantOrderSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResturantManagmentSystemUILayer
{
    public partial class frmProducts : Form
    {
        List<clsProduct> Products = new List<clsProduct>();
        List<clsProduct> _Products = new List<clsProduct>();
        DataTable ProductsDT = new DataTable();
        List<UCProductPar> ProductPars = new List<UCProductPar>();
        public frmProducts()
        {
            InitializeComponent();
            
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            
            
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            FillCategoriescbox();
            ProductsDT = clsProduct.GetAllProducts();
            dgvProducts.DataSource = ProductsDT;
            dgvProducts.DefaultCellStyle.Font = new Font("Eras Medium ITC", 20.75f);

            
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Eras Medium ITC", 27.75f, FontStyle.Bold);

            dgvProducts.ColumnHeadersHeight = 50;

            dgvProducts.RowTemplate.Height = 30;

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
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

        private void txtFind_TextChanged(object sender, EventArgs e)
        {


            DataView dv = ProductsDT.DefaultView;
            


            dv.RowFilter = $"ProductName LIKE '%{txtFind.Text}%'";

            dgvProducts.DataSource = dv;


        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbCategory.SelectedIndex == 0)
            {
                dgvProducts.DataSource = ProductsDT;
                return;
            }
            if (cbCategory.SelectedValue != null)
            {
                string CategoryName = cbCategory.SelectedText ;

                DataView dv = ProductsDT.DefaultView;

                dv.RowFilter = $"CategoryName = {CategoryName}";

                dgvProducts.DataSource = dv;
            }
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewProduct form = new AddNewProduct();
            form.ShowDialog();
            ProductsDT = clsProduct.GetAllProducts();
            dgvProducts.DataSource = ProductsDT;
        }

        void UpdateDataGirdView()
        {
            ProductsDT = clsProduct.GetAllProducts();
            dgvProducts.DataSource = ProductsDT;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                int id = (int) dgvProducts.CurrentRow.Cells["ProductID"].Value;

                clsProduct Product = clsProduct.Find(id);

                if(MessageBox.Show("Are you sour to Delete " + Product.ProductName, "Delete Item", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    clsProduct.DeleteProduct(id);
                }
            }
            UpdateDataGirdView();
            
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ShowQuantityDialog()
        {
            int id;
            clsProduct product;
            
                 id = (int)dgvProducts.CurrentRow.Cells["ProductID"].Value;

                product = clsProduct.Find(id);

                
            
            var dialog = new Form
            {
                Text = "Edit Price",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(250, 120),
                BackColor = Color.White
            };

            var lblQuantity = new Label
            {
                Text = $"Price for {product.ProductName}:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Black
            };

            var numQuantity = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 50,
                Value = decimal.Parse(product.Price.ToString()),
                Location = new Point(20, 50),
                Width = 210,
                Font = new Font("Segoe UI", 10)
            };

            var btnUpdate = new Button
            {
                Text = "Update",
                DialogResult = DialogResult.OK,
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(20, 80),
                Width = 100,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnUpdate.FlatAppearance.BorderSize = 0;

            var btnRemove = new Button
            {
                Text = "Cansel",
                DialogResult = DialogResult.Ignore,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(130, 80),
                Width = 100,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnRemove.FlatAppearance.BorderSize = 0;

            dialog.Controls.Add(lblQuantity);
            dialog.Controls.Add(numQuantity);
            dialog.Controls.Add(btnUpdate);
            dialog.Controls.Add(btnRemove);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                float quantity = (float)numQuantity.Value;
                product.Price = quantity;
                product.Save();

            }
            
        }

        private void changePriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowQuantityDialog();
            UpdateDataGirdView();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
