using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using BuddyBiller.BLL;

namespace BuddyBiller.DAL
{
    class TransactionDetailDal
    {
        //Create Connection String
        static string _myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Method for Transaction Detail
        public bool InsertTransactionDetail(TransactionDetailBll td)
        {
            //Create a boolean value and set its default value to false
            bool isSuccess = false;

            //Create a database connection here
            SqlConnection conn = new SqlConnection(_myconnstrng);

            try
            {
                //Sql Query to Insert Transaction detais
                string sql = "INSERT INTO tbl_transaction_detail (product_id, rate, qty, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                //Passing the value to the SQL Query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the values using cmd
                cmd.Parameters.AddWithValue("@product_id", td.ProductId);
                cmd.Parameters.AddWithValue("@rate", td.Rate);
                cmd.Parameters.AddWithValue("@qty", td.Qty);
                cmd.Parameters.AddWithValue("@total", td.Total);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.DeaCustId);
                cmd.Parameters.AddWithValue("@added_date", td.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", td.AddedBy);

                //Open Database connection
                conn.Open();

                //declare the int variable and execute the query
                int rows = cmd.ExecuteNonQuery();

                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
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
            return isSuccess;
        }
        #endregion
    }
}
