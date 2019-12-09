using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BB.System.Common;
using BuddyBiller.BLL;

namespace BuddyBiller.DAL
{
    class CategoriesDal
    {
        //Static String Method for Database Connection String

        SqlConnection conn = RepositoryFactory.RepositoryConnectionBuilder();

        #region Select Method
        public DataTable Select()
        {
            //Creating Database Connection

            DataTable dt = new DataTable();

            try
            {
                //Wrting SQL Query to get all the data from DAtabase
                string sql = "SELECT * FROM tbl_categories";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Open DAtabase Connection
                conn.Open();
                //Adding the value from adapter to Data TAble dt
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
        #region Insert New CAtegory
        public bool Insert(CategoriesBll c)
        {
            //Creating A Boolean VAriable and set its default value to false
            bool isSucces = false;

            //Connecting to Database


            try
            {
                //Writing Query to Add New Category
                string sql = "INSERT INTO tbl_categories (title, description, added_date, added_by) VALUES (@title, @description, @added_date, @added_by)";

                //Creating SQL Command to pass values in our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing Values through parameter
                cmd.Parameters.AddWithValue("@title", c.Title);
                cmd.Parameters.AddWithValue("@description", c.Description);
                cmd.Parameters.AddWithValue("@added_date", c.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", c.AddedBy);

                //Open Database Connection
                conn.Open();

                //Creating the int variable to execute query
                int rows = cmd.ExecuteNonQuery();

                //If the query is executed successfully then its value will be greater than 0 else it will be less than 0

                if(rows>0)
                {
                    //Query Executed Succesfully
                    isSucces = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSucces = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Database Connection
                conn.Close();
            }

            return isSucces;
        }
        #endregion
        #region Update Method
        public bool Update(CategoriesBll c)
        {
            //Creating Boolean variable and set its default value to false
            bool isSuccess = false;

            //Creating SQL Connection
          

            try
            {
                //Query to Update Category
                string sql = "UPDATE tbl_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";

                //SQl Command to Pass the Value on Sql Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Value using cmd
                cmd.Parameters.AddWithValue("@title", c.Title);
                cmd.Parameters.AddWithValue("@description", c.Description);
                cmd.Parameters.AddWithValue("@added_date", c.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", c.AddedBy);
                cmd.Parameters.AddWithValue("@id", c.Id);

                //Open DAtabase Connection
                conn.Open();

                //Create Int Variable to execute query
                int rows = cmd.ExecuteNonQuery();

                //if the query is successfully executed then the value will be grater than zero 
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
        #region Delete Category Method
        public bool Delete(CategoriesBll c)
        {
            //Create a Boolean variable and set its value to false
            bool isSuccess = false;

           

            try
            {
                //SQL Query to Delete from Database
                string sql = "DELETE FROM tbl_categories WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the value using cmd
                cmd.Parameters.AddWithValue("@id", c.Id);

                //Open SqlConnection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the query is executd successfully then the value of rows will be greater than zero else it will be less than 0
                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Faied to Execute Query
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
        #region Method for Searh Funtionality
        public DataTable Search(string keywords)
        {
            //SQL Connection For Database Connection
   

            //Creating Data TAble to hold the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //SQL Query To Search Categories from DAtabase
                String sql = "SELECT * FROM tbl_categories WHERE id LIKE '%"+keywords+"%' OR title LIKE '%"+keywords+"%' OR description LIKE '%"+keywords+"%'";
                //Creating SQL Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Getting DAta From DAtabase
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open DatabaseConnection
                conn.Open();
                //Passing values from adapter to Data Table dt
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
