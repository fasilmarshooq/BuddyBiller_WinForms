using AnyStore.BLL;
using BB.System.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmDeaCust : Form
    {
        BuddyBillerRepository db = new BuddyBillerRepository();
        List<GridParty> parties;
        DataTable Partiesdt;
        int selectedPartyId = 0;


        public frmDeaCust()
        {
            InitializeComponent();
        }

        private void ReloadForm()
        {
            Clear();
            var partyTypesList = db.PartyTypeConfigs.Select(X => X);
            parties = db.Parties.Where(x => x.IsActive).Select(X => new GridParty { Id = X.Id, Name = X.Name, Type = X.Type, PhoneNumber = X.PhoneNumber, Address = X.Address, Email = X.Email, IsActive = X.IsActive }).ToList();

            Partiesdt = DataSetLinqOperators.ToDataTable<GridParty>(parties);
            dgvDeaCust.DataSource = Partiesdt;

            DataTable partyTypes = DataSetLinqOperators.ToDataTable<PartyTypeConfig>(partyTypesList);
            cmbDeaCust.DataSource = partyTypes;
            cmbDeaCust.DisplayMember = "Name";
            cmbDeaCust.ValueMember = "Name";

            txtDeaCustID.Visible = false;
            txtDeaCustID.Visible = false;

            selectedPartyId = 0;
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            ReloadForm();
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void Clear()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }



        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int rowIndex = e.RowIndex;

            selectedPartyId = int.Parse(dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString());
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            Party party = new Party();
            party.Id = selectedPartyId;
            party.Type = cmbDeaCust.Text;
            party.Name = txtName.Text;
            party.Email = txtEmail.Text;
            party.PhoneNumber = txtContact.Text;
            party.Address = txtAddress.Text;
            party.Added_Date = DateTime.Now;
            party.IsActive = true;


            DeaCustBLL bLL = new DeaCustBLL();

            bLL.SaveOrUpdate(party, db);
            ReloadForm();

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

            Party party = new Party();
            party.Id = selectedPartyId;
            party.Type = cmbDeaCust.Text;
            party.Name = txtName.Text;
            party.Email = txtEmail.Text;
            party.PhoneNumber = txtContact.Text;
            party.Address = txtAddress.Text;
            party.Added_Date = DateTime.Now;
            party.IsActive = false;

            DeaCustBLL bLL = new DeaCustBLL();

            bLL.SaveOrUpdate(party, db);

            ReloadForm();


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string keyword = txtSearch.Text;

            if (keyword != null)
            {
                var fileteredPartyResult = parties.Where(x => (x.Name.Contains(keyword) && x.IsActive)).Select(X => new GridParty { Id = X.Id, Name = X.Name, Type = X.Type, PhoneNumber = X.PhoneNumber, Address = X.Address, Email = X.Email, IsActive = X.IsActive }).ToList();

                dgvDeaCust.DataSource = DataSetLinqOperators.ToDataTable<GridParty>(fileteredPartyResult);
            }
            else
            {

                dgvDeaCust.DataSource = Partiesdt;
            }
        }
    }

    public class GridParty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
