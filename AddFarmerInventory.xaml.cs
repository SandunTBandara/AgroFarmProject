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
    /// Interaction logic for AddFarmerInventory.xaml
    /// </summary>
    public partial class AddFarmerInventory : UserControl
    {
        SqlConnection Con;
        SqlCommand Com;
        SqlDataReader dr;
        string qty;
        double totqty;
        double qty1;
        string vqty;


        public AddFarmerInventory()
        {
            InitializeComponent();
            Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");
        }

        private void Btn_DDel_Click_1(object sender, RoutedEventArgs e)
        { 
            //vinuri
            try
            {
                if (txt_Dqty.Text.Length == 0)
                {
                    lbl_error.Content = "Quantity cannot be null";
                }
                else if (txt_Dqty.Text.Any(char.IsLetter))
                {
                    lbl_error.Content = "Quantity cannot Caontain any letter";
                }
                else
                {
                    Con.Open();
                    //sanu


            // taking current quantity state
            Com = new SqlCommand("select quantity from DairyInventory where Item_type = '" + cmb_DItemType.Text + "' ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    qty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }
            // add current quantity to input quantity
            qty1 = Convert.ToDouble(qty);
            totqty = qty1 + Convert.ToDouble(txt_Dqty.Text);
            //store total quantity back to DB
            Con.Open();
            Com = new SqlCommand("Update DairyInventory set quantity = '" + totqty + "' where Item_type = '" + cmb_DItemType.Text + "'  ", Con);
            int i = Com.ExecuteNonQuery();
            if (i == 1)
            {
                MessageBox.Show("Inventory added succesfully", "Infromation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("inventory not added please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Con.Close();
            Com.Dispose();



            //take totalvegetable quantity data
            Con.Open();
            Com = new SqlCommand("select quantity from DairyInventory where Item_type =  'DairyTotal' ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    vqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            //adding total vegetable quantity to current quantity
            double qty2 = Convert.ToDouble(vqty);
            double totqty1 = qty2 + Convert.ToDouble(txt_Dqty.Text);

            //insert data into total vegitable field
            Con.Open();
            Com = new SqlCommand("Update DairyInventory set quantity = '" + totqty1 + "' where Item_type =  'DairyTotal' ", Con);
            Com.ExecuteNonQuery();

            Con.Close();
            Com.Dispose();
                }
            }

            //vinuri
            catch (SqlException)
            {
                MessageBox.Show("Some Databae error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Some error occured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
