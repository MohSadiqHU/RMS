using RestaurantManagementSystemBusiness;
using RestaurantOrderSystem;
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
    public partial class frmSetUserInfo : Form
    {
        clsEmployee Employee = new clsEmployee();
        clsUser User = new clsUser();
        public frmSetUserInfo(clsEmployee employee)
        {
            InitializeComponent();
            Employee = employee;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void frmSetUserInfo_Load(object sender, EventArgs e)
        {
            lbFirstName.Text = Employee.FirstName;
            LbMidName.Text = Employee.MidName;
            LbLastName.Text = Employee.LastName;
            if (Employee.Phones.Count > 0)
                lbPhone.Text = Employee.Phones[0];
            else
                lbPhone.Text = "Nothing";
            lblSalary.Text = Employee.Salary.ToString();
            lbPosition.Text = Employee.Position;
            LbEmail.Text = Employee.Email;

            User.RemovePermission(clsUser.enMainMenuePermissions.eAll);

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chbOrders.Checked)
            {
                User.AddPermission(clsUser.enMainMenuePermissions.pOrders);
            }
            else
            {
                User.RemovePermission(clsUser.enMainMenuePermissions.pOrders);
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPurchies.Checked)
            {
                User.AddPermission(clsUser.enMainMenuePermissions.pPurchases);
            }
            else
            {
                User.RemovePermission(clsUser.enMainMenuePermissions.pPurchases);
            }
        }

        private void chbAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chbAll.Checked)
            {
                chbOrders.Enabled = false;
                chbProducts.Enabled = false;
                chbUsers.Enabled = false;
                chbCustomer.Enabled = false;
                chbEmployee.Enabled = false;
                chbIngreadient.Enabled = false;
                chbPurchies.Enabled = false;
                User.AddPermission(clsUser.enMainMenuePermissions.eAll);
            }
            else
            {
                chbOrders.Enabled = true;
                chbProducts.Enabled = true;
                chbUsers.Enabled = true;
                chbCustomer.Enabled = true;
                chbEmployee.Enabled = true;
                chbIngreadient.Enabled = true;
                chbPurchies.Enabled = true;

                chbOrders.Checked = false;
                chbProducts.Checked = false;
                chbUsers.Checked = false;
                chbCustomer.Checked = false;
                chbEmployee.Checked = false;
                chbIngreadient.Checked = false;
                chbPurchies.Checked = false;
                User.RemovePermission(clsUser.enMainMenuePermissions.eAll);
            }
        }

        private void chbProducts_CheckedChanged(object sender, EventArgs e)
        {
            if (chbProducts.Checked)
            {
                User.AddPermission(clsUser.enMainMenuePermissions.pProducts);
            }
            else
            {
                User.RemovePermission(clsUser.enMainMenuePermissions.pProducts);
            }
        }

        private void chbIngreadient_CheckedChanged(object sender, EventArgs e)
        {
            if (chbIngreadient.Checked)
            {
                User.AddPermission(clsUser.enMainMenuePermissions.pIngredients);
            }
            else
            {
                User.RemovePermission(clsUser.enMainMenuePermissions.pIngredients);
            }
        }

        private void chbEmployee_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEmployee.Checked)
            {
                User.AddPermission(clsUser.enMainMenuePermissions.pEmployees);
            }
            else
            {
                User.RemovePermission(clsUser.enMainMenuePermissions.pEmployees);
            }
        }

        private void chbCustomer_CheckedChanged(object sender, EventArgs e)
        {

            if (chbCustomer.Checked)
            {
                User.AddPermission(clsUser.enMainMenuePermissions.pCustomers);
            }
            else
            {
                User.RemovePermission(clsUser.enMainMenuePermissions.pCustomers);
            }
        }

        private void chbUsers_CheckedChanged(object sender, EventArgs e)
        {
            if(chbUsers.Checked)
            {
                User.AddPermission(clsUser.enMainMenuePermissions.pUsers);
            }
            else
            {
                User.RemovePermission(clsUser.enMainMenuePermissions.pUsers);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            User.UserName = txtUserName.Text;
            User.Password = txtPassword.Text;
            User.EmployeeID = Employee.ID;
            if(User.Save())
            {
                MessageBox.Show("User Created Successfully");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error in Creating User");
            }
        }
    }
}
