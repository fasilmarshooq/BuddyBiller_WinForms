using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BB.System.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;

namespace AnyStore.UI
{
    public partial class frmDeaCust : Form
    {
        BuddyBillerRepository db = new BuddyBillerRepository();
        IQueryable<Party> parties;
        DataTable Partiesdt;
        public frmDeaCust()
        {
            InitializeComponent();
            
            
        }

        private void reloadForm()
        {
            Clear();
            var partyTypesList = db.PartyTypeConfigs.Select(X => X);
            parties = db.Parties.Select(X => X).Where(x=>x.IsActive);

            Partiesdt = DataSetLinqOperators.ToDataTable<Party>(parties);
            dgvDeaCust.DataSource = Partiesdt;

            DataTable partyTypes = DataSetLinqOperators.ToDataTable<PartyTypeConfig>(partyTypesList);
            cmbDeaCust.DataSource = partyTypes;
            cmbDeaCust.DisplayMember = "Name";
            cmbDeaCust.ValueMember = "Name";
        }


        private void   frmDeaCust_Load(object sender,EventArgs e)
        {
            reloadForm();
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Write the code to close this form
            this.Hide();
        }

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();

        //userDAL uDal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the Values from Form


            Party party = new Party();
            party.Id = txtDeaCustID.Text.Equals("") ? 0  : int.Parse(txtDeaCustID.Text);
            party.Type = cmbDeaCust.Text;
            party.Name = txtName.Text;
            party.Email = txtEmail.Text;
            party.PhoneNumber = txtContact.Text;
            party.Address = txtAddress.Text;
            party.Added_Date = DateTime.Now;
            party.IsActive = true;


            SaveOrUpdate(party);
            reloadForm();
            //Getting the ID to Logged in user and passign its value in dealer or cutomer module
            //string loggedUsr = frmLogin.loggedIn;
            //userBLL usr = uDal.GetIDFromUsername(loggedUsr);
            //dc.added_by = usr.id;

            //Creating boolean variable to check whether the dealer or cutomer is added or not
            // bool success = dcDal.Insert(dc);

            // if(success==true)
            //{
            //Dealer or Cutomer inserted successfully 
            //    MessageBox.Show("Dealer or Customer Added Successfully");
            //    Clear();
            //    //Refresh Data Grid View
            //    DataTable dt = dcDal.Select();
            //    dgvDeaCust.DataSource = dt;
            //}
            //else
            //{
            //    //failed to insert dealer or customer
            //}
        }

        public void SaveOrUpdate(Party entity)
        {
            var sql = @"MERGE INTO Parties
               USING (VALUES (@Id,@Type,@Name,@Email,@PhoneNumber,@Address,@Added_Date,@Added_By_Id,@IsActive)) AS
                            s(Id,Type,Name,Email,PhoneNumber,Address,Added_Date,Added_By_Id,IsActive)
                ON Parties.id = s.id
                WHEN MATCHED THEN
                    UPDATE
                    SET     Type=s.Type
                            ,Name=s.Name
                            ,Email=s.Email
                            ,PhoneNumber=s.PhoneNumber
                            ,Address=s.Address
                            ,Added_Date=s.Added_Date
                            ,Added_By_Id=s.Added_By_Id
                            ,IsActive=s.IsActive

                WHEN NOT MATCHED THEN
                    INSERT (Type,Name,Email,PhoneNumber,Address,Added_Date,Added_By_Id,IsActive)
                    VALUES (s.Type,s.Name,s.Email,s.PhoneNumber,s.Address,s.Added_Date,s.Added_By_Id,s.IsActive);";

            object[] parameters = {
                new SqlParameter("@id", entity.Id),
                new SqlParameter("@Type", entity.Type),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@PhoneNumber", entity.PhoneNumber),
                new SqlParameter("@Address", entity.Address),
                new SqlParameter("@Added_Date", entity.Added_Date),
                new SqlParameter("@Added_By_Id", '1'), // TO:DO extend to stamp user session id
                new SqlParameter("@IsActive", entity.IsActive) 
                
            };
            db.Database.ExecuteSqlCommand(sql, parameters);
        }



        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }

        public string partyID;

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int variable to get the identityof row clicked
            int rowIndex = e.RowIndex;

            txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();

            partyID = txtDeaCustID.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from Form
            dc.id = int.Parse(txtDeaCustID.Text);
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            //Getting the ID to Logged in user and passign its value in dealer or cutomer module
            string loggedUsr = frmLogin.loggedIn;
            //userBLL usr = uDal.GetIDFromUsername(loggedUsr);
           // dc.added_by = usr.id;

            //create boolean variable to check whether the dealer or customer is updated or not
            bool success = dcDal.Update(dc);
            
            if(success==true)
            {
                //Dealer and Customer update Successfully
                MessageBox.Show("Dealer or Customer updated Successfully");
                Clear();
                //Refresh the Data Grid View
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Failed to udate Dealer or Customer
                MessageBox.Show("Failed to Udpate Dealer or Customer");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            

            Party party = new Party();
            party.Id = txtDeaCustID.Text.Equals("") ? 0 : int.Parse(txtDeaCustID.Text);
            party.Type = cmbDeaCust.Text;
            party.Name = txtName.Text;
            party.Email = txtEmail.Text;
            party.PhoneNumber = txtContact.Text;
            party.Address = txtAddress.Text;
            party.Added_Date = DateTime.Now;
            party.IsActive = false;

            SaveOrUpdate(party);
            reloadForm();


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the keyowrd from text box
            string keyword = txtSearch.Text;

            if(keyword!=null)
            {

                var fileteredPartyResult = parties.Where(x => (x.Name.Contains(keyword)&& x.IsActive));

                dgvDeaCust.DataSource = DataSetLinqOperators.ToDataTable<Party>(fileteredPartyResult); ;
            }
            else
            {

                dgvDeaCust.DataSource = Partiesdt;
            }
        }
    }
}
