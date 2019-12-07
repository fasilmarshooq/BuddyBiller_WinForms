using AnyStore.BLL;
using BB.System.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class loginDAL
    {


        public bool loginCheck(loginBLL l)
        {
            //Create a boolean variable and set its value to false and return it
            bool isSuccess = false;

            try
            {
                using (BuddyBillerRepository db = new BuddyBillerRepository())
                {

                    var usercontext = from user in db.users
                                      where user.username.Equals(l.username)
                                      && user.password.Equals(l.password)
                                      && user.user_type.Equals(l.user_type)
                                      select user;

                    if (usercontext.Any())
                    {
                        //Login Sucessful
                        isSuccess = true;
                    }
                    else
                    {
                        //Login Failed
                        isSuccess = false;
                    }
                }





                //    //SQL Query to check login
                //string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";

                ////Creating SQL Command to pass value
                //SqlCommand cmd = new SqlCommand(sql, conn);

                ////cmd.Parameters.AddWithValue("@username", l.username);
                ////cmd.Parameters.AddWithValue("@password", l.password);
                ////cmd.Parameters.AddWithValue("@user_type", l.user_type);

                //SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //DataTable dt = new DataTable();

                //adapter.Fill(dt);

                ////Checking The rows in DataTable 
                //if(dt.Rows.Count>0)
                //{
                //    //Login Sucessful
                //    isSuccess = true;
                //}
                //else
                //{
                //    //Login Failed
                //    isSuccess = false;
                //}
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
     

            return isSuccess;
        }
    }
}
