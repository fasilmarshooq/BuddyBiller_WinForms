using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BB.System.Common;
using BuddyBiller.BLL;

namespace AnyStore.UI
{
    public partial class FrmDealCust : Form
    {
        private readonly BuddyBillerRepository db = new BuddyBillerRepository();
        private List<GridParty> parties;
        private DataTable partiesdt;
        private int selectedPartyId;

        public FrmDealCust()
        {
            InitializeComponent();
        }

        private void ReloadForm()
        {
            Clear();
            var partyTypesList = db.PartyTypeConfigs.Select(x => x);
            parties = db.Parties.Where(x => x.IsActive).Select(x => new GridParty
            {
                Id = x.Id, Name = x.Name, Type = x.Type, PhoneNumber = x.PhoneNumber, Address = x.Address,
                Email = x.Email, IsActive = x.IsActive
            }).ToList();

            partiesdt = parties.ToDataTable();
            dgvDeaCust.DataSource = partiesdt;

            var partyTypes = partyTypesList.ToDataTable<PartyTypeConfig>();
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
            Hide();
        }

        private void Clear()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }


        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var rowIndex = e.RowIndex;

            selectedPartyId = int.Parse(dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString());
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var party = new Party
            {
                Id = selectedPartyId,
                Type = cmbDeaCust.Text,
                Name = txtName.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtContact.Text,
                Address = txtAddress.Text,
                Added_Date = DateTime.Now,
                IsActive = true
            };


            var bLl = new DeaCustBll();

            bLl.SaveOrUpdate(party, db);
            ReloadForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var party = new Party
            {
                Id = selectedPartyId,
                Type = cmbDeaCust.Text,
                Name = txtName.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtContact.Text,
                Address = txtAddress.Text,
                Added_Date = DateTime.Now,
                IsActive = false
            };

            var bLl = new DeaCustBll();

            bLl.SaveOrUpdate(party, db);

            ReloadForm();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text;

            if (keyword != null)
            {
                var fileteredPartyResult = parties.Where(x => x.Name.Contains(keyword) && x.IsActive).Select(x =>
                    new GridParty
                    {
                        Id = x.Id, Name = x.Name, Type = x.Type, PhoneNumber = x.PhoneNumber, Address = x.Address,
                        Email = x.Email, IsActive = x.IsActive
                    }).ToList();

                dgvDeaCust.DataSource = fileteredPartyResult.ToDataTable();
            }
            else
            {
                dgvDeaCust.DataSource = partiesdt;
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