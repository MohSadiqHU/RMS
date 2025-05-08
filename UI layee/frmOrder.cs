using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Guna.UI2.WinForms;
using RestaurantManagementSystemBusiness;
using iTextSharp.text;
using iTextSharp.text.pdf;



using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ResturantManagmentSystemUILayer
{
    public  partial  class  frmOrder : Form
    {
        public clsOrder Order = new clsOrder();
        DataTable Categories = new DataTable();
        List<clsProduct> Products = new List<clsProduct>();
        List<clsProduct> _Products = new List<clsProduct>();
        DataTable ProductsDT = new DataTable();
        List<UCProductPar> ProductPars = new List<UCProductPar>();
        List<UCOrderProduct> UCOrderProducts = new List<UCOrderProduct>();

        
        private void  _AddNewOrderProduct(clsProduct product, int TotalAmount)
        {
            
            foreach(clsOrderProduct orderProduct in Order.OrderProducts)
            {
                if(product.ProductID == orderProduct.ProductID)
                {
                    orderProduct.TotalAmount = TotalAmount;
                    orderProduct.TotalPrice = product.Price * TotalAmount;
                    return;
                }
                
            }
            if (Order.OrderID == -1)
            {
                Order.Save();
            }
            Order.OrderProducts.Add( clsOrderProduct._SetOrderProduct(product, TotalAmount,Order.OrderID));
        }

        public void ClickOnOrderInProductBar(clsProduct product, int TotalAmount)
        {
            _AddNewOrderProduct(product,TotalAmount);
            ShowOrderProductBars();

        }

        private clsProduct FindProductFromList(int ProductID)
        {
            foreach (clsProduct product in _Products)
            {
                if (product.ProductID == ProductID)
                {
                    return product;
                }
            }
            return null;
        }

        private void ShowOrderProductBars()
        {
            panel1.Controls.Clear();
            
            
            foreach (clsOrderProduct orderProduct in Order.OrderProducts)
            {
                clsProduct product = FindProductFromList(orderProduct.ProductID);
                
                UCOrderProduct orderProductPar = new UCOrderProduct(product, orderProduct);
                orderProductPar.Dock = DockStyle.Top;
                panel1.Controls.Add(orderProductPar);
                orderProductPar.Show();
                orderProductPar.DeleteProduct += DeleteOrderProduct;
            }
            Order.UpdateTotalPrice();
            lblTotalPrice.Text = Order.TotalPrice.ToString() + " $";


        }
        public frmOrder()
        {
            InitializeComponent();
        }

        public void DeleteOrderProduct(clsOrderProduct orderProduct)
        {
            Order.OrderProducts.Remove(orderProduct);
            ShowOrderProductBars();
        }

        void FillProductsFromTable()
        {
            Products.Clear();
            _Products.Clear();
            ProductsDT = clsProduct.GetAllProducts();
            foreach (DataRow row in ProductsDT.Rows)
            {
                // Assuming the first column contains the product name
                int ProductID = Convert.ToInt32(row["ProductID"]);
                // Assuming the second column contains the product price

                clsProduct product = clsProduct.Find(ProductID);
                Products.Add(product);
                _Products.Add(product);
            }
           
        }

        void FillCategoriescbox()
        {
            // Clear the existing items in the ComboBox
            foreach (DataRow row in Categories.Rows)
            {
                // Assuming the first column contains the category name
                string categoryName = row["CategoryName"].ToString();
                cbCategory.Items.Add(categoryName);
            }
        }


        void FillProductsFromTable(string Find)
        {
            
            Products.Clear();
            ProductsDT = clsProduct.GetAllProducts();
            DataRow[] foundRows = new  DataRow[5];
            if (Find == "" && cbCategory.SelectedIndex != 0)
            {
                foundRows = ProductsDT.Select($"CategoryName LIKE '{cbCategory.SelectedItem}'");

            }
            else if (Find != "" && cbCategory.SelectedIndex == 0)
            {
                foundRows = ProductsDT.Select($"ProductName LIKE '%{Find}%'");

            }
            else if (Find != "" && cbCategory.SelectedIndex != 0)
            {
                foundRows = ProductsDT.Select($"ProductName LIKE '%{Find}%' AND CategoryName = '{cbCategory.SelectedItem}'");
            }
            else if( Find == "" && cbCategory.SelectedIndex == 0)
            {
                FillProductsFromTable();
                return;
            }


            foreach (DataRow row in foundRows)
            {
                // Assuming the first column contains the product name
                int ProductID = Convert.ToInt32(row["ProductID"]);
                // Assuming the second column contains the product price

                clsProduct product = clsProduct.Find(ProductID);
                Products.Add(product);
            }
        }

        void FillProductsFromTable(string CategoryName, bool value)
        {
            if (CategoryName == "")
                return;
            Products.Clear();
            ProductsDT = clsProduct.GetAllProducts();
            DataRow[] foundRows = ProductsDT.Select($"CategoryName LIKE '{CategoryName}'");
            foreach (DataRow row in foundRows)
            {
                // Assuming the first column contains the product name
                int productID = Convert.ToInt32(row["ProductID"]);
                // Assuming the second column contains the product price

                clsProduct product = clsProduct.Find(productID);
                Products.Add(product);
            }
        }



        void ShowProductsbars()
        {
            pProducts.Controls.Clear();
            foreach (clsProduct product in Products)
            {
                UCProductPar productPar = new UCProductPar(product);
                ProductPars.Add(productPar);
                productPar.Dock = DockStyle.Top;
                pProducts.Controls.Add(productPar);
                productPar.product1 += ClickOnOrderInProductBar;
                productPar.Show();
            }
        }
        private void frmOrder_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
           
            Categories = clsCategory.GetAllCategoriesinTable();
            FillCategoriescbox();
            FillProductsFromTable();
            ShowProductsbars();

            cbCategory.SelectedIndex = 0;

        }

        

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Resize(object sender, EventArgs e)
        {
            

        }

        private void frmOrder_Resize(object sender, EventArgs e)
        {
            cbCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //cbCategory.Left = this.ClientSize.Width - cbCategory.Width - 10;
            //cbCategory.Top = 10;  // 10 بيكسل من الحافة العلوية

            // إضافة Anchor لكي يظل يتحرك بشكل ديناميكي مع الفورم
            cbCategory.Anchor =  AnchorStyles.Right;
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            
                FillProductsFromTable(txtFind.Text);
                ShowProductsbars();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void cbCategory_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
            FillProductsFromTable(txtFind.Text);
            ShowProductsbars();

                
        }

        private void pProducts_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddToOrder_Click(object sender, EventArgs e)
        {
          
        }

        private void pOrderProducts_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddToOrder_Click_1(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Get The Order", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {    
                
                if (MessageBox.Show("Do you want to Print the Order", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Order.UpdateTotalPrice();
                    FillProductsFromTable();
                    InvoiceForm frm = new InvoiceForm(Order,_Products);
                    frm.ShowDialog();
                }
                Order.Save();
                panel1.Controls.Clear();
                Order = new clsOrder();

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you suer you want to Cancel", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                panel1.Controls.Clear();
                Order.Clear();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
