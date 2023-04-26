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
using System.Drawing;
using System.Data;
using System.Data.SqlClient;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for FarmerDetails.xaml
    /// </summary>
    public partial class FarmerDetails : UserControl
    {
        public FarmerDetails()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            farmerreport obj = new farmerreport();
            obj.Show();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True"))
            {
                con.Open();
                SqlDataAdapter sad = new SqlDataAdapter("SELECT fid,name,address,tel,email,type FROM farmerRegistration", con);
                DataTable dt = new DataTable();
                sad.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                con.Close();
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}