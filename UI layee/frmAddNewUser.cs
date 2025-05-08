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
    public partial class frmAddNewUser : Form
    {
        DataTable EmployeesDT =  new DataTable();

        public delegate void DataBackEventHandler(int PersonID);

        
        public event DataBackEventHandler DataBack;

        public frmAddNewUser()
        {
            InitializeComponent();
        }

        void UpdateEmployeeDataGridView()
        {
            EmployeesDT = clsEmployee.GetAllEmployees();
            dgvEmployee.DataSource = EmployeesDT;
            
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            UpdateEmployeeDataGridView();
            cbPostion.SelectedIndex = 0;
        }

        private void cbPostion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPostion.SelectedIndex == 0)
            {
                UpdateEmployeeDataGridView();
                return;
            }
            DataView dv = EmployeesDT.DefaultView;
            dv.RowFilter = $"Position LIKE '%{cbPostion.SelectedItem}%'";

            dgvEmployee.DataSource = dv;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            int id = (int)dgvEmployee.CurrentRow.Cells["EmployeeID"].Value;
            DataBack?.Invoke(id);
            this.Close();
        }
    }
}
