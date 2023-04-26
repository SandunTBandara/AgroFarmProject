using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for UserControlBHome.xaml
    /// </summary>
    public partial class UserControlBHome : UserControl
    {

        public string selectid;


        SqlConnection Con;
        SqlCommand Com;
        SqlDataReader dr;
       

        public UserControlBHome()
        {
           
            InitializeComponent();

            Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");
           
            //select tempory database fid and store in to my selectid variable
            Con.Open();
            Com = new SqlCommand("select fid from BuyerRegistrationtemp where Id = 001 ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    selectid = dr["fid"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            //diplay on to label my primary table data
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
            Con.Open();
            Com = new SqlCommand("select * from buyerRegistration where fid = '" + selectid + "' ", Con);
            dr = Com.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtBname.Text = dr["name"].ToString();
                    txt_Id.Text = dr["fid"].ToString();
                    txt_address.Text = dr["address"].ToString();
                    txt_tel.Text = dr["tel"].ToString();
                    txt_email.Text = dr["email"].ToString();
                    txtType.Text = dr["type"].ToString();
                }
                dr.Close();
                Con.Close();

               
            }
          



        }
    }
}
