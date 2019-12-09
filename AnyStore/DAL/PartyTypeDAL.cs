using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BB.System.Common;

namespace BuddyBiller.DAL
{
    class PartyTypeDal
    {

        SqlConnection conn = RepositoryFactory.RepositoryConnectionBuilder();

        public DataTable Select()
        {
            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM partytypeconfigs";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dt;
        }
    }
}
