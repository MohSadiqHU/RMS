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
    public partial class frmEmployee : Form
    {
        DataTable EmployeesDT = new DataTable();
        public frmEmployee()
        {
            InitializeComponent();
        }

        void UpdateEmployeeDataGridView()
        {
            EmployeesDT = clsEmployee.GetAllEmployees();
            dgvEmployee.DataSource = EmployeesDT;
            dgvEmployee.DefaultCellStyle.Font = new Font("Eras Medium ITC", 20.75f);
            dgvEmployee.ColumnHeadersDefaultCellStyle.Font = new Font("Eras Medium ITC", 27.75f, FontStyle.Bold);
            dgvEmployee.ColumnHeadersHeight = 50;
            dgvEmployee.RowTemplate.Height = 30;
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            UpdateEmployeeDataGridView();
            cbPostion.SelectedIndex = 0;
        }

        private void changePriceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbPostion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbPostion.SelectedIndex == 0) {
                UpdateEmployeeDataGridView();
                return;
            }
            DataView dv = EmployeesDT.DefaultView;
            dv.RowFilter = $"Position LIKE '%{cbPostion.SelectedItem}%'";

            dgvEmployee.DataSource = dv;
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewEmployee addNewEmployee = new AddNewEmployee();
            addNewEmployee.ShowDialog();
            UpdateEmployeeDataGridView();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvEmployee.CurrentRow != null)
            {
                int id = (int)dgvEmployee.CurrentRow.Cells["EmployeeID"].Value;

                clsEmployee Employee = clsEmployee.Find(id);

                if (MessageBox.Show("Are you sour to Delete " + Employee.FirstName, "Delete Item", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    clsEmployee.Delete(id);
                }
            }
            UpdateEmployeeDataGridView();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dgvEmployee.CurrentRow.Cells["EmployeeID"].Value;
            AddNewEmployee addNewEmployee = new AddNewEmployee(id);
            addNewEmployee.ShowDialog();
            UpdateEmployeeDataGridView();
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
