using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BuddyBiller.BLL;

namespace BuddyBiller.DAL
{
    class UserDal
    {
        static string _myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Data from Database
        public DataTable Select()
        {
            //Static MEthod to connect Database
            SqlConnection conn = new SqlConnection(_myconnstrng);
            //TO hold the data from database 
            DataTable dt = new DataTable();
            try
            {
                //SQL Query to Get Data From DAtabase
                String sql = "SELECT * FROM tbl_users";
                //For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Getting DAta from dAtabase
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Database Connection Open
                conn.Open();
                //Fill Data in our DataTable
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
            return dt;
        }
        #endregion
        #region Insert Data in Database
        public bool Insert(UserBll u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(_myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_users (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.FirstName);
                cmd.Parameters.AddWithValue("@last_name", u.LastName);
                cmd.Parameters.AddWithValue("@email", u.Email);
                cmd.Parameters.AddWithValue("@username", u.Username);
                cmd.Parameters.AddWithValue("@password", u.Password);
                cmd.Parameters.AddWithValue("@contact", u.Contact);
                cmd.Parameters.AddWithValue("@address", u.Address);
                cmd.Parameters.AddWithValue("@gender", u.Gender);
                cmd.Parameters.AddWithValue("@user_type", u.UserType);
                cmd.Parameters.AddWithValue("@added_date", u.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", u.AddedBy);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the query is executed Successfully then the value to rows will be greater than 0 else it will be less than 0
                if(rows>0)
                {
                    //Query Sucessfull
                    isSuccess = true;
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
        #region Update data in Database
        public bool Update(UserBll u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(_myconnstrng);
            
            try
            {
                string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, address=@address, gender=@gender, user_type=@user_type, added_date=@added_date, added_by=@added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.FirstName);
                cmd.Parameters.AddWithValue("@last_name", u.LastName);
                cmd.Parameters.AddWithValue("@email", u.Email);
                cmd.Parameters.AddWithValue("@username", u.Username);
                cmd.Parameters.AddWithValue("@password", u.Password);
                cmd.Parameters.AddWithValue("@contact", u.Contact);
                cmd.Parameters.AddWithValue("@address", u.Address);
                cmd.Parameters.AddWithValue("@gender", u.Gender);
                cmd.Parameters.AddWithValue("@user_type", u.UserType);
                cmd.Parameters.AddWithValue("@added_date", u.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", u.AddedBy);
                cmd.Parameters.AddWithValue("@id", u.Id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    //Query Successfull
                    isSuccess = true;
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
        #region Delete Data from DAtabase
        public bool Delete(UserBll u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(_myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", u.Id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    //Query Successfull
                    isSuccess = true;
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
        #region Search User on Database usingKeywords
        public DataTable Search(string keywords)
        {
            //Static MEthod to connect Database
            SqlConnection conn = new SqlConnection(_myconnstrng);
            //TO hold the data from database 
            DataTable dt = new DataTable();
            try
            {
                //SQL Query to Get Data From DAtabase
                String sql = "SELECT * FROM tbl_users WHERE id LIKE '%"+keywords+"%' OR first_name LIKE '%"+keywords+"%' OR last_name LIKE '%"+keywords+"%' OR username LIKE '%"+keywords+"%'";
                //For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Getting DAta from dAtabase
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Database Connection Open
                conn.Open();
                //Fill Data in our DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
            return dt;
        }
        #endregion
        #region Getting User ID from Username
        public UserBll GetIdFromUsername (string username)
        {
            UserBll u = new UserBll();
            SqlConnection conn = new SqlConnection(_myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROM tbl_users WHERE username='"+username+"'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();

                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    u.Id = int.Parse(dt.Rows[0]["id"].ToString());
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
            return u;
        }
        #endregion
    }
}
