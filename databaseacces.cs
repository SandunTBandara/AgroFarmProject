using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace cwainmenuexs1
{
    class databaseacces
    {
       

            SqlConnection con;
            SqlCommand cmd;
            SqlDataAdapter adpt;
            public void load()
            {
                con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");
            }

            public int insert_data(string a)
            {
                con.Open();
                cmd = new SqlCommand(a, con);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }

            public int get_farmerId(string a)
            {
            string val = null;
                con.Open();
                adpt = new SqlDataAdapter(a, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                 con.Close();
                int c = dt.Rows.Count;
            for (int i = 0 ;i < dt.Rows.Count; i++)
            {
                val = dt.Rows[i]["fid"].ToString();
            }
                string id = val[1].ToString() + val[2].ToString() + val[3].ToString();
                return Convert.ToInt32(id);

            }
       
        

    }
}

