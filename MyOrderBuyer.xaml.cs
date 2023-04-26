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
using System.Data;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for MyOrderBuyer.xaml
    /// </summary>
    public partial class MyOrderBuyer : Window
    {
        public MyOrderBuyer()
        {
            InitializeComponent();
        }

        OrderBidSystemDatabse db = new OrderBidSystemDatabse();
        String ono, fno;
        private void form_loaded(object sender, RoutedEventArgs e)
        {
            try { 
            DataTable dt = db.DisplayOnGrid("select * from AcceptedBids where bno = '" + OrderBidSystemDatabse.b + "';"); // add 'where bno' to the query
            DataGridView1.ItemsSource = dt.DefaultView;

            DataTable dt2 = db.DisplayOnGrid("Select * from lateBids where bno = '" + OrderBidSystemDatabse.b + "';");
            DataGridViewLate.ItemsSource = dt2.DefaultView;

                if(dt.Rows.Count == 0)
                { 
                     lbl_error_Orders.Content = "You haven't any orders";
                  }
                else
                {
                lbl_error_Orders.Content = "";

                MessageBox.Show("Please wait! Your data is analyzing", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime? date = DateTime.Today;
                        DateTime? duedate = Convert.ToDateTime(dt.Rows[i]["dueDate"]);

                        TimeSpan? duration = duedate - date;
                        string days = duration.Value.Days.ToString();
                        DataGridCell cell = (DataGridView1.Columns[10].GetCellContent(DataGridView1.Items[i])).Parent as DataGridCell;
                        TextBlock txtb = new TextBlock();
                        txtb.Text = days + " Days";
                        cell.Content = txtb;
                        if (Convert.ToInt32(days) < 0)
                        {
                            MessageBox.Show("Your Bid on Your Order no " + dt.Rows[i]["ono"].ToString() + " is Canceled because it's Out of due Date", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            int m = db.DataInsertUpdateDelete("delete from AcceptedBids where ono='" + dt.Rows[i]["ono"].ToString() + "';");
                            if (m == 1)
                            {
                                DataTable dt3 = db.DisplayOnGrid("select * from AcceptedBids where bno = '" + OrderBidSystemDatabse.b + "';");
                                DataGridView1.ItemsSource = dt.DefaultView;

                                DataTable dt4 = db.DisplayOnGrid("Select * from lateBids where bno = '" + OrderBidSystemDatabse.b + "';");
                                DataGridViewLate.ItemsSource = dt4.DefaultView;
                            }
                            else
                            {

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

        private void DatagridView_selctionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DatagridViewLate_selctionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView row_selected = gd.SelectedItem as DataRowView;
                if (row_selected != null)
                {
                    ono = row_selected["ono"].ToString();
                    fno = row_selected["fno"].ToString();
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

        private void bnt_restore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Do you want to Restore this order", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int i = db.DataInsertUpdateDelete("Update orders set Ostatus='null' where oid='" + ono + "';");
                    int l = db.DataInsertUpdateDelete("insert into noOfBidsforEachOrder values('" + ono + "','" + OrderBidSystemDatabse.b + "', '" +  fno +"' , 0);");
                    int j = db.DataInsertUpdateDelete("delete from LateBids where ono = '" + ono + "';");

                    if ((i == 1) && (j == 1) && (l == 1))
                    {
                        DataTable dt2 = db.DisplayOnGrid("Select * from lateBids");
                        DataGridViewLate.ItemsSource = dt2.DefaultView;
                        MessageBox.Show("Your order restored Successfully", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

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

        private void bnt_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Do you want to Delete this order", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int i = db.DataInsertUpdateDelete("Update orders set Ostatus='deleted' where oid='" + ono + "';");
                    int j = db.DataInsertUpdateDelete("delete from LateBids where ono = '" + ono + "';");



                    if ((i == 1) && (j == 1))
                    {
                        DataTable dt2 = db.DisplayOnGrid("Select * from lateBids");
                        DataGridViewLate.ItemsSource = dt2.DefaultView;
                        MessageBox.Show("Your order deleted Successfully", "Error", MessageBoxButton.OK, MessageBoxImage.Information);



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
    }
}
