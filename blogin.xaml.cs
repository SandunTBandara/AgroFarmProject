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
    /// Interaction logic for blogin.xaml
    /// </summary>
    public partial class blogin : Window
    {
        public string myval = null;
        public blogin()
        {
            InitializeComponent();
        }
        SqlConnection Con;
        SqlCommand com;

        private void Btn_signup_Click(object sender, RoutedEventArgs e)
        {
            Buyer obj = new Buyer();
            obj.Show();
        }

        private void Btn_flogin_Click(object sender, RoutedEventArgs e)
        {
            Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");

            try
            {
                if (Con.State == System.Data.ConnectionState.Closed)
                    Con.Open();
                
                com = new SqlCommand("select count(1)  from buyerRegistration where fid=@fid and password=@password", Con);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@fid", txt_funame.Text);
                com.Parameters.AddWithValue("@password", txt_fpassword.Password);
                int count = Convert.ToInt32(com.ExecuteScalar());
                if(count == 1)
                {
                    BuyerHome obj = new BuyerHome();
                    obj.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("username or password is incorrect");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }

            myval = txt_funame.Text;
            Con.Open();
            com = new SqlCommand("update BuyerRegistrationtemp set fid = '" + myval + "' where id = 001   ", Con);
            com.ExecuteNonQuery();
            Con.Close();
            com.Dispose();
            OrderBidSystemDatabse.b = myval;
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
