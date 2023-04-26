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
    /// Interaction logic for MyOrderFarmer.xaml
    /// </summary>
    public partial class MyOrderFarmer : UserControl
    {

        public MyOrderFarmer()
        {
            InitializeComponent();

        
        }

        OrderBidSystemDatabse db = new OrderBidSystemDatabse();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                DataTable dt = db.DisplayOnGrid("select * from AcceptedBids where fno='" + OrderBidSystemDatabse.f + "';");
                DataGridView1.ItemsSource = dt.DefaultView;

                DataTable dt2 = db.DisplayOnGrid("Select * from lateBids where fno='" + OrderBidSystemDatabse.f + "';");
                DataGridViewLate.ItemsSource = dt2.DefaultView;

                if(dt.Rows.Count == 0)
                {
                    lbl_error_orders.Content = "You haven't received any orders yet";
                }
                else
                {
                    lbl_error_orders.Content = "";
                

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime? date = DateTime.Today;
                    DateTime? duedate = Convert.ToDateTime(dt.Rows[i]["dueDate"]);


                    TimeSpan? duration = duedate - date;
                    string days = duration.Value.Days.ToString();
              
                    if (Convert.ToInt32(days) < 0)
                    {
                        MessageBox.Show("Your order on Order no " + dt.Rows[i]["ono"].ToString() + " is Canceled because it's Out of due Date", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        int m = db.DataInsertUpdateDelete("delete from AcceptedBids where ono='" + dt.Rows[i]["ono"].ToString() + "';");
                        if (m == 1)
                        {
                            DataTable dt3 = db.DisplayOnGrid("select * from AcceptedBids  where fno='" + OrderBidSystemDatabse.f + "';");
                            DataGridView1.ItemsSource = dt.DefaultView;



                            DataTable dt4 = db.DisplayOnGrid("Select * from lateBids  where fno='" + OrderBidSystemDatabse.f + "';");
                            DataGridViewLate.ItemsSource = dt3.DefaultView;
                        }
                        
                    }
                }
                }

                if (dt.Rows.Count == 0)
                {
                    lbl_error_lates.Content = "No cancelled orders";
                }
                else
                {
                    lbl_error_lates.Content = "";

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

  
    }
}
