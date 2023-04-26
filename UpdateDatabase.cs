using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace cwainmenuexs1
{
    class UpdateDatabase
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;

        public static string id;

        public UpdateDatabase()
        {
            con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");
        }



        public DataTable loadData(string a)
        {
            con.Open();
            adpt = new SqlDataAdapter(a, con);
            dt = new DataTable();
            adpt.Fill(dt);
            con.Close();
            return dt;
        }



        public int updateDelete(string a)
        {
            con.Open();
            cmd = new SqlCommand(a, con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}
