using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VasthuApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (LoginFormValidate() && ValidateUser())
            {
                MDIParent _mdi = new MDIParent();
                _mdi.Show();
                this.Hide();
            }
        }

        bool ValidateUser()
        {
            var db = new Models.VasthuDBEntities();
            var user = db.Users.SingleOrDefault(u => u.UserName.Equals(txtUserName.Text.Trim()) && u.Password.Equals(txtPassword.Text.Trim()));
            if (user == null)
            {
                lblError.Text = "Invalid username or password!";
                return false;
            }
            else
            {
                return true;
            }
        }


        bool LoginFormValidate()
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                lblError.Text = "Enter user name";
                return false;

            }
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                lblError.Text = "Enter password";
                return false;
            }
            return true;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
