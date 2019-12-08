using AnyStore.BLL;
using AnyStore.DAL;
using BB.System.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmProductTypes: Form
    {
        BuddyBillerRepository db = new BuddyBillerRepository();
        DataTable ProductTypedt;
        int selectedProductTypeId;

        public frmProductTypes()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
     
        private void frmCategories_Load(object sender, EventArgs e)
        {
            reloadForm();
        }

        private void reloadForm()
        {
            Clear();
            var productTypes = db.ProductTypes.Where(t=>t.IsActive).Select(X => X);

            ProductTypedt = DataSetLinqOperators.ToDataTable<ProductType>(productTypes);            
            grdProductTypes.DataSource = ProductTypedt;

            txtProductTypeId.Visible = false;
        }

        private void Clear()
        {
            txtProductTypeId.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
        }

        private void grdProductTypes_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Finding the Row Index of the Row Clicked on Data Grid View
            int RowIndex = e.RowIndex;
            selectedProductTypeId = int.Parse(grdProductTypes.Rows[RowIndex].Cells[0].Value.ToString());
            txtProductTypeId.Text = grdProductTypes.Rows[RowIndex].Cells[0].Value.ToString();
            txtName.Text = grdProductTypes.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = grdProductTypes.Rows[RowIndex].Cells[2].Value.ToString();
        }
       
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ////Get the Keywords
            //string keywords = txtSearch.Text;

            ////Filte the categories based on keywords
            //if(keywords!=null)
            //{
            //    //Use Searh Method To Display Categoreis
            //    DataTable dt = dal.Search(keywords);
            //    grdProductTypes.DataSource = dt;
            //}
            //else
            //{
            //    //Use Select Method to Display All Categories
            //    DataTable dt = dal.Select();
            //    grdProductTypes.DataSource = dt;
            //}
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from Categroy Form

            var productType = new ProductType();

            productType.Id=selectedProductTypeId;
            productType.Name = txtName.Text;
            productType.Description = txtDescription.Text;
            productType.Added_Date = DateTime.Now;
            productType.IsActive=true;

            //Creating Boolean Method To insert data into database
            bool success = SaveOrUpdate(productType);;
            reloadForm();
            //If the category is inserted successfully then the value of the success will be true else it will be false
            if(success==true)
            {
                //NewCAtegory Inserted Successfully
                MessageBox.Show("New Product Type Inserted/Updated Successfully.");                           
            }
            else
            {
                //FAiled to Insert New Category
                MessageBox.Show("Failed to Insert New Product Type.");
            }
             
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var productType = new ProductType();

            productType.Id=selectedProductTypeId;
            productType.Name = txtName.Text;
            productType.Description = txtDescription.Text;
            productType.Added_Date = DateTime.Now;
            productType.IsActive=false;

            //Creating Boolean Method To insert data into database
            bool success = SaveOrUpdate(productType);;
            reloadForm();
        }

        private bool SaveOrUpdate(ProductType entity)
        {
            var sql = @"MERGE INTO ProductTypes
               USING (VALUES (@Id,@Name,@Description,@Added_Date,@Added_By_Id,@IsActive)) AS
                            s(Id,Name,Description,Added_Date,Added_By_Id,IsActive)
                ON ProductTypes.id = s.Id
                WHEN MATCHED THEN
                    UPDATE
                    SET      Name=s.Name
                            ,Description=s.Description                            
                            ,Added_Date=s.Added_Date
                            ,Added_By_Id=s.Added_By_Id
                            ,IsActive=s.IsActive
                WHEN NOT MATCHED THEN
                    INSERT (Name,Description,Added_Date,Added_By_Id,IsActive)
                    VALUES (s.NAme,s.Description,s.Added_Date,s.Added_By_Id,s.IsActive);";

            object[] parameters = {
                new SqlParameter("@id", entity.Id),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@Added_Date", entity.Added_Date),
                new SqlParameter("@Added_By_Id", '1'), // TO:DO extend to stamp user session id
                new SqlParameter("@IsActive", entity.IsActive)
            };
            return db.Database.ExecuteSqlCommand(sql, parameters)==1;
        }

    }
}
