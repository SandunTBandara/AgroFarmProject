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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for FLogin.xaml
    /// </summary>
    public partial class FLogin : Window
    {
        public string myval=null;


        public FLogin()
        {
            InitializeComponent();
            
        }
        
        SqlConnection con;
        SqlCommand com;

        private void Btn_signup_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Farmer_Registration obj = new Farmer_Registration();
            obj.Show();

        }

        private void Btn_flogin_Click(object sender, RoutedEventArgs e)
        {
            

            con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");

            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                
                com = new SqlCommand("select count(1)  from farmerRegistration where fid=@fid and password=@password", con);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@fid", txt_funame.Text);
                com.Parameters.AddWithValue("@password", txt_fpassword.Password);
                
                int count = Convert.ToInt32(com.ExecuteScalar());
                if (count == 1)
                {
                    FArmerHome obj = new FArmerHome();
                    obj.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("username or password is incorrect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            //update my tempory table to fid for future reuse
            myval = txt_funame.Text;
            con.Open();
            com = new SqlCommand("update farmerRegistrationtemp set fid = '"+myval+"' where id = 001   ", con);
            com.ExecuteNonQuery();
            con.Close();
            com.Dispose();

            OrderBidSystemDatabse.f = myval;
            UpdateDatabase.id = myval;
           
           
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Buttopower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow obj = new MainWindow();
            obj.Show();
        }
    }
}
