using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace cwainmenuexs1
{
    class OrderBidSystemDatabse
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        public static string b;
        public static string f;




        public OrderBidSystemDatabse()
        {
            con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");
        }



        public string getID(string q, string name)
        {
            string id = name;
            int rowno = 0, count = 0;



            con.Open();
            adpt = new SqlDataAdapter(q, con);
            dt = new DataTable();
            adpt.Fill(dt);
            int i = dt.Rows.Count;



            if (i != 0)
            {
                rowno = i - 1;
                count = Convert.ToInt32(dt.Rows[rowno]["id"]);
                Console.WriteLine(rowno + count);
                count = count + 1;
            }
            else
            {
                count = 1;
            }



            if (count < 10)
            {
                id += "00" + count;
            }
            else if (count < 100)
            {
                id += "0" + count;
            }
            else if (count < 1000)
            {
                id += "" + count;
            }
            con.Close();
            return id.ToString();
        }



        public int DataInsertUpdateDelete(String q)
        {
            con.Open();
            cmd = new SqlCommand(q, con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }



        public DataTable DisplayOnGrid(string q)
        {
            con.Open();
            adpt = new SqlDataAdapter(q, con);
            dt = new DataTable();
            adpt.Fill(dt);
            con.Close();
            return dt;
        }



        public DataTable getDataSet(string q)
        {
            con.Open();
            adpt = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            DataTable dt = ds.Tables[0];



            con.Close();
            return dt;
        }

     
    }
}
