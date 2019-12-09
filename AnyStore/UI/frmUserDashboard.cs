using AnyStore.UI;
using System;
using System.Windows.Forms;
using BuddyBiller.Properties;

namespace AnyStore
{
    public partial class FrmUserDashboard : Form
    {
        FrmProductTypes productTypes;
        private FrmProducts product;


        public FrmUserDashboard()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        //Set a public static method to specify whether the form is purchase or sales
        public static string transactionType;
        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Hide();
        }

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = FrmLogin.loggedIn;
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDealCust dealCust = new FrmDealCust();
            dealCust.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set value on transactionType static method
            transactionType = "Purchase";
            FrmPurchaseAndSales purchase = new FrmPurchaseAndSales();
            purchase.MdiParent = this;
            purchase.Show();
        }

        private void salesFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set the value to transacionType method to sales
            transactionType = "Sales";
            FrmPurchaseAndSales sales = new FrmPurchaseAndSales();
            sales.Show();
            
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (product == null)
            {
                product = new FrmProducts();
                product.MdiParent = this;
            }

            product.WindowState = FormWindowState.Maximized;
            product.Show();
        }

        private void productTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (productTypes == null)
            {
                productTypes = new FrmProductTypes {MdiParent = this};
            }

            productTypes.StartPosition = FormStartPosition.CenterParent;
            productTypes.Text = Resources.FrmUserDashboard_productTypeToolStripMenuItem_Click_Product_Types;
            productTypes.Show();
        }
      
    }
}
