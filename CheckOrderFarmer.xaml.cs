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
//using Microsoft.VisualStudio.Data;


namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for CheckOrderFarmer.xaml
    /// </summary>
    public partial class CheckOrderFarmer : UserControl
    {
        public CheckOrderFarmer()
        {
            InitializeComponent();
        }


        OrderBidSystemDatabse db = new OrderBidSystemDatabse();
        string oid = null;
        private void form_loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
                DataTable dt1 = db.DisplayOnGrid("Select oid, bno, buyer_name, item_Type, item, Quantity from Orders where OStatus = 'null' and oid != all(Select ono from Bits_for_Orders where fno = '" + OrderBidSystemDatabse.f + "'); ");
                DataGridView.ItemsSource = dt1.DefaultView;



                DataTable dt2 = db.DisplayOnGrid("Select oid, bno, buyer_name, item_Type, item, Quantity from Orders where OStatus = 'null' and oid = any(Select ono from Bits_for_Orders where fno = '" + OrderBidSystemDatabse.f + "'); ");
                DataGridView2.ItemsSource = dt2.DefaultView;
                
                if (dt1.Rows.Count == 0)
                {
                    lbl_error_orders.Content = "There are no orders available";
                }
                else
                {
                    lbl_error_orders.Content = "";
                }



                if (dt2.Rows.Count == 0)
                {
                    lbl_error_bids.Content = "You haven't bid for any order";
                }
                else
                {
                    lbl_error_bids.Content = "";
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

        BiddingDetals form = new BiddingDetals();

        private void DatagridView_selctionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView row_selected = gd.SelectedItem as DataRowView;
                if (row_selected != null)
                {
                    oid = row_selected["oid"].ToString();
                    form.txt_oid.Text = row_selected["oid"].ToString();
                    form.txt_bid.Text = row_selected["bno"].ToString();
                    form.txt_fid.Text = OrderBidSystemDatabse.f;
                    form.txt_qty.Text = row_selected["Quantity"].ToString();
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

        private void bnt_bit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Do you want a make bid for the order no " + form.txt_oid.Text + "?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    //MessageBox.Show("Please wait your bid for Order no " + form.txt_oid.Text + " is pending...", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    Button btn = sender as Button;
                    btn.Background = Brushes.Red;
                    btn.Content = "Bidded";
                    form.Show();
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
