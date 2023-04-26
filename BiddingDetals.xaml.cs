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
    /// Interaction logic for BiddingDetals.xaml
    /// </summary>
    public partial class BiddingDetals : Window
    {
        public BiddingDetals()
        {
            InitializeComponent();
        }

        OrderBidSystemDatabse db = new OrderBidSystemDatabse();
        private void bnt_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bnt_submit_Click(object sender, RoutedEventArgs e)
        {
            //try { 
            int c = 6;



            if (txt_oid.Text.Length == 0)
            {
                lbl_oid.Content = "Order ID cannot be null";
                txt_oid.Focus();
            }
            else
            {
                lbl_oid.Content = "";
                c -= 1;
            }



            if (txt_bid.Text.Length == 0)
            {
                lbl_bid.Content = "Buyer ID cannot be null";
                txt_bid.Focus();
            }
            else
            {
                lbl_bid.Content = "";
                c -= 1;
            }



            if (txt_fid.Text.Length == 0)
            {
                lbl_fid.Content = "Farmer ID cannot be null";
                txt_fid.Focus();
            }
            else
            {
                lbl_fid.Content = "";
                c -= 1;
            }



            if (txt_qty.Text.Length == 0)
            {
                lbl_qty.Content = "Quantity cannot be null";
                txt_qty.Focus();
            }
            else
            {
                lbl_qty.Content = "";
                c -= 1;
            }



            if (txt_unitp.Text.Length == 0)
            {
                lbl_uprice.Content = "Unit Price cannot be null";
                txt_unitp.Focus();
            }
            else if (txt_unitp.Text.Any(char.IsLetter))
            {
                lbl_uprice.Content = "Unit Price cannot contain any letters";
                txt_unitp.Focus();
            }
            else
            {
                lbl_uprice.Content = "";
                c -= 1;
            }



            if (date.Text.Length == 0)
            {
                lbl_date.Content = "Date cannot be null";
                date.Focus();
            }
            else if (date.SelectedDate < DateTime.Now)
            {
                lbl_date.Content = "Invalid Date";
                date.Focus();
            }
            else
            {
                lbl_date.Content = "";
                c -= 1;
            }



            if (c == 0)
            {
                double qty = Convert.ToDouble(txt_qty.Text);
                double up = Convert.ToDouble(txt_unitp.Text);
                double tot = qty * up;
                int i = db.DataInsertUpdateDelete("insert into Bits_for_Orders (ono, bno, fno, price_per_unit, Total, dueDate) values ('" + txt_oid.Text + "','" + txt_bid.Text + "','" + txt_fid.Text + "'," + Convert.ToDouble(txt_unitp.Text) + "," + tot + ", '" + date.Text + "');");
                if (i == 1)
                {
                    int j = db.DataInsertUpdateDelete("insert into noOfBidsforEachOrder values ('" + txt_oid.Text + "','" + txt_bid.Text + "','" + txt_fid.Text + "', 1);");
                    this.Close();
                    if (j == 1)
                    {
                        MessageBox.Show("Your bid Successfull!", "Infomation", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong, Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                  
                }
                else
                    MessageBox.Show("Some error occured, Your bid cancelled. Please Try again! ", "Infomation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
//        }
//catch (SqlException)
//{
//MessageBox.Show("Some Database Error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//}
//catch (Exception)
//{
//MessageBox.Show("Some Error occured!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//}


        }
    }
}
