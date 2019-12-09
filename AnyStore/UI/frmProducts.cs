using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BB.System.Common;
using BuddyBiller.BLL;
using BuddyBiller.Properties;

namespace AnyStore.UI
{
    public partial class FrmProducts : Form
    {
        private readonly BuddyBillerRepository db = new BuddyBillerRepository();
        private List<ProductGrid> products;
        private IQueryable<ProductType> productType;
        private DataTable productsDt;
        private DataTable producttypeDt;
        private int selectedProductId;

        public FrmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            Hide();
        }


        private void ReloadForm()
        {
            products = db.Products.Where(x => x.IsActive).Select(x => new ProductGrid
            {
                Id = x.Id, Name = x.Name, Description = x.Description, Rate = x.Rate, Qty = x.Qty,
                IsActive = x.IsActive, ProductType = x.ProductType.Name
            }).ToList();

            productsDt = products.ToDataTable();

            productType = db.ProductTypes.Select(x => x);

            producttypeDt = productType.ToDataTable<ProductType>();

            cmbCategory.DataSource = producttypeDt;
            //Specify Display Member and Value Member for Combobox
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Name";
            txtRate.Text = "0.00";

            dgvProducts.DataSource = productsDt;
            selectedProductId = 0;
            Clear();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            ReloadForm();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(cmbCategory.Text))
            {
                MessageBox.Show(Resources.FrmProducts_btnAdd_Click_Fill_in_mandatory_fields);
                return;
            }

            Submit();
        }

        private void Submit(bool isActive = true)
        {
            var product = new Product();
            var productTypeObject = new ProductType
                {Id = productType.Where(x => x.Name.Equals(cmbCategory.Text)).Select(x => x.Id).FirstOrDefault()};
            var user = new User {Id = 1}; // TO DO : implement user audit

            product.Id = selectedProductId;
            product.Name = txtName.Text;
            product.ProductType = productTypeObject;
            product.Description = txtDescription.Text;
            product.Rate = string.IsNullOrEmpty(txtRate.Text) ? 0 : decimal.Parse(txtRate.Text);
            product.Qty = 0; // TO DO : implement based on ask
            product.Added_Date = DateTime.Now;
            product.Added_By = user;
            product.IsActive = isActive;

            var bLl = new ProductsBll();
            bLl.SaveOrUpdate(product, db);
            ReloadForm();
        }

        public void Clear()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "0.00";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Create integer variable to know which product was clicked
            var rowIndex = e.RowIndex;
            //Display the Value on Respective TextBoxes
            selectedProductId = int.Parse(dgvProducts.Rows[rowIndex].Cells[0].Value.ToString());
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == 0)
            {
                MessageBox.Show(Resources.frmProducts_btnDelete_Click_Select_the_product_to_be_deleted);
                return;
            }

            Submit(false);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var keywords = txtSearch.Text;

            if (keywords != null)
            {
                var filteredProductResults = db.Products.Where(x => x.Name.Contains(keywords) && x.IsActive).Select(x =>
                    new ProductGrid
                    {
                        Id = x.Id, Name = x.Name, Description = x.Description, Rate = x.Rate, Qty = x.Qty,
                        IsActive = x.IsActive, ProductType = x.ProductType.Name
                    }).ToList();

                dgvProducts.DataSource = filteredProductResults.ToDataTable();
            }
            else
            {
                dgvProducts.DataSource = productsDt;
            }
        }
    }

    public class ProductGrid
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public string Description { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Qty { get; set; }
        public bool IsActive { get; set; }
    }
}