using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BB.System.Common;
using BuddyBiller.BLL;

namespace BuddyBiller.DAL
{
    class DeaCustDal
    {

        SqlConnection conn = RepositoryFactory.RepositoryConnectionBuilder();

        #region SELECT MEthod for Dealer and Customer
        public DataTable Select(String keyword = "")
        {


            //DataTble to hold the value from database and return it
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Query t Select all the DAta from dAtabase
                string sql = "SELECT * FROM tbl_dea_cust where name LIKE '%" + keyword + "%'";

                //Creating SQL Command to execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Creting SQL Data Adapter to Store Data From Database Temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();
                //Passign the value from SQL Data Adapter to DAta table
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
        #region INSERT Method to Add details fo Dealer or Customer
        public bool Insert(DeaCustBll dc)
        {

            using (SqlConnection repositoryConnectionBuilder = RepositoryFactory.RepositoryConnectionBuilder())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("SaveCustomers", repositoryConnectionBuilder);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Passing the calues using Parameters
                    cmd.Parameters.AddWithValue("@type", dc.Type);
                    cmd.Parameters.AddWithValue("@name", dc.Name);
                    cmd.Parameters.AddWithValue("@email", dc.Email);
                    cmd.Parameters.AddWithValue("@contact", dc.Contact);
                    cmd.Parameters.AddWithValue("@address", dc.Address);
                    cmd.Parameters.AddWithValue("@added_by", dc.AddedBy);

                    //Open DAtabaseConnection
                    repositoryConnectionBuilder.Open();

                    //Int variable to check whether the query is executed successfully or not
                    var success = cmd.ExecuteNonQuery();

                    if (success == 0)
                        return false;
                    else
                        return true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    repositoryConnectionBuilder.Close();
                }
            }

              
        }
        #endregion
        #region UPDATE method for Dealer and Customer Module
        public bool Update(DeaCustBll dc)
        {

            //Create Boolean variable and set its default value to false
            bool isSuccess = false;

            try
            {
                //SQL Query to update data in database
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by WHERE id=@id";
                //Create SQL Command to pass the value in sql
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the values through parameters
                cmd.Parameters.AddWithValue("@type", dc.Type);
                cmd.Parameters.AddWithValue("@name", dc.Name);
                cmd.Parameters.AddWithValue("@email", dc.Email);
                cmd.Parameters.AddWithValue("@contact", dc.Contact);
                cmd.Parameters.AddWithValue("@address", dc.Address);
                cmd.Parameters.AddWithValue("@added_date", dc.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", dc.AddedBy);
                cmd.Parameters.AddWithValue("@id", dc.Id);

                //open the Database Connection
                conn.Open();

                //Int varialbe to check if the query executed successfully or not
                int rows = cmd.ExecuteNonQuery();
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
        #region DELETE Method for Dealer and Customer Module
        public bool Delete(DeaCustBll dc)
        {

            //Create a Boolean Variable and set its default value to false
            bool isSuccess = false;

            try
            {
                //SQL Query to Delete Data from dAtabase
                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                //SQL command to pass the value
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the value
                cmd.Parameters.AddWithValue("@id", dc.Id);

                //Open DB Connection
                conn.Open();
                //integer variable
                int rows = cmd.ExecuteNonQuery();
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
        #region SEARCH METHOD for Dealer and Customer Module
        public DataTable Search(string keyword)
        {

            //Creating a Data TAble and returnign its value
            DataTable dt = new DataTable();

            try
            {
                //Write the Query to Search Dealer or Customer Based in id, type and name
                string sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%"+keyword+"%' OR type LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";

                //Sql Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Sql Dat Adapeter to hold tthe data from dataase temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open DAta Base Connection
                conn.Open();
                //Pass the value from adapter to data table
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
        #region METHOD TO SAERCH DEALER Or CUSTOMER FOR TRANSACTON MODULE
        public DeaCustBll SearchDealerCustomerForTransaction(string keyword)
        {
            //Create an object for DeaCustBLL class
            DeaCustBll dc = new DeaCustBll();


            //Create a DAta Table to hold the value temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write a SQL Query to Search Dealer or Customer Based on Keywords
                string sql = "SELECT name, email, contact, address from tbl_dea_cust WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";

                //Create a Sql Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //Open the DAtabase Connection
                conn.Open();

                //Transfer the data from SqlData Adapter to DAta Table
                adapter.Fill(dt);

                //If we have values on dt we need to save it in dealerCustomer BLL
                if(dt.Rows.Count>0)
                {
                    dc.Name = dt.Rows[0]["name"].ToString();
                    dc.Email = dt.Rows[0]["email"].ToString();
                    dc.Contact = dt.Rows[0]["contact"].ToString();
                    dc.Address = dt.Rows[0]["address"].ToString();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database connection
                conn.Close();
            }

            return dc;
        }
        #endregion
        #region METHOD TO GET ID OF THE DEALER OR CUSTOMER BASED ON NAME
        public DeaCustBll GetDeaCustIdFromName(string name)
        {
            //First Create an Object of DeaCust BLL and REturn it
            DeaCustBll dc = new DeaCustBll();

            //Data TAble to Holdthe data temporarily
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Get id based on Name
                string sql = "SELECT id FROM tbl_dea_cust WHERE name='"+name+"'";
                //Create the SQL Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                //Passing the CAlue from Adapter to DAtatable
                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    dc.Id = int.Parse(dt.Rows[0]["id"].ToString());
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

            return dc;
        }
        #endregion

    }
}
