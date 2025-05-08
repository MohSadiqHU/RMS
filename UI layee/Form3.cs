using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace RestaurantOrderSystem
{
    public partial class MainOrderForm : Form
    {
        private Color PrimaryColor = Color.FromArgb(44, 62, 80);
        private Color SecondaryColor = Color.FromArgb(52, 152, 219);
        private Color AccentColor = Color.FromArgb(46, 204, 113);
        private Color LightColor = Color.FromArgb(236, 240, 241);
        private Color DarkTextColor = Color.FromArgb(44, 62, 80);
        private Color LightTextColor = Color.FromArgb(236, 240, 241);

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        public MainOrderForm()
        {
            InitializeComponent();
            ConfigureUI();
            LoadSampleData();
            UpdateOrderSummary();
        }

        private void ConfigureUI()
        {
            // Form setup
            this.Text = "Restaurant Order System";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 800);
            this.BackColor = LightColor;
            this.Font = new Font("Segoe UI", 10);
            this.Icon = SystemIcons.Application;

            // Main layout container
            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.Transparent,
                Padding = new Padding(10)
            };
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            this.Controls.Add(mainPanel);

            // Left panel - Categories
            var categoryPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };
            mainPanel.Controls.Add(categoryPanel, 0, 0);

            var categoryLabel = new Label
            {
                Text = "MENU CATEGORIES",
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = PrimaryColor,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = LightColor,
                Padding = new Padding(10, 0, 0, 0)
            };
            categoryPanel.Controls.Add(categoryLabel);

            categoriesListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(0, 10, 0, 0),
                IntegralHeight = false,
                DrawMode = DrawMode.OwnerDrawFixed
            };
            categoriesListBox.DrawItem += CategoriesListBox_DrawItem;
            categoriesListBox.SelectedIndexChanged += CategoriesListBox_SelectedIndexChanged;
            categoryPanel.Controls.Add(categoriesListBox);

            // Middle panel - Products
            var productsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };
            mainPanel.Controls.Add(productsPanel, 1, 0);

            var productsLabel = new Label
            {
                Text = "PRODUCTS",
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = PrimaryColor,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = LightColor,
                Padding = new Padding(10, 0, 0, 0)
            };
            productsPanel.Controls.Add(productsLabel);

            productsFlowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White,
                Margin = new Padding(0, 10, 0, 0)
            };
            productsPanel.Controls.Add(productsFlowPanel);

            // Right panel - Order details
            var orderPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };
            mainPanel.Controls.Add(orderPanel, 2, 0);

            var orderLabel = new Label
            {
                Text = $"ORDER #{DateTime.Now:yyyyMMddHHmm}",
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = PrimaryColor,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = LightColor,
                Padding = new Padding(10, 0, 0, 0)
            };
            orderPanel.Controls.Add(orderLabel);

            orderDetailsListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = false,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 10, 0, 10)
            };
            orderDetailsListView.Columns.Add("Item", 150);
            orderDetailsListView.Columns.Add("Qty", 50);
            orderDetailsListView.Columns.Add("Price", 80);
            orderDetailsListView.Columns.Add("Total", 100);
            orderDetailsListView.OwnerDraw = true;
            orderDetailsListView.DrawColumnHeader += OrderDetailsListView_DrawColumnHeader;
            orderDetailsListView.DrawSubItem += OrderDetailsListView_DrawSubItem;
            orderDetailsListView.MouseClick += OrderDetailsListView_MouseClick;
            orderPanel.Controls.Add(orderDetailsListView);

            // Order summary panel
            var summaryPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 180,
                BackColor = LightColor,
                Padding = new Padding(10)
            };

            var subtotalLabel = new Label
            {
                Text = "Subtotal:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = DarkTextColor,
                Location = new Point(10, 10),
                AutoSize = true
            };
            summaryPanel.Controls.Add(subtotalLabel);

            subtotalValueLabel = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 10),
                ForeColor = DarkTextColor,
                Location = new Point(150, 10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };
            summaryPanel.Controls.Add(subtotalValueLabel);

            var taxLabel = new Label
            {
                Text = "Tax (10%):",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = DarkTextColor,
                Location = new Point(10, 35),
                AutoSize = true
            };
            summaryPanel.Controls.Add(taxLabel);

            taxValueLabel = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 10),
                ForeColor = DarkTextColor,
                Location = new Point(150, 35),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };
            summaryPanel.Controls.Add(taxValueLabel);

            var totalLabel = new Label
            {
                Text = "Total:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = DarkTextColor,
                Location = new Point(10, 65),
                AutoSize = true
            };
            summaryPanel.Controls.Add(totalLabel);

            totalValueLabel = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = PrimaryColor,
                Location = new Point(150, 65),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };
            summaryPanel.Controls.Add(totalValueLabel);

            var placeOrderButton = new Button
            {
                Text = "PLACE ORDER",
                BackColor = AccentColor,
                ForeColor = LightTextColor,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Height = 45,
                Width = summaryPanel.Width - 20,
                Location = new Point(10, 100),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            placeOrderButton.FlatAppearance.BorderSize = 0;
            placeOrderButton.Click += PlaceOrderButton_Click;
            summaryPanel.Controls.Add(placeOrderButton);

            var printButton = new Button
            {
                Text = "PRINT INVOICE",
                BackColor = SecondaryColor,
                ForeColor = LightTextColor,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Height = 35,
                Width = (summaryPanel.Width - 25) / 2,
                Location = new Point(10, 150),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            printButton.FlatAppearance.BorderSize = 0;
            printButton.Click += PrintButton_Click;
            summaryPanel.Controls.Add(printButton);

            var clearButton = new Button
            {
                Text = "CLEAR ORDER",
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = LightTextColor,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Height = 35,
                Width = (summaryPanel.Width - 25) / 2,
                Location = new Point(printButton.Right + 5, 150),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            clearButton.FlatAppearance.BorderSize = 0;
            clearButton.Click += ClearButton_Click;
            summaryPanel.Controls.Add(clearButton);

            orderPanel.Controls.Add(summaryPanel);
        }

        private void CategoriesListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            using (Brush brush = new SolidBrush(isSelected ? SecondaryColor : DarkTextColor))
            {
                e.Graphics.DrawString(categoriesListBox.Items[e.Index].ToString(),
                    e.Font, brush, e.Bounds);
            }

            if (isSelected)
            {
                e.Graphics.FillRectangle(new SolidBrush(SecondaryColor),
                    new Rectangle(e.Bounds.Right - 5, e.Bounds.Top, 5, e.Bounds.Height));
            }

            e.DrawFocusRectangle();
        }

        private void OrderDetailsListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var headerBrush = new SolidBrush(PrimaryColor))
            using (var headerFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var textBrush = new SolidBrush(LightTextColor))
            {
                e.Graphics.FillRectangle(headerBrush, e.Bounds);
                e.Graphics.DrawString(e.Header.Text, headerFont, textBrush, e.Bounds.Left + 5, e.Bounds.Top + 5);
            }
        }

        private void OrderDetailsListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private ListBox categoriesListBox;
        private FlowLayoutPanel productsFlowPanel;
        private ListView orderDetailsListView;
        private Label subtotalValueLabel;
        private Label taxValueLabel;
        private Label totalValueLabel;

        private void LoadSampleData()
        {
            // Sample categories
            var categories = new List<string>
            {
                "Appetizers",
                "Main Courses",
                "Desserts",
                "Beverages",
                "Specials"
            };

            categoriesListBox.DataSource = categories;

            // Select first category by default
            if (categories.Count > 0)
            {
                categoriesListBox.SelectedIndex = 0;
            }
        }

        private void CategoriesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoriesListBox.SelectedItem == null) return;

            string selectedCategory = categoriesListBox.SelectedItem.ToString();
            LoadProductsForCategory(selectedCategory);
        }

        private void LoadProductsForCategory(string category)
        {
            productsFlowPanel.Controls.Clear();

            // Sample products based on category
            List<Product> products = new List<Product>();

            switch (category)
            {
                case "Appetizers":
                    products.Add(new Product("Bruschetta", 8.99m, "Toasted bread with tomatoes"));
                    products.Add(new Product("Calamari", 12.99m, "Fried squid with marinara"));
                    products.Add(new Product("Garlic Bread", 5.99m, "Fresh baked with herbs"));
                    products.Add(new Product("Mozzarella Sticks", 7.99m, "Breaded and fried"));
                    break;
                case "Main Courses":
                    products.Add(new Product("Spaghetti Carbonara", 16.99m, "Classic Italian pasta"));
                    products.Add(new Product("Grilled Salmon", 22.99m, "With lemon butter sauce"));
                    products.Add(new Product("Filet Mignon", 29.99m, "8oz with mashed potatoes"));
                    products.Add(new Product("Vegetable Risotto", 14.99m, "Creamy arborio rice"));
                    break;
                case "Desserts":
                    products.Add(new Product("Tiramisu", 8.99m, "Classic Italian dessert"));
                    products.Add(new Product("Chocolate Lava Cake", 9.99m, "With vanilla ice cream"));
                    products.Add(new Product("Cheesecake", 7.99m, "New York style"));
                    break;
                case "Beverages":
                    products.Add(new Product("Soda", 2.99m, "Coke, Sprite, Fanta"));
                    products.Add(new Product("Iced Tea", 3.49m, "Fresh brewed"));
                    products.Add(new Product("Coffee", 3.99m, "Regular or decaf"));
                    products.Add(new Product("Wine", 8.99m, "House red or white"));
                    break;
                case "Specials":
                    products.Add(new Product("Chef's Special Pasta", 18.99m, "Today's special creation"));
                    products.Add(new Product("Seasonal Soup", 6.99m, "Ask server for details"));
                    break;
            }

            foreach (var product in products)
            {
                var productCard = new Panel
                {
                    Width = 200,
                    Height = 120,
                    Margin = new Padding(10),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Cursor = Cursors.Hand,
                    Tag = product
                };

                var nameLabel = new Label
                {
                    Text = product.Name,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = PrimaryColor,
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                var priceLabel = new Label
                {
                    Text = product.Price.ToString("C"),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = SecondaryColor,
                    Location = new Point(10, 35),
                    AutoSize = true
                };

                var descLabel = new Label
                {
                    Text = product.Description,
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(120, 120, 120),
                    Location = new Point(10, 60),
                    AutoSize = false,
                    Size = new Size(180, 40)
                };

                productCard.Controls.Add(nameLabel);
                productCard.Controls.Add(priceLabel);
                productCard.Controls.Add(descLabel);

                // Add click event to add to order
                productCard.Click += (sender, e) => AddToOrder(product);

                // Add hover effects
                productCard.MouseEnter += (sender, e) =>
                {
                    productCard.BackColor = Color.FromArgb(245, 245, 245);
                    productCard.BorderStyle = BorderStyle.Fixed3D;
                };
                productCard.MouseLeave += (sender, e) =>
                {
                    productCard.BackColor = Color.White;
                    productCard.BorderStyle = BorderStyle.FixedSingle;
                };

                productsFlowPanel.Controls.Add(productCard);
            }
        }

        private void AddToOrder(Product product)
        {
            // Check if item already exists in order
            bool itemExists = false;
            foreach (ListViewItem item in orderDetailsListView.Items)
            {
                if (item.Text == product.Name)
                {
                    // Update quantity
                    int quantity = int.Parse(item.SubItems[1].Text) + 1;
                    item.SubItems[1].Text = quantity.ToString();
                    decimal total = quantity * product.Price;
                    item.SubItems[3].Text = total.ToString("C");
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // Add new item to order
                var item = new ListViewItem(product.Name);
                item.SubItems.Add("1");
                item.SubItems.Add(product.Price.ToString("C"));
                item.SubItems.Add(product.Price.ToString("C"));
                orderDetailsListView.Items.Add(item);
            }

            UpdateOrderSummary();
        }

        private void UpdateOrderSummary()
        {
            decimal subtotal = 0;

            foreach (ListViewItem item in orderDetailsListView.Items)
            {
                subtotal += decimal.Parse(item.SubItems[3].Text, System.Globalization.NumberStyles.Currency);
            }

            decimal tax = subtotal * 0.10m; // 10% tax
            decimal total = subtotal + tax;

            subtotalValueLabel.Text = subtotal.ToString("C");
            taxValueLabel.Text = tax.ToString("C");
            totalValueLabel.Text = total.ToString("C");
        }

        private void OrderDetailsListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = orderDetailsListView.HitTest(e.Location);
                if (hitTest.Item != null)
                {
                    ShowQuantityDialog(hitTest.Item);
                }
            }
        }

        private void ShowQuantityDialog(ListViewItem item)
        {
            var dialog = new Form
            {
                Text = "Edit Quantity",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(250, 120),
                BackColor = LightColor
            };

            var lblQuantity = new Label
            {
                Text = $"Quantity for {item.Text}:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = DarkTextColor
            };

            var numQuantity = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 50,
                Value = decimal.Parse(item.SubItems[1].Text),
                Location = new Point(20, 50),
                Width = 210,
                Font = new Font("Segoe UI", 10)
            };

            var btnUpdate = new Button
            {
                Text = "Update",
                DialogResult = DialogResult.OK,
                BackColor = SecondaryColor,
                ForeColor = LightTextColor,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(20, 80),
                Width = 100,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnUpdate.FlatAppearance.BorderSize = 0;

            var btnRemove = new Button
            {
                Text = "Remove",
                DialogResult = DialogResult.Cancel,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = LightTextColor,
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
                decimal price = decimal.Parse(item.SubItems[2].Text, System.Globalization.NumberStyles.Currency);
                item.SubItems[1].Text = quantity.ToString();
                item.SubItems[3].Text = (quantity * price).ToString("C");
                UpdateOrderSummary();
            }
            else if (dialog.DialogResult == DialogResult.Cancel)
            {
                orderDetailsListView.Items.Remove(item);
                UpdateOrderSummary();
            }
        }

        private void PlaceOrderButton_Click(object sender, EventArgs e)
        {
            if (orderDetailsListView.Items.Count == 0)
            {
                MessageBox.Show("Please add items to the order before placing it.", "Empty Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Confirm placement of this order?", "Confirm Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // In a real application, you would save the order to a database here
                MessageBox.Show("Order placed successfully!", "Order Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Generate and print invoice
                PrintInvoice();

                // Clear the current order and start a new one
                ClearCurrentOrder();
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (orderDetailsListView.Items.Count == 0)
            {
                MessageBox.Show("No items in the order to print.", "Empty Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintInvoice();
        }

        private void PrintInvoice()
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = $"Order Invoice #{DateTime.Now:yyyyMMddHHmm}";
            printDoc.PrintPage += (sender, e) =>
            {
                Graphics graphics = e.Graphics;
                Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
                Font headerFont = new Font("Segoe UI", 12, FontStyle.Bold);
                Font itemFont = new Font("Segoe UI", 10);
                Font totalFont = new Font("Segoe UI", 12, FontStyle.Bold);

                float yPos = 50;
                float leftMargin = e.MarginBounds.Left;
                float rightMargin = e.MarginBounds.Right;

                // Draw title
                string title = "RESTAURANT INVOICE";
                graphics.DrawString(title, titleFont, new SolidBrush(PrimaryColor),
                    new RectangleF(leftMargin, yPos, e.MarginBounds.Width, titleFont.Height),
                    new StringFormat { Alignment = StringAlignment.Center });
                yPos += titleFont.Height + 20;

                // Draw order info
                graphics.DrawString($"Order #: {DateTime.Now:yyyyMMddHHmm}", headerFont,
                    new SolidBrush(DarkTextColor), leftMargin, yPos);
                yPos += headerFont.Height + 5;
                graphics.DrawString($"Date: {DateTime.Now:g}", headerFont,
                    new SolidBrush(DarkTextColor), leftMargin, yPos);
                yPos += headerFont.Height + 20;

                // Draw column headers
                graphics.DrawString("Item", headerFont, new SolidBrush(PrimaryColor), leftMargin, yPos);
                graphics.DrawString("Qty", headerFont, new SolidBrush(PrimaryColor), leftMargin + 200, yPos);
                graphics.DrawString("Price", headerFont, new SolidBrush(PrimaryColor), leftMargin + 250, yPos);
                graphics.DrawString("Total", headerFont, new SolidBrush(PrimaryColor), leftMargin + 350, yPos);
                yPos += headerFont.Height + 5;

                // Draw a line
                graphics.DrawLine(new Pen(PrimaryColor), leftMargin, yPos, rightMargin, yPos);
                yPos += 10;

                // Draw order items
                foreach (ListViewItem item in orderDetailsListView.Items)
                {
                    graphics.DrawString(item.Text, itemFont, new SolidBrush(DarkTextColor), leftMargin, yPos);
                    graphics.DrawString(item.SubItems[1].Text, itemFont, new SolidBrush(DarkTextColor), leftMargin + 200, yPos);
                    graphics.DrawString(item.SubItems[2].Text, itemFont, new SolidBrush(DarkTextColor), leftMargin + 250, yPos);
                    graphics.DrawString(item.SubItems[3].Text, itemFont, new SolidBrush(DarkTextColor), leftMargin + 350, yPos);
                    yPos += itemFont.Height + 5;
                }

                yPos += 10;
                graphics.DrawLine(new Pen(PrimaryColor), leftMargin, yPos, rightMargin, yPos);
                yPos += 20;

                // Draw totals
                graphics.DrawString("Subtotal:", totalFont, new SolidBrush(DarkTextColor), leftMargin + 250, yPos);
                graphics.DrawString(subtotalValueLabel.Text, totalFont, new SolidBrush(DarkTextColor), leftMargin + 350, yPos);
                yPos += totalFont.Height + 5;

                graphics.DrawString("Tax (10%):", totalFont, new SolidBrush(DarkTextColor), leftMargin + 250, yPos);
                graphics.DrawString(taxValueLabel.Text, totalFont, new SolidBrush(DarkTextColor), leftMargin + 350, yPos);
                yPos += totalFont.Height + 5;

                graphics.DrawString("Total:", totalFont, new SolidBrush(PrimaryColor), leftMargin + 250, yPos);
                graphics.DrawString(totalValueLabel.Text, totalFont, new SolidBrush(PrimaryColor), leftMargin + 350, yPos);
                yPos += totalFont.Height + 20;

                // Draw thank you message
                graphics.DrawString("Thank you for your order!", headerFont,
                    new SolidBrush(SecondaryColor),
                    new RectangleF(leftMargin, yPos, e.MarginBounds.Width, headerFont.Height),
                    new StringFormat { Alignment = StringAlignment.Center });
            };

            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDoc,
                WindowState = FormWindowState.Maximized
            };
            previewDialog.ShowDialog();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (orderDetailsListView.Items.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you want to clear the current order?",
                    "Clear Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    ClearCurrentOrder();
                }
            }
        }

        private void ClearCurrentOrder()
        {
            orderDetailsListView.Items.Clear();
            UpdateOrderSummary();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Product(string name, decimal price, string description = "")
        {
            Name = name;
            Price = price;
            Description = description;
        }
    }
}

