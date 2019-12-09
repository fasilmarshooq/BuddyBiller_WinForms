using System;
using System.Linq;
using System.Windows.Forms;
using BuddyBiller.BLL;

namespace BuddyBiller.DAL
{
    class LoginDal
    {


        public bool loginCheck(LoginBll l)
        {
            //Create a boolean variable and set its value to false and return it
            bool isSuccess = false;

            try
            {
                using (BuddyBillerRepository db = new BuddyBillerRepository())
                {

                    var usercontext = from user in db.Users
                                      where user.username.Equals(l.Username)
                                      && user.password.Equals(l.Password)
                                      && user.user_type.Equals(l.UserType)
                                      select user;

                    if (usercontext.Any())
                    {
                        //Login Sucessful
                        isSuccess = true;
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
