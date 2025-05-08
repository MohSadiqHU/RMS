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
    public partial class AddNewEmployee : Form
    {
        clsEmployee employee = new clsEmployee();
        public AddNewEmployee()
        {
            InitializeComponent();
        }

        public AddNewEmployee(int EmployeeID)
        {
            InitializeComponent();
            
            employee = clsEmployee.Find(EmployeeID);
            txtFirstName.Text = employee.FirstName;
            txtMidName.Text = employee.MidName;
            txtLastName.Text = employee.LastName;
            txtPosition.Text = employee.Position;
            txtSalary.Text = employee.Salary.ToString();
            if(employee.Phones.Count > 0)
                txtPhone.Text = employee.Phones[0];
            txtEmail.Text = employee.Email;
            btnSave.Text = "Update";
            this.Text = "Update Employee";
            lbMode.Text = "Update Employee";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to Save this employee?", "Add Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                employee.FirstName = txtFirstName.Text;
                employee.LastName = txtLastName.Text;
                employee.Position = txtPosition.Text;
                employee.Salary = float.Parse(txtSalary.Text);
                employee.Phones.Add(txtPhone.Text);
                employee.Email = txtEmail.Text;
                if (employee.Save())
                {
                    MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
