using System;
using System.Windows.Forms;
using BuddyBiller.BLL;
using BuddyBiller.DAL;
using BuddyBiller.Properties;

namespace AnyStore.UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private readonly LoginBll l = new LoginBll();
        private readonly LoginDal dal = new LoginDal();
        public static string loggedIn;

        private void PboxClose_Click(object sender, EventArgs e)
        {
            //Code to close this form
            Close();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Login();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            l.Username = txtUsername.Text.Trim();
            l.Password = txtPassword.Text.Trim();
            l.UserType = cmbUserType.Text.Trim();

            //Checking the login credentials
            var sucess = dal.loginCheck(l);
            if (sucess)
            {
                //Login Successfull
                //MessageBox.Show("Login Successful.");
                loggedIn = l.Username;
                //Need to open Respective Forms based on User Type
                switch (l.UserType)
                {
                    case "Admin":
                    {
                        //Display Admin Dashboard
                        var admin = new FrmAdminDashboard();
                        admin.Show();
                        Hide();
                    }
                        break;

                    case "User":
                    {
                        //Display User Dashboard
                        var user = new FrmUserDashboard();
                        user.Show();
                        Hide();
                    }
                        break;

                    default:
                    {
                        //Display an error message
                        MessageBox.Show(Resources.frmLogin_Login_Invalid_User_Type_);
                    }
                        break;
                }
            }
            else
            {
                //login Failed
                MessageBox.Show(Resources.frmLogin_Login_Login_Failed__Try_Again);
            }
        }
    }
}