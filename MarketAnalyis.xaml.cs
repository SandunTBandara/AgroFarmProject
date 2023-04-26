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
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for MarketAnalyis.xaml
    /// </summary>
    public partial class MarketAnalyis : UserControl
    {
        SqlConnection Con;
        SqlCommand Com;
        SqlDataReader dr;
        string vegqty;
        string meatqty;
        string dairyqty;
        string fruitqty;
        double dvegqty;
        double ddairyqty;
        double dmeatqty;
        double dfruitqty;


        public MarketAnalyis()
        {
            InitializeComponent();
            Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");

            Con.Open();
            // taking current quantity state
            Com = new SqlCommand("select quantity from VegInventory where Item_type = 'vegTotal'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    vegqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dvegqty = Convert.ToDouble(vegqty);

            Con.Open();
            // taking current quantity state
            Com = new SqlCommand("select quantity from DairyInventory where Item_type = 'DairyTotal'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    dairyqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            ddairyqty = Convert.ToDouble(dairyqty);

            Con.Open();
            // taking current quantity state
            Com = new SqlCommand("select quantity from  MeatInventory where Item_type = 'MeatTotal'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    meatqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dmeatqty = Convert.ToDouble(meatqty);



            Con.Open();
            // taking current quantity state
            Com = new SqlCommand("select quantity from  FruitInventory where Item_type = 'FruitTotal'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    fruitqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dfruitqty = Convert.ToDouble(fruitqty);






            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "vegetable",
                    Values = new ChartValues<double> { dvegqty }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Dairy",
                Values = new ChartValues<double> { ddairyqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Meat",
                Values = new ChartValues<double> { dmeatqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Fruit",
                Values = new ChartValues<double> { dfruitqty }
            });
            //also adding values updates and animates the chart automatically


            Labels = new[] { "" };
            Formatter = value => value.ToString("N");

            DataContext = this;

            //----------------------------------------------------------------------------------------------------------

            //Py chart passing
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        //pie chart 
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }


        // report buttons 
        private void Btn_vegIn_Click(object sender, RoutedEventArgs e)
        {
            veginventoryreport obj = new veginventoryreport();
            obj.Show();
        }

        private void Btn_DarIn_Click(object sender, RoutedEventArgs e)
        {
            dailyinventory obj = new dailyinventory();
            obj.Show();
        }

        private void Btn_metIn_Click(object sender, RoutedEventArgs e)
        {
            MeatInventory obj = new MeatInventory();
            obj.Show();
        }

        private void Btn_friIn_Click(object sender, RoutedEventArgs e)
        {
            FruitInventory obj = new FruitInventory();
            obj.Show();
        }

        //live chart window showing live | chart button
        private void Btn_vegrp_Click(object sender, RoutedEventArgs e)
        {
            vegetablelivechart obj = new vegetablelivechart();
            obj.Show();
        }

        private void Btn_Darrp_Click(object sender, RoutedEventArgs e)
        {
            dairylivechart obj = new dairylivechart();
            obj.Show();
        }

        private void Btn_metrp_Click(object sender, RoutedEventArgs e)
        {
            meatlivechart obj = new meatlivechart();
            obj.Show();
        }

        private void Btn_frirp_Click(object sender, RoutedEventArgs e)
        {
            Fruitlivechart obj = new Fruitlivechart();
            obj.Show();

        }
    }
}
