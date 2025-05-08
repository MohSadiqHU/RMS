using RestaurantManagementSystemBusiness;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace ResturantManagmentSystemUILayer
{
    public class InvoiceForm : Form
    {
        private clsOrder _order = new clsOrder();
        List<clsProduct> _Products= new List<clsProduct>();

        public InvoiceForm(clsOrder order, List<clsProduct> products)
        {
            _Products = products;
            _order = order;
            InitializeComponents();
        }

        public string FindProductName(int ProductID)
        {
            foreach (clsProduct product in _Products)
            {
                if (product.ProductID == ProductID)
                    return product.ProductName;
            }
            return null;
        }

        private void InitializeComponents()
        {
            // Form setup
            this.Text = $"Invoice for Order #{_order.OrderID}";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Main panel with scroll
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };
            this.Controls.Add(mainPanel);

            // Header
            var lblTitle = new Label
            {
                Text = "INVOICE",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 20)
            };
            mainPanel.Controls.Add(lblTitle);

            // Order information
            var orderPanel = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            AddOrderInfo(orderPanel);
            mainPanel.Controls.Add(orderPanel);

            // Products grid
            var productsGrid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 300,
                Margin = new Padding(0, 20, 0, 0),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                RowHeadersVisible = false
            };

            SetupProductsGrid(productsGrid);
            mainPanel.Controls.Add(productsGrid);

            // Footer with total
            var footerPanel = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(10),
                Margin = new Padding(0, 20, 0, 0)
            };

            AddFooter(footerPanel);
            mainPanel.Controls.Add(footerPanel);

            // Print button
            var btnPrint = new Button
            {
                Text = "Print Invoice",
                Dock = DockStyle.Top,
                Height = 40,
                Margin = new Padding(0, 20, 0, 0)
            };
            btnPrint.Click += BtnPrint_Click;
            mainPanel.Controls.Add(btnPrint);
        }

        private void AddOrderInfo(Panel panel)
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                AutoSize = true
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            AddLabelValuePair(layout, "Order ID:", _order.OrderID.ToString());
            AddLabelValuePair(layout, "Customer ID:", _order.CustomerID.ToString());
            AddLabelValuePair(layout, "Employee ID:", _order.EmployeeID.ToString());
            AddLabelValuePair(layout, "Order Date:", _order.OrderDate.ToString("yyyy-MM-dd HH:mm"));

            panel.Controls.Add(layout);
        }

        private void AddLabelValuePair(TableLayoutPanel panel, string label, string value)
        {
            var lbl = new Label
            {
                Text = label,
                Font = new Font("Arial", 10, FontStyle.Bold),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var val = new Label
            {
                Text = value,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(val);
        }

        private void SetupProductsGrid(DataGridView grid)
        {
            grid.Columns.Add("ProductName", "Product");
            grid.Columns.Add("Quantity", "Qty");
            grid.Columns.Add("UnitPrice", "Unit Price");
            grid.Columns.Add("Discount", "Discount %");
            grid.Columns.Add("LineTotal", "Line Total");

            // Format columns
            grid.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
            grid.Columns["Discount"].DefaultCellStyle.Format = "P0";
            grid.Columns["LineTotal"].DefaultCellStyle.Format = "C2";

            // Add products
            foreach (var item in _order.OrderProducts)
            {
                grid.Rows.Add(
                    FindProductName(item.ProductID),
                    item.TotalAmount,
                    item.TotalPrice / item.TotalAmount,
                    0,
                    item.TotalPrice
                );
            }
        }

        private void AddFooter(Panel panel)
        {
            var lblTotal = new Label
            {
                Text = $"TOTAL:   $" + _order.TotalPrice,
                Font = new Font("Arial", 14, FontStyle.Bold),
                AutoSize = true,
                Dock = DockStyle.Bottom
            };

            panel.Controls.Add(lblTotal);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            // You would implement actual printing here
            MessageBox.Show("Printing functionality would be implemented here", "Print Invoice");
        }
    }
}