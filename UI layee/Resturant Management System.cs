using Guna.UI2.WinForms;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        void ShowNotPermissionedMessage()
        {
            MessageBox.Show("You don't have permission to access this section Contact Your Admin.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void ShowWillBeaddedMessage()
        {
            MessageBox.Show("This feature will be added soon.", "Feature Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        List<Guna2Button> guna2Buttons = new List<Guna2Button>();
        


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        void SetbtnColor(Guna2Button button)
        {
            foreach (Guna2Button btn in guna2Buttons)
            {
                if (btn == button)
                {
                    btn.FillColor = Color.White;
                }
                else
                {
                    btn.FillColor = Color.FromArgb(255, 255, 192);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2Buttons.Add(btnOrderfrm);
            guna2Buttons.Add(btn2);
            guna2Buttons.Add(btn3);
            guna2Buttons.Add(btn4);
            guna2Buttons.Add(btn5);

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private  void btnOrderfrm_Click(object sender, EventArgs e)
        {
            if(!clsGlobal.SystemUser.HasPermission(clsUser.enMainMenuePermissions.pOrders))
            {
                ShowNotPermissionedMessage();
                return;
            }
            SetbtnColor(btnOrderfrm);
            pMain.Controls.Clear();
            frmOrder Uscn = new frmOrder();
            Uscn.TopLevel = false;
            Uscn.FormBorderStyle = FormBorderStyle.None;
            Uscn.Dock = DockStyle.Fill;
            pMain.Controls.Add(Uscn);
            pMain.Tag = Uscn;
            Uscn.Show();
            Uscn.BringToFront();

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.SystemUser.HasPermission(clsUser.enMainMenuePermissions.pOrders))
            {
                ShowNotPermissionedMessage();
                return;
            }
            SetbtnColor(btn2);
            pMain.Controls.Clear();
            frmProducts Uscn = new frmProducts();
            Uscn.TopLevel = false;
            Uscn.FormBorderStyle = FormBorderStyle.None;
            Uscn.Dock = DockStyle.Fill;
            pMain.Controls.Add(Uscn);
            pMain.Tag = Uscn;
            Uscn.Show();
            Uscn.BringToFront();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.SystemUser.HasPermission(clsUser.enMainMenuePermissions.pEmployees))
            {
                ShowNotPermissionedMessage();
                return;
            }
            SetbtnColor(btn4);
            pMain.Controls.Clear();
            frmEmployee Uscn = new frmEmployee();
            Uscn.TopLevel = false;
            Uscn.FormBorderStyle = FormBorderStyle.None;
            Uscn.Dock = DockStyle.Fill;
            pMain.Controls.Add(Uscn);
            pMain.Tag = Uscn;
            Uscn.Show();
            Uscn.BringToFront();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.SystemUser.HasPermission(clsUser.enMainMenuePermissions.pUsers))
            {
                ShowNotPermissionedMessage();
                return;
            }
            SetbtnColor(btn5);
            pMain.Controls.Clear();
            frmUsers Uscn = new frmUsers();
            Uscn.TopLevel = false;
            Uscn.FormBorderStyle = FormBorderStyle.None;
            Uscn.Dock = DockStyle.Fill;
            pMain.Controls.Add(Uscn);
            pMain.Tag = Uscn;
            Uscn.Show();
            Uscn.BringToFront();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.SystemUser.HasPermission(clsUser.enMainMenuePermissions.pIngredients))
            {
                ShowNotPermissionedMessage();
                return;
            }
            ShowWillBeaddedMessage();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (!clsGlobal.SystemUser.HasPermission(clsUser.enMainMenuePermissions.pPurchases))
            {
                ShowNotPermissionedMessage();
                return;
            }
            ShowWillBeaddedMessage();

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (!clsGlobal.SystemUser.HasPermission(clsUser.enMainMenuePermissions.pCustomers))
            {
                ShowNotPermissionedMessage();
                return;
            }
            ShowWillBeaddedMessage();

        }
    }
}
