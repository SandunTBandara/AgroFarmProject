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
    /// Interaction logic for vegetablelivechart.xaml
    /// </summary>
    public partial class vegetablelivechart : Window
    {
        SqlConnection Con;
        SqlCommand Com;
        SqlDataReader dr;
        string Carrotqty;
        string Leeksqty;
        string potatoqty;
        string Tomatoqty;
        string vegTotalqty;
        double dCarrotqty;
        double dLeeksqty;
        double dpotatoqty;
        double dTomatoqty;
        double dvegTotalqty;
        public vegetablelivechart()
        {
            InitializeComponent();
            Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");


            Con.Open();
            Com = new SqlCommand("select quantity from VegInventory where Item_type = 'Carrot'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Carrotqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dCarrotqty = Convert.ToDouble(Carrotqty);

            Con.Open();
            Com = new SqlCommand("select quantity from VegInventory where Item_type = 'Leeks'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Leeksqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dLeeksqty = Convert.ToDouble(Leeksqty);

            Con.Open();
            Com = new SqlCommand("select quantity from VegInventory where Item_type = 'potato'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    potatoqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dpotatoqty = Convert.ToDouble(potatoqty);


            Con.Open();
            Com = new SqlCommand("select quantity from VegInventory where Item_type = 'Tomato'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Tomatoqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dTomatoqty = Convert.ToDouble(Tomatoqty);


            Con.Open();
            Com = new SqlCommand("select quantity from VegInventory where Item_type = 'vegTotal'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    vegTotalqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dvegTotalqty = Convert.ToDouble(vegTotalqty);




            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "vegetable",
                    Values = new ChartValues<double> { dCarrotqty }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Leeks",
                Values = new ChartValues<double> { dLeeksqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Potato",
                Values = new ChartValues<double> { dpotatoqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Tomato",
                Values = new ChartValues<double> { dTomatoqty }
            });
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Total Vegetable",
                Values = new ChartValues<double> { dvegTotalqty }
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
