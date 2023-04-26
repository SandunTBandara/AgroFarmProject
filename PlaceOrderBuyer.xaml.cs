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

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for PlaceOrderBuyer.xaml
    /// </summary>
    public partial class PlaceOrderBuyer : UserControl
    {
        public double Qty;
        public PlaceOrderBuyer()
        {
            InitializeComponent();
        }

        OrderBidSystemDatabse db = new OrderBidSystemDatabse();
      //  public void getdata(string x)
        //{
          //  a = x;
       // }
        private void form_loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = db.getID("Select * from Orders;", "O");



                
                txt_oid.Text = id;
                txt_bid.Text = OrderBidSystemDatabse.b;
                txt_bid.IsReadOnly = false;
                txt_oid.IsReadOnly = true;
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

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int c = 6;


                if (txt_oid.Text.Length == 0)
                {
                    txt_oid.Width = 200;
                    lbl_ono.Content = "Order ID cannot be null";
                    txt_oid.Focus();
                }
                else
                {
                    txt_oid.Width = 300;
                    lbl_ono.Content = "";
                    c -= 1;
                }



                if (txt_bid.Text.Length == 0)
                {
                    txt_bid.Width = 200;
                    lbl_bno.Content = "Buyer ID cannot be null";
                    txt_bid.Focus();
                }
                else
                {
                    txt_bid.Width = 300;
                    lbl_bno.Content = "";
                    c -= 1;
                }



                if (txt_bname.Text.Length == 0)
                {
                    txt_bname.Width = 200;
                    lbl_bname.Content = "Buyer name cannot be null";
                    txt_bname.Focus();
                }
                else if (txt_bname.Text.Any(char.IsDigit))
                {
                    txt_bname.Width = 150;
                    lbl_bname.Content = "Buyer name cannot contain any number";
                    txt_bname.Focus();
                }
                else
                {
                    txt_bname.Width = 300;
                    lbl_bname.Content = "";
                    c -= 1;
                }



                if (cmb_type.Text.Length == 0)
                {
                    cmb_type.Width = 200;
                    lbl_type.Content = "Item Type cannot be null";
                    cmb_type.Focus();
                }
                else
                {
                    cmb_type.Width = 300;
                    lbl_type.Content = "";
                    c -= 1;



                }



                if (txt_item.Text.Length == 0)
                {
                    txt_item.Width = 200;
                    lbl_item.Content = "Item cannot be null";
                    txt_item.Focus();
                }
                else if (txt_item.Text.Any(char.IsDigit))
                {
                    txt_item.Width = 150;
                    lbl_item.Content = "Itemcannot contain any number";
                    txt_item.Focus();
                }
                else
                {
                    txt_item.Width = 300;
                    lbl_item.Content = "";
                    c -= 1;



                }

             
              

                if (txt_Qty.Text.Length == 0)
                {
                    txt_Qty.Width = 200;
                    lbl_qty.Content = "Quantity cannot be null";
                    txt_Qty.Focus();
                }
                else if (txt_Qty.Text.Any(char.IsLetter))
                {
                    txt_Qty.Width = 150;
                    lbl_qty.Content = "Quantity cannot contain any letter";
                    txt_Qty.Focus();
                }
             
                else
                {
                    txt_Qty.Width = 300;
                    lbl_qty.Content = "";
                    c -= 1;
                }



                if (c == 0)
                {
                    string query = "insert into Orders (bno, buyer_name, item_Type, item, Quantity) values('" + txt_bid.Text + "','" + txt_bname.Text + "','" + cmb_type.Text + "','" + txt_item.Text + "'," + Convert.ToDouble(txt_Qty.Text) + " );";
                    int i = db.DataInsertUpdateDelete(query);
                    int j = db.DataInsertUpdateDelete("insert into noOfBidsforEachOrder values('" + txt_oid.Text + "','" + txt_bid.Text + "', '0' , 0);");
                    if (i == 1)
                    {
                        MessageBox.Show("order enterd successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        txt_oid.Clear();
                        txt_bid.Clear();
                        txt_bname.Clear();
                        cmb_type.Text = "";
                        txt_item.Text="";
                        txt_Qty.Clear();



                        string id = db.getID("Select * from Orders;", "O");
                        txt_oid.Text = id;
                        txt_bid.Text = OrderBidSystemDatabse.b;
                        txt_bid.IsReadOnly = false;
                        txt_oid.IsReadOnly = true;
                    }

                   else
                    {
                        MessageBox.Show("order failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void bnt_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_oid.Clear();
            txt_bid.Clear();
            txt_bname.Clear();
            cmb_type.Text = "";
            txt_item.Text="";
            txt_Qty.Clear();

        }
    }
}
