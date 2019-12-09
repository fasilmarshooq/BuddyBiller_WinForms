using BB.System.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AnyStore.UI
{

    public partial class FrmPurchaseAndSales : Form
    {
        BuddyBillerRepository db = new BuddyBillerRepository();
        public FrmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            //Get the transactionType value from frmUserDashboard
            string type = FrmUserDashboard.transactionType;
            //Set the value on lblTop
            lblTop.Text = type;

            initializeFields();
        }

        private void initializeFields()
        {
            txtSubTotal.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtVat.Text = "0.00";
            txtGrandTotal.Text = "0.00";
            txtPaidAmount.Text = "0.00";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the keyword fro the text box
            string keyword = txtSearch.Text;

            if (keyword == "")
            {
                //Clear all the textboxes
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }
            else
            {
                var fileteredPartyResult = db.Parties.Where(x => (x.Name.Contains(keyword) && x.IsActive)).Select(x => new GridParty { Id = x.Id, Name = x.Name, Type = x.Type, PhoneNumber = x.PhoneNumber, Address = x.Address, Email = x.Email, IsActive = x.IsActive }).FirstOrDefault();


                txtName.Text = fileteredPartyResult?.Name;
                txtEmail.Text = fileteredPartyResult?.Email;
                txtContact.Text = fileteredPartyResult?.PhoneNumber;
                txtAddress.Text = fileteredPartyResult?.Address;
            }
           
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            //Get the keyword from productsearch textbox
            string keywords = txtSearchProduct.Text;

            //Check if we have value to txtSearchProduct or not
            if (keywords == "")
            {
                txtProductName.Text = "";
                txtDescription.Text = "";
                txtRate.Text = "";
                TxtQty.Text = "";
                return;
            }

            //Search the product and display on respective textboxes

            var filteredProductResults = db.Products.Where(x => ( x.Name.Contains(keywords) || x.Description.Contains(keywords)) && x.IsActive).Select(x => new ProductGrid { Id = x.Id, Name = x.Name, Description = x.Description, Rate = x.Rate, Qty = x.Qty, IsActive = x.IsActive, ProductType = x.ProductType.Name }).FirstOrDefault();


            //Set the values on textboxes based on p object
            txtProductName.Text = filteredProductResults?.Name;
            txtDescription.Text = filteredProductResults?.Qty.ToString();
            txtRate.Text = filteredProductResults?.Rate.ToString();
            TxtQty.Text = filteredProductResults?.Qty.ToString();
        }

        List<AddedProductGrid> listOfAddedProducts = new List<AddedProductGrid>();
        DataTable addedProductsDt;

        private void UpdateAddedProductGrid(AddedProductGrid product)
        {
            listOfAddedProducts.Add(product);
            addedProductsDt = DataSetLinqOperators.ToDataTable(listOfAddedProducts);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddedProductGrid product = new AddedProductGrid()
            {
                ProductName = txtProductName.Text,
                ProductDescription = txtDescription.Text,
                Rate = decimal.Parse(txtRate.Text),
                Quantity = decimal.Parse(TxtQty.Text),
                Total = decimal.Parse(txtRate.Text) * decimal.Parse(TxtQty.Text)

            };


            //Check whether the product is selected or not
            if(product.ProductName == "")
            {
                //Display error MEssage
                MessageBox.Show("Select the product first. Try Again.");
            }
            else
            {
                UpdateAddedProductGrid(product);
                
               
                dgvAddedProducts.DataSource = addedProductsDt;

                CalculateSubTotal();

                //Clear the Textboxes
                txtSearchProduct.Text = "";
                txtProductName.Text = "";
                txtDescription.Text = "0.00";
                txtRate.Text = "0.00";
                TxtQty.Text = "0.00";
            }
        }

        private void CalculateSubTotal()
        {

            var subTotal = listOfAddedProducts.Sum(x => x.Total);

            txtSubTotal.Text = subTotal.ToString();
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            FrmProducts products = new FrmProducts();
            products.Show();
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressed(sender, e);
        }

        private static void KeyPressed(object sender, KeyPressEventArgs e)
        {
// allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
    }

    public class AddedProductGrid
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Rate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }

    }

}
