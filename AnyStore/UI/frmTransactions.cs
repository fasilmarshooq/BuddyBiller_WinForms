using BuddyBiller.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class FrmTransactions : Form
    {
        public FrmTransactions()
        {
            InitializeComponent();
        }

        TransactionDal tdal = new TransactionDal();
        DeaCustDal dCdal = new DeaCustDal();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {
            DataTable customerdt = dCdal.Select();
            cmbCustomer.DataSource = customerdt;
            cmbCustomer.DisplayMember = "Name";
            cmbCustomer.ValueMember = "Name";

        }


        private void btnAll_Click(object sender, EventArgs e)
        {
            string transactionTypetype = cmbTransactionType.Text;

            DataTable dt = tdal.DisplayTransactionReport(transactionTypetype);
            dgvTransactions.DataSource = dt;
        }
    }
}
