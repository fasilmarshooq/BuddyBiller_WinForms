using System.Configuration;
using System.Data.SqlClient;

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
