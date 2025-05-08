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
    public partial class UCOrderProduct : UserControl
    {
        public clsOrderProduct OrderProduct { get; set; }

        public clsProduct Product { get; set; }

        



        public delegate void DeleteFromOrderBoard(clsOrderProduct orderProduct);

        public event DeleteFromOrderBoard DeleteProduct;

        public UCOrderProduct(clsProduct product, clsOrderProduct orderProduct)
        {
            InitializeComponent();
            this.OrderProduct = orderProduct;
            Product = product;
            lblProductName.Text = product.ProductName;
            lblTotalPrice.Text = orderProduct.TotalPrice.ToString() + " $";
            lblUnitePrice.Text = product.Price.ToString() + " $";
            lblUnites.Text = orderProduct.TotalAmount.ToString() ;
        }

        public UCOrderProduct()
        {
            InitializeComponent();
            
        }

        private void UCOrderProduct_Load(object sender, EventArgs e)
        {
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DeleteProduct?.Invoke(OrderProduct);
            }
        }

        private void ShowQuantityDialog()
        {
            var dialog = new Form
            {
                Text = "Edit Quantity",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(250, 120),
                BackColor = Color.White
            };

            var lblQuantity = new Label
            {
                Text = $"Quantity for {Product.ProductName}:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Black
            };

            var numQuantity = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 50,
                Value = decimal.Parse(OrderProduct.TotalAmount.ToString()),
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
                Text = "Remove",
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
                int quantity = (int)numQuantity.Value;
                float price = Product.Price;
                OrderProduct.TotalAmount = quantity;
                OrderProduct.TotalPrice = (float) quantity * price;
                lblUnites.Text = OrderProduct.TotalAmount.ToString();
                lblTotalPrice.Text = OrderProduct.TotalPrice.ToString() + " $";

            }
            else if (dialog.DialogResult == DialogResult.Ignore)
            {
                DeleteProduct?.Invoke(OrderProduct);
                dialog.Close();
                
            }
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ShowQuantityDialog();
            
        }

        private void lblUnitePrice_Click(object sender, EventArgs e)
        {

        }
    }
}
