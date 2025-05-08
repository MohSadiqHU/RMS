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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           if (! clsUser.isUserExists(txtUserName.Text))
           {
                MessageBox.Show("User Name is Not Exists");
                return;
           }
           if( clsUser.isUserExists(txtUserName.Text))
            {
                clsGlobal.SystemUser = clsUser.Find(txtUserName.Text);
                if(!clsGlobal.SystemUser.AllowLogin(txtPassword.Text))
                {
                    MessageBox.Show("The Password Is Not Correct");
                }
                else
                {
                    clsGlobal.Password = txtPassword.Text;

                    this.Close();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
