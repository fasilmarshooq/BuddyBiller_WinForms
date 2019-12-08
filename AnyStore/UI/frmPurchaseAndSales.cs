using AnyStore.BLL;
using AnyStore.DAL;
using BB.System.Common;
using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace AnyStore.UI
{
   
    public partial class frmPurchaseAndSales : Form
    {
        BuddyBillerRepository db = new BuddyBillerRepository();
        public frmPurchaseAndSales()
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
            string type = frmUserDashboard.transactionType;
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
                var fileteredPartyResult = db.Parties.Where(x => (x.Name.Contains(keyword) && x.IsActive)).Select(X => new GridParty { Id = X.Id, Name = X.Name, Type = X.Type, PhoneNumber = X.PhoneNumber, Address = X.Address, Email = X.Email, IsActive = X.IsActive }).FirstOrDefault();


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

        List<AddedProductGrid> listOFAddedProducts = new List<AddedProductGrid>();
        DataTable addedProductsDT;

        private void UpdateAddedProductGrid(AddedProductGrid product)
        {
            listOFAddedProducts.Add(product);
            addedProductsDT = DataSetLinqOperators.ToDataTable(listOFAddedProducts);
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
                
               
                dgvAddedProducts.DataSource = addedProductsDT;

                CalculateSubTotal();
                txtDiscount_TextChanged(sender, e);

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

            var subTotal = listOFAddedProducts.Sum(x => x.Total);

            txtSubTotal.Text = subTotal.ToString();
            
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
           
            
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
    
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmProducts products = new frmProducts();
            products.Show();
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
