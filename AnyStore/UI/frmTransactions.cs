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

namespace AnyStore.UI
{
    public partial class frmTransactions : Form
    {
        public frmTransactions()
        {
            InitializeComponent();
        }

        transactionDAL tdal = new transactionDAL();
        DeaCustDAL dCDAL = new DeaCustDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {
            DataTable Customerdt = dCDAL.Select();
            cmbCustomer.DataSource = Customerdt;
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
