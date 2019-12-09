using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BuddyBiller.BLL;

namespace BuddyBiller.DAL
{
    class ProductsDal
    {
        //Creating STATI String Method for DB Connection
        static string _myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for Product Module
        public DataTable Select()
        {
            //Create Sql Connection to connect Databaes
            SqlConnection conn = new SqlConnection(_myconnstrng);

            //DAtaTable to hold the data from database
            DataTable dt = new DataTable();

            try
            {
                //Writing the Query to Select all the products from database
                String sql = "SELECT * FROM tbl_products";

                //Creating SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SQL Data Adapter to hold the value from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open DAtabase Connection
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
        #region Method to Insert Product in database
        public bool Insert(ProductsBll p)
        {
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(_myconnstrng);

            try
            {
                //SQL Query to insert product into database
                String sql = "INSERT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passign the values through parameters
                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@category", p.Category);
                cmd.Parameters.AddWithValue("@description", p.Description);
                cmd.Parameters.AddWithValue("@rate", p.Rate);
                cmd.Parameters.AddWithValue("@qty", p.Qty);
                cmd.Parameters.AddWithValue("@added_date", p.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", p.AddedBy);

                //Opening the Database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the query is executed successfully then the value of rows will be greater than 0 else it will be less than 0
                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //FAiled to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region Method to Update Product in Database
        public bool Update(ProductsBll p)
        {
            //create a boolean variable and set its initial value to false
            bool isSuccess = false;

            //Create SQL Connection for DAtabase
            SqlConnection conn = new SqlConnection(_myconnstrng);

            try
            {
                //SQL Query to Update Data in dAtabase
                String sql = "UPDATE tbl_products SET name=@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by WHERE id=@id";

                //Create SQL Cmmand to pass the value to query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the values using parameters and cmd
                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@category", p.Category);
                cmd.Parameters.AddWithValue("@description", p.Description);
                cmd.Parameters.AddWithValue("@rate", p.Rate);
                cmd.Parameters.AddWithValue("@qty", p.Qty);
                cmd.Parameters.AddWithValue("@added_date", p.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", p.AddedBy);
                cmd.Parameters.AddWithValue("@id", p.Id);

                //Open the Database connection
                conn.Open();

                //Create Int Variable to check if the query is executed successfully or not
                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the value of rows will be greater than 0 else it will be less than zero
                if(rows>0)
                {
                    //Query ExecutedSuccessfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region Method to Delete Product from Database
        public bool Delete(ProductsBll p)
        {
            //Create Boolean Variable and Set its default value to false
            bool isSuccess = false;

            //SQL Connection for DB connection
            SqlConnection conn = new SqlConnection(_myconnstrng);

            try
            {
                //Write Query Product from DAtabase
                String sql = "DELETE FROM tbl_products WHERE id=@id";

                //Sql Command to Pass the Value
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the values using cmd
                cmd.Parameters.AddWithValue("@id", p.Id);

                //Open Database Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //If the query is executed successfullly then the value of rows will be greated than 0 else it will be less than 0
                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region SEARCH Method for Product Module
        public DataTable Search (string keywords)
        {
            //SQL Connection fro DB Connection
            SqlConnection conn = new SqlConnection(_myconnstrng);
            //Creating DAtaTable to hold value from dAtabase
            DataTable dt = new DataTable();

            try
            {
                //SQL query to search product
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%"+keywords+"%' OR name LIKE '%"+keywords+"%' OR category LIKE '%"+keywords+"%'";
                //Sql Command to execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SQL Data Adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
        #region METHOD TO SEARCH PRODUCT IN TRANSACTION MODULE
        public ProductsBll GetProductsForTransaction(string keyword)
        {
            //Create an object of productsBLL and return it
            ProductsBll p = new ProductsBll();
            //SqlConnection
            SqlConnection conn = new SqlConnection(_myconnstrng);
            //Datatable to store data temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write the Query to Get the detaisl
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";
                //Create Sql Data Adapter to Execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //Open DAtabase Connection
                conn.Open();

                //Pass the value from adapter to dt
                adapter.Fill(dt);

                //If we have any values on dt then set the values to productsBLL
                if(dt.Rows.Count>0)
                {
                    p.Name = dt.Rows[0]["name"].ToString();
                    p.Rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.Qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return p;
        }
        #endregion
        #region METHOD TO GET PRODUCT ID BASED ON PRODUCT NAME
        public ProductsBll GetProductIdFromName(string productName)
        {
            //First Create an Object of DeaCust BLL and REturn it
            ProductsBll p = new ProductsBll();

            //SQL Conection here
            SqlConnection conn = new SqlConnection(_myconnstrng);
            //Data TAble to Holdthe data temporarily
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Get id based on Name
                string sql = "SELECT id FROM tbl_products WHERE name='" + productName + "'";
                //Create the SQL Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                //Passing the CAlue from Adapter to DAtatable
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    p.Id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return p;
        }
        #endregion
        #region METHOD TO GET CURRENT QUantity from the Database based on Product ID
        public decimal GetProductQty(int productId)
        {
            //SQl Connection First
            SqlConnection conn = new SqlConnection(_myconnstrng);
            //Create a Decimal Variable and set its default value to 0
            decimal qty = 0;

            //Create Data Table to save the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write WQL Query to Get Quantity from Database
                string sql = "SELECT qty FROM tbl_products WHERE id = "+productId;

                //Cerate A SqlCommand
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create a SQL Data Adapter to Execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open DAtabase Connection
                conn.Open();

                //PAss the calue from Data Adapter to DataTable
                adapter.Fill(dt);

                //Lets check if the datatable has value or not
                if(dt.Rows.Count>0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return qty;
        }
        #endregion
        #region METHOD TO UPDATE QUANTITY
        public bool UpdateQuantity(int productId, decimal qty)
        {
            //Create a Boolean Variable and Set its value to false
            bool success = false;

            //SQl Connection to Connect Database
            SqlConnection conn = new SqlConnection(_myconnstrng);

            try
            {
                //Write the SQL Query to Update Qty
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";

                //Create SQL Command to Pass the calue into Queyr
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the VAlue trhough parameters
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@id", productId);

                //Open Database Connection
                conn.Open();

                //Create Int Variable and Check whether the query is executed Successfully or not
                int rows = cmd.ExecuteNonQuery();
                //Lets check if the query is executed Successfully or not
                if(rows>0)
                {
                    //Query Executed Successfully
                    success = true;
                }
                else
                {
                    //Failed to Execute Query
                    success = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }
        #endregion
        #region METHOD TO INCREASE PRODUCT
        public bool IncreaseProduct(int productId, decimal increaseQty)
        {
            //Create a Boolean Variable and SEt its value to False
            bool success = false;

            //Create SQL Connection To Connect DAtabase
            SqlConnection conn = new SqlConnection(_myconnstrng);

            try
            {
                //Get the Current Qty From dAtabase based on id
                decimal currentQty = GetProductQty(productId);

                //Increase the Current Quantity by the qty purchased from Dealer
                decimal newQty = currentQty + increaseQty;

                //Update the Prudcty Quantity Now
                success = UpdateQuantity(productId, newQty);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region METHOD TO DECREASE PRODUCT
        public bool DecreaseProduct(int productId, decimal qty)
        {
            //Create Boolean Variable and SEt its Value to false
            bool success = false;

            SqlConnection conn = new SqlConnection(_myconnstrng);

            try
            {
                //Get the Current product Quantity
                decimal currentQty = GetProductQty(productId);

                //Decrease the Product Quantity based on product sales
                decimal newQty = currentQty - qty;

                //Update Product in Database
                success = UpdateQuantity(productId, newQty);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region DESPLAY PRODUCTS BASED ON CATEGORIES
        public DataTable DisplayProductsByCategory(string category)
        {
            //Sql Connection First
            SqlConnection conn = new SqlConnection(_myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Display Product Based on CAtegory
                string sql = "SELECT * FROM tbl_products WHERE category='"+category+"'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection Here
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
    }
}
