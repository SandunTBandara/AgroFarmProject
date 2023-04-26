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

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for BuyerHome.xaml
    /// </summary>
    public partial class BuyerHome : Window
    {
        public BuyerHome()
        {
            InitializeComponent();
           
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Welcome_Screen());

           
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
                    GridPrincipal.Children.Add(new UserControlBHome());
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new BuyerDetails());
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new FarmerDetails());
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new MarketAnalyis());
                    break;
                case 4:

                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new PlaceOrderBuyer());
                    break;
                case 5:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new checkOrdersBuyer());
                    break;
                default:
                    break;

            }
        }

        private void MoveCursorMenu(int index)
        {
            TransitioningContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }



        private void Buttopower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void Btn_bhelp_Click(object sender, RoutedEventArgs e)
        {
            Helpwindow obj = new Helpwindow();
            obj.Show();
        }

        private void Btn_help2_Click(object sender, RoutedEventArgs e)
        {
            Helpwindow obj = new Helpwindow();
            obj.Show();
        }

        private void Btn_buyerqr_Click(object sender, RoutedEventArgs e)
        {
            QrCode obj = new QrCode();
            obj.Show();
        }

        private void Btn_buyerQrtxt_Click(object sender, RoutedEventArgs e)
        {
            QrCode obj = new QrCode();
            obj.Show();
        }

        private void Btn_update_Click(object sender, RoutedEventArgs e)
        {
            UpdateBuyer obj = new UpdateBuyer();
            obj.Show();
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            
            BuyerDelete obj = new BuyerDelete();
            obj.Show();
        }



    }
}
