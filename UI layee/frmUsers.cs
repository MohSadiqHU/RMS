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
    public partial class frmUsers : Form
    {
        clsEmployee Employee = new clsEmployee();
        DataTable UsersDT = new DataTable();
        public frmUsers()
        {
            InitializeComponent();
        }

        private void Employee_DataBack( int PersonID)
        {
            // Handle the data received from Form2
            Employee = clsEmployee.Find(PersonID);


        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmAddNewUser frm = new frmAddNewUser();
            frm.DataBack += Employee_DataBack;
            frm.ShowDialog();
            

            if (Employee.ID != 0)
            {
                frmSetUserInfo frmSetUserInfo = new frmSetUserInfo(Employee);
                frmSetUserInfo.ShowDialog();
            }

        }
        void UpdateDGV()
        {
            UsersDT = clsUser.GetAllUsers();
            dgvUsers.DataSource = UsersDT;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            UpdateDGV();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            if (MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsUser.Delete(id))
                {
                    MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateDGV();
                }
                else
                {
                    MessageBox.Show("Failed to delete user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvUsers.CurrentRow.Cells["EmployeeID"].Value;
            Employee = clsEmployee.Find(id);
            if (Employee != null)
            {
                frmSetUserInfo frmSetUserInfo = new frmSetUserInfo(Employee);
                frmSetUserInfo.ShowDialog();
                UpdateDGV();
            }
            else
            {
                MessageBox.Show("Failed to find employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
