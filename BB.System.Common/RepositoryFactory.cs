using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.System.Common
{
    public class RepositoryFactory
    {

        public static SqlConnection RepositoryConnectionBuilder()
        {

            string myconnstrng = ConfigurationManager.ConnectionStrings["temp"].ConnectionString;

             SqlConnection conn = new SqlConnection(myconnstrng);

            BuddyBillerRepository db = new BuddyBillerRepository();


            return conn;
        }

    }
}
