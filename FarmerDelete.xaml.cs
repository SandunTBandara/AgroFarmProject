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
using System.Data;
using System.Data.SqlClient;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for FarmerDelete.xaml
    /// </summary>
    public partial class FarmerDelete : Window
    {
        public FarmerDelete()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet ds;

        UpdateDatabase db = new UpdateDatabase();

        String constring = ("Data Source=laptop-4afh7sre;Initial Catalog=OrderBitSystem;Integrated Security=True");


        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("once you click delete button your account will be deleted permanantly. Do you want to delete your account? ", "Infomation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int m = db.updateDelete("Delete from AcceptedBids where fno='" + UpdateDatabase.id + "';");
                    int l = db.updateDelete("Delete from noOfBidsforEachOrder where fno='" + UpdateDatabase.id + "';");
                    int k = db.updateDelete("Delete from userImages where id='" + UpdateDatabase.id + "';");
                    int j = db.updateDelete("Delete from bits_for_orders where fno='" + UpdateDatabase.id + "';");
                    int i = db.updateDelete("Delete from farmerRegistration where fid='" + UpdateDatabase.id + "';");



                    if (i == 1)
                    {
                        MessageBox.Show("Your account is deleted! Please log out from your account", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        // write codes to log out
                        Close();
                       
                    }
                    else
                    {
                        MessageBox.Show("Your account did not deleted!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                        
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Some Database Error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Some Error occured!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

     
        

        private void btn_no_Click(object sender, RoutedEventArgs e)
        {
            Close();
            
        }
    }
}
