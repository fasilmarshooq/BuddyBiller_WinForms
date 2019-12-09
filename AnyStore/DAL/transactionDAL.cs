using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BB.System.Common;
using BuddyBiller.BLL;

namespace BuddyBiller.DAL
{
    class TransactionDal
    {
        SqlConnection conn = RepositoryFactory.RepositoryConnectionBuilder();

        #region Insert Transaction Method
        public bool Insert_Transaction(TransactionsBll t, out int transactionId)
        {
            //Create a boolean value and set its default value to false
            bool isSuccess = false;
            //Set the out transactionID value to negative 1 i.e. -1
            transactionId = -1;
            //Create a SqlConnection first
            try
            {
                //SQL Query to Insert Transactions
                string sql = "INSERT INTO tbl_transactions (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY;";

                //Sql Commandto pass the value in sql query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the value to sql query using cmd
                cmd.Parameters.AddWithValue("@type", t.Type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.DeaCustId);
                cmd.Parameters.AddWithValue("@grandTotal", t.GrandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.TransactionDate);
                cmd.Parameters.AddWithValue("@tax", t.Tax);
                cmd.Parameters.AddWithValue("@discount", t.Discount);
                cmd.Parameters.AddWithValue("@added_by", t.AddedBy);

                //Open Database Connection
                conn.Open();

                //Execute the Query
                object o = cmd.ExecuteScalar();

                //If the query is executed successfully then the value will not be null else it will be null
                if(o!=null)
                {
                    //Query Executed Successfully
                    transactionId = int.Parse(o.ToString());
                    isSuccess = true;
                }
                else
                {
                    //failed to execute query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close the connection 
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region METHOD for reporting transactions
        public DataTable DisplayTransactionReport(string transactionTypetype)
        {
            using (SqlConnection conn = RepositoryFactory.RepositoryConnectionBuilder())
            {
                DataTable dt = new DataTable();

                try
                {
                    SqlCommand cmd = new SqlCommand("TransactionSummaryReport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Passing the calues using Parameters
                    cmd.Parameters.AddWithValue("@type", transactionTypetype);
                    //SqlDataAdapter to Hold the data from database
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conn.Open();
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                return dt;
            }
        }
        #endregion

    }
}
