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
    /// Interaction logic for FArmerHome.xaml
    /// </summary>
    public partial class FArmerHome : Window
    {
        public string selectid;
        public string Type;
        SqlConnection Con;
        SqlCommand Com;
        SqlDataReader dr;

        public FArmerHome()
        {
            InitializeComponent();
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new UserControlfhome());
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Welcome_Screen());

        }



        private void Buttopower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ListviewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Index = ListviewMenu.SelectedIndex;
            MoveCursorMenu(Index);

            switch(Index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlfhome());
                    
                  
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new FarmerDetails());
                  
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new BuyerDetails());
                    break;

                case 3:


                    Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");

                    //select tempory database fid and store in to my selectid variable
                    Con.Open();
                    Com = new SqlCommand("select fid from farmerRegistrationtemp where Id = 001 ", Con);
                    dr = Com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            selectid = dr["fid"].ToString();
                        }
                        dr.Close();
                        Con.Close();
                        Com.Dispose();
                    }

                    //diplay on to label my primary table data
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                    Con.Open();
                    Com = new SqlCommand("select * from farmerRegistration where fid = '" + selectid + "' ", Con);
                    dr = Com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                           Type = dr["type"].ToString();
                    
                        }
                        dr.Close();
                        Con.Close();
                    }

                    //Selecting item type and show that usercontroll acording given item type

                    if(Type == "Dairy")
                    {
                        GridPrincipal.Children.Clear();
                        GridPrincipal.Children.Add(new AddFarmerInventory());

                    }

                    else if(Type == "Vegetables")
                    {
                        GridPrincipal.Children.Clear();
                        GridPrincipal.Children.Add(new Addfarmerveginventory());
                    }

                    else if (Type == "Meat")
                    {
                        GridPrincipal.Children.Clear();
                        GridPrincipal.Children.Add(new Addfarmermeatinventory());
                    }
                    else if (Type == "Fruits")
                    {
                        GridPrincipal.Children.Clear();
                        GridPrincipal.Children.Add(new Addfarmerfruitinventory());
                    }

                    break;
                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new MarketAnalyis());
                    break;
                case 5:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new CheckOrderFarmer());
                    break;
                case 6:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new MyOrderFarmer());
                    break;
                default:
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            TransitioningContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0 );
        }

        private void Btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow obj = new MainWindow();
            obj.Show();
        }

        private void Btn_help1_Click(object sender, RoutedEventArgs e)
        {
            Helpwindow obj = new Helpwindow();
            obj.Show();
        }

        private void Btn_help2_Click(object sender, RoutedEventArgs e)
        {
            Helpwindow obj = new Helpwindow();
            obj.Show();
        }

        private void Btn_help2_Click_1(object sender, RoutedEventArgs e)
        {
            Helpwindow obj = new Helpwindow();
            obj.Show();
        }

        private void Btn_drtxt_Click(object sender, RoutedEventArgs e)
        {
            FarmerQrCode obj = new FarmerQrCode();
            obj.Show();
        }

        private void Btn_buyerqr_Click(object sender, RoutedEventArgs e)
        {
            FarmerQrCode obj = new FarmerQrCode();
            obj.Show();
        }

        private void Btn_update_Click(object sender, RoutedEventArgs e)
        {
            UpdateFarmer obj = new UpdateFarmer();
            obj.Show();
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
           
            FarmerDelete obj = new FarmerDelete();
            obj.Show();
        }
    }
}
