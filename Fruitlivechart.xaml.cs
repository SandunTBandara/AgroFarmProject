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
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for Fruitlivechart.xaml
    /// </summary>
    public partial class Fruitlivechart : Window
    {
        SqlConnection Con;
        SqlCommand Com;
        SqlDataReader dr;
        string PineAppleqty;
        string Bananaqty;
        string Grapesqty;
        string WoodAppleqty;
        string FruitTotalqty;
        double dPineAppleqty;
        double dBananaqty;
        double dGrapesqty;
        double dWoodAppleqty;
        double dFruitTotalqty;




        public Fruitlivechart()
        {
            InitializeComponent();
            Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");

            Con.Open();

            Com = new SqlCommand("select quantity from FruitInventory where Item_type = 'PineApple'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Bananaqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dBananaqty = Convert.ToDouble(Bananaqty);

            Con.Open();
            Com = new SqlCommand("select quantity from FruitInventory where Item_type = 'Banana'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    PineAppleqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dPineAppleqty = Convert.ToDouble(PineAppleqty);

            Con.Open();
            Com = new SqlCommand("select quantity from FruitInventory where Item_type = 'Grapes'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Grapesqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dGrapesqty = Convert.ToDouble(Grapesqty);


            Con.Open();
            Com = new SqlCommand("select quantity from FruitInventory where Item_type = 'WoodApple'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    WoodAppleqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dWoodAppleqty = Convert.ToDouble(WoodAppleqty);


            Con.Open();
            Com = new SqlCommand("select quantity from FruitInventory where Item_type = 'FruitTotal'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    FruitTotalqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dFruitTotalqty = Convert.ToDouble(FruitTotalqty);





            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Banana",
                    Values = new ChartValues<double> { dPineAppleqty }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "PineApple",
                Values = new ChartValues<double> { dPineAppleqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Grapes",
                Values = new ChartValues<double> { dGrapesqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "WoodApple",
                Values = new ChartValues<double> { dWoodAppleqty }
            });
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Total Fruit",
                Values = new ChartValues<double> { dFruitTotalqty }
            });
            //also adding values updates and animates the chart automatically


            Labels = new[] { "" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
