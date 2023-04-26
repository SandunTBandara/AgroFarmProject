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
    /// Interaction logic for dairylivechart.xaml
    /// </summary>
    public partial class dairylivechart : Window
    {
        SqlConnection Con;
        SqlCommand Com;
        SqlDataReader dr;
        string milkqty;
        string cheeseqty;
        string butterqty;
        string yogurtqty;
        string DairyTotalqty;
        double dmilkqty;
        double dcheeseqty;
        double dbutterqty;
        double dyogurtqt;
        double dDairyTotalqty;
        public dairylivechart()
        {
            InitializeComponent();

            Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");

            Con.Open();

            Com = new SqlCommand("select quantity from DairyInventory where Item_type = 'milk'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    milkqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dmilkqty = Convert.ToDouble(milkqty);

            Con.Open();
            Com = new SqlCommand("select quantity from DairyInventory where Item_type = 'cheese'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cheeseqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dcheeseqty = Convert.ToDouble(cheeseqty);

            Con.Open();
            Com = new SqlCommand("select quantity from DairyInventory where Item_type = 'butter'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    butterqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dbutterqty = Convert.ToDouble(butterqty);


            Con.Open();
            Com = new SqlCommand("select quantity from DairyInventory where Item_type = 'yogurt'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    yogurtqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dyogurtqt = Convert.ToDouble(yogurtqty);


            Con.Open();
            Com = new SqlCommand("select quantity from DairyInventory where Item_type = 'DairyTotal'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DairyTotalqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dDairyTotalqty = Convert.ToDouble(DairyTotalqty);


            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Milk",
                    Values = new ChartValues<double> { dmilkqty }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Cheese",
                Values = new ChartValues<double> { dcheeseqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Butter",
                Values = new ChartValues<double> { dbutterqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Yogurt",
                Values = new ChartValues<double> { dyogurtqt }
            });
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Total Dairy",
                Values = new ChartValues<double> { dDairyTotalqty }
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
