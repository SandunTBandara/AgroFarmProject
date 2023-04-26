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
    /// Interaction logic for checkOrdersBuyer.xaml
    /// </summary>
    public partial class checkOrdersBuyer : UserControl
    {
        public checkOrdersBuyer()
        {
            InitializeComponent();
        }

        OrderBidSystemDatabse db = new OrderBidSystemDatabse();
        DataTable dt1;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        string constrin = "Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True";

        private void form_loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dt1 = db.DisplayOnGrid("Select ono,bno,fno,price_per_unit,Total,dueDate from Bits_for_Orders where bno ='" + OrderBidSystemDatabse.b + "';");
                DataGridView.ItemsSource = dt1.DefaultView;

                DataTable dt2 = db.DisplayOnGrid("Select oid,item_Type,item,Quantity from orders where bno = '" + OrderBidSystemDatabse.b + "' and Ostatus = 'null' order by oid asc;");
                DataGridView2.ItemsSource = dt2.DefaultView;

                DataTable dt3 = db.DisplayOnGrid("Select ono, Sum(count) as count from noOfBidsforEachOrder where bno ='" + OrderBidSystemDatabse.b + "' Group By ono Order By ono;");
                DataGridView3.ItemsSource = dt3.DefaultView;


                if (dt2.Rows.Count == 0)
                {
                    lbl_error_orders.Content = "*You haven't Orderd any thing yet!";
                }
                else if (dt1.Rows.Count == 0)
                {
                    lbl_error_bids.Content = "Sorry! You haven't got any bid for your orders";
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

        string ono, bno, fno, uprice, tot, date;
        private void btn_myorders_Click(object sender, RoutedEventArgs e)
        {
            MyOrderBuyer form = new MyOrderBuyer();
            form.Show();
        }

        private void bnt_accept_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                var result = MessageBox.Show("Do you want to Accept this bid bidded by farmer ID " + fno + "?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int a = db.DataInsertUpdateDelete("Update Bits_for_Orders set bStatus='Accepted' where ono ='" + ono + "' and fno='" + fno + "';");
                    int c = db.DataInsertUpdateDelete("Update orders set oStatus='Accepted' where oid ='" + ono + "';");



                    if ((a == 1) && (c == 1))
                    {
                       // MessageBox.Show("Plase wait a moment...", "Inforamtion", MessageBoxButton.OK, MessageBoxImage.Information);
                        Button btn = sender as Button;
                        btn.Background = Brushes.Green;
                        btn.Content = "Accepted";



                        DataTable dt = db.DisplayOnGrid("select buyer_name, item_Type, item, Quantity from orders where oid = '" + ono + "';");



                        string name = dt.Rows[0]["buyer_name"].ToString();
                        string type = dt.Rows[0]["item_type"].ToString();
                        string item = dt.Rows[0]["item"].ToString();
                        double Quantity = Convert.ToDouble(dt.Rows[0]["quantity"].ToString());

                        
                        if(type == "Dairy")
                        {
                            double itemQty = getItemQuantiyt("Select quantity from DairyInventory where Item_type = '" + item + "';", Quantity);
                            double totQty = getTot("Select quantity from DairyInventory where Item_type =' DairyTotal' ", Quantity);
                            string q1 = "update DairyInventory set quantity =" + Convert.ToDouble(itemQty)+ " where Item_type = '" + item +"';";
                            string q2 = "update DairyInventory set quantity =" + totQty + " where Item_type =' DairyTotal' ";
                            int z = updateTot(q1, q2);
;                                 
                        }
                        else if(type == "Fruits")
                        {

                            double itemQty = getItemQuantiyt("Select quantity from FruitInventory where Item_type = '" + item + "';", Quantity);
                            double totQty = getTot("Select quantity from FruitInventory where Item_type =' FruitTotal' ", Quantity);
                            string q1 = "update FruitInventory set quantity =" + itemQty + " where Item_type = '" + item + "';";
                            string q2 = "update FruitInventory set quantity =" + totQty + " where Item_type =' FruitTotal' ";
                            int z = updateTot(q1, q2);
                        }
                        else if(type == "vegetables")
                        {

                            double itemQty = getItemQuantiyt("Select quantity from VegInventory where Item_type = '" + item + "';", Quantity);
                            double totQty = getTot("Select quantity from VegInventory where Item_type ='vegTotal' ;", Quantity);
                            string q1 = "update VegInventory set quantity =" + itemQty + " where Item_type = '" + item + "';";
                            string q2 = "update VegInventory set quantity =" + totQty + " where Item_type =' vegTotal' ";
                            int z = updateTot(q1, q2);
                        }
                        else if(type == "Meat")
                        {

                            double itemQty = getItemQuantiyt("Select quantity from MeatInventory where Item_type = '" + item + "';", Quantity);
                            double totQty = getTot("Select quantity from MeatInventory where Item_type =' MeatTotal' ", Quantity);
                            string q1 = "update MeatInventory set quantity =" + itemQty + " where Item_type = '" + item + "';";
                            string q2 = "update MeatInventory set quantity =" + totQty + " where Item_type =' MeatTotal' ";
                            int z = updateTot(q1, q2);
                        }

                        int i = db.DataInsertUpdateDelete("insert into AcceptedBids values('" + ono + "','" + fno + "','" + bno + "','" + name + "','" + type + "','" + item + "'," + Quantity + "," + uprice + "," + tot + ",'" + date + "');");
                        if (i == 1 )
                        {
                            MessageBox.Show("You accepted the bit by farmer no " + fno + " for your Order no " + ono + " Succesfully!", "Inforamtion", MessageBoxButton.OK, MessageBoxImage.Information);



                            for (int b = 0; b < dt1.Rows.Count; b++)
                            {
                                if ((dt1.Rows[b]["ono"].ToString() == ono) && (dt1.Rows[b]["fno"].ToString() != fno))
                                {
                                    DataGridCell cell = (DataGridView.Columns[6].GetCellContent(DataGridView.Items[b])).Parent as DataGridCell;
                                    Button btn_rej = new Button();
                                    btn_rej.Content = "X Rejected";
                                    btn_rej.Background = Brushes.Gray;
                                    btn_rej.Foreground = Brushes.Red;
                                    cell.Content = btn_rej;
                                }
                            }



                            int k = db.DataInsertUpdateDelete("delete from Bits_for_Orders where ono = '" + ono + "';");
                            int g = db.DataInsertUpdateDelete("delete from noOfBidsforEachOrder where ono = '" + ono + "';");
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong, Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong, Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            //}
            //catch (SqlException)
            //{
            //    MessageBox.Show("Some Database Error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Some Error occured!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

        }

        private void DatagridView_selctionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {



                DataGrid dg = (DataGrid)sender;
                DataRowView row_selected = dg.SelectedItem as DataRowView;
                if (row_selected != null)
                {
                    fno = row_selected["fno"].ToString();
                    ono = row_selected["ono"].ToString();
                    bno = row_selected["bno"].ToString();
                    uprice = row_selected["price_per_unit"].ToString();
                    tot = row_selected["Total"].ToString();
                    date = row_selected["dueDate"].ToString();
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
        public double getItemQuantiyt(string a, double q)
        {
            string qty;
           
            con = new SqlConnection(constrin);
            con.Open();
            cmd = new SqlCommand(a, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    qty = dr["quantity"].ToString();
                }
                dr.Close();

                cmd.Dispose();
            }

            double newqty = Convert.ToDouble(tot) - q;
            con.Close();

            return newqty;
        }

        public double getTot(string a, double q)
        {
            string tot = null;
            con = new SqlConnection(constrin);
            con.Open();
             cmd = new SqlCommand(a, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tot = dr["quantity"].ToString();
                }
                dr.Close();
               
                cmd.Dispose();
            }
            
            double newtot = Convert.ToDouble(tot) - q;
            con.Close();

            return newtot;
        }

        public int updateTot(string a1, string a2)
        {
            con = new SqlConnection(constrin);
            con.Open();
            cmd = new SqlCommand(a1,con);
            int i = cmd.ExecuteNonQuery();

            cmd = new SqlCommand(a2, con);
            int j = cmd.ExecuteNonQuery();
            con.Close();
            int k;
            if (i == 1 && j == 1)
                k = 1;
            else
                k = 0;
            return i;
        }

           
    }
}
