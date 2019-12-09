using BuddyBiller.BLL;
using BuddyBiller.DAL;
using BuddyBiller.Properties;
using System;
using System.Data;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class FrmUsers : Form
    {
        public FrmUsers()
        {
            InitializeComponent();
        }

        UserBll u = new UserBll();
        UserDal dal = new UserDal();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            

            //Gettting Data FRom UI
            u.FirstName = txtFirstName.Text;
            u.LastName = txtLastName.Text;
            u.Email = txtEmail.Text;
            u.Username = txtUsername.Text;
            u.Password = txtPassword.Text;
            u.Contact = txtContact.Text;
            u.Address = txtAddress.Text;
            u.Gender = cmbGender.Text;
            u.UserType = cmbUserType.Text;
            u.AddedDate = DateTime.Now;

            //Getting Username of the logged in user
            string loggedUser = FrmLogin.loggedIn;
            UserBll usr = dal.GetIdFromUsername(loggedUser);

            u.AddedBy = usr.Id;

            //Inserting Data into DAtabase
            bool success = dal.Insert(u);
            //If the data is successfully inserted then the value of success will be true else it will be false
            if(success)
            {
                //Data Successfully Inserted
                MessageBox.Show(Resources.frmUsers_btnAdd_Click_User_successfully_created_);
                clear();
            }
            else
            {
                //Failed to insert data
                MessageBox.Show(Resources.frmUsers_btnAdd_Click_Failed_to_add_new_user);
            }
            //Refreshing Data Grid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }
        private void clear()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the index of particular row
            int rowIndex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from User UI
            u.Id = Convert.ToInt32(txtUserID.Text);
            u.FirstName = txtFirstName.Text;
            u.LastName = txtLastName.Text;
            u.Email = txtEmail.Text;
            u.Username = txtUsername.Text;
            u.Password = txtPassword.Text;
            u.Contact = txtContact.Text;
            u.Address = txtAddress.Text;
            u.Gender = cmbGender.Text;
            u.UserType = cmbUserType.Text;
            u.AddedDate = DateTime.Now;
            u.AddedBy = 1;

            //Updating Data into database
            bool success = dal.Update(u);
            //if data is updated successfully then the value of success will be true else it will be false
            if(success)
            {
                //Data Updated Successfully
                MessageBox.Show(Resources.frmUsers_btnUpdate_Click_User_successfully_updated);
                clear();
            }
            else
            {
                //failed to update user
                MessageBox.Show(Resources.frmUsers_btnUpdate_Click_Failed_to_update_user);
            }
            //Refreshing Data Grid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Getting User ID from Form
            u.Id = Convert.ToInt32(txtUserID.Text);

            bool success = dal.Delete(u);
            //if data is deleted then the value of success will be true else it will be false
            if(success)
            {
                //User Deleted Successfully 
                MessageBox.Show(Resources.frmUsers_btnDelete_Click_User_deleted_successfully);
                clear();
            }
            else
            {
                //Failed to Delete User
                MessageBox.Show(Resources.frmUsers_btnDelete_Click_Failed_to_delete_user);

            }
            //refreshing Datagrid view
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get Keyword from Text box
            string keywords = txtSearch.Text;

            //Chec if the keywords has value or not
            if(keywords!=null)
            {
                //Show user based on keywords
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                //show all users from the database
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }
    }
}
