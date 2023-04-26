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
    /// Interaction logic for meatlivechart.xaml
    /// </summary>
    public partial class meatlivechart : Window
    {
        SqlConnection Con;
        SqlCommand Com;
        SqlDataReader dr;
        string Chickenqty;
        string Beefqty;
        string Muttonqty;
        string Porkqty;
        string MeatTotalqty;
        double dChickenqty;
        double dBeefqty;
        double dMuttonqty;
        double dPorkqty;
        double dMeatTotalqty;
        public meatlivechart()
        {
            InitializeComponent();
            Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");

            Con.Open();

            Com = new SqlCommand("select quantity from MeatInventory where Item_type = 'Chicken'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Chickenqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dChickenqty = Convert.ToDouble(Chickenqty);

            Con.Open();
            Com = new SqlCommand("select quantity from MeatInventory where Item_type = 'Beef'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Beefqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dBeefqty = Convert.ToDouble(Beefqty);

            Con.Open();
            Com = new SqlCommand("select quantity from MeatInventory where Item_type = 'Mutton'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Muttonqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dMuttonqty = Convert.ToDouble(Muttonqty);


            Con.Open();
            Com = new SqlCommand("select quantity from MeatInventory where Item_type = 'Pork'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Porkqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dPorkqty = Convert.ToDouble(Porkqty);


            Con.Open();
            Com = new SqlCommand("select quantity from MeatInventory where Item_type = 'MeatTotal'  ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    MeatTotalqty = dr["quantity"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            dMeatTotalqty = Convert.ToDouble(MeatTotalqty);




            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Chicken",
                    Values = new ChartValues<double> { dChickenqty }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Beef",
                Values = new ChartValues<double> { dBeefqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Mutton",
                Values = new ChartValues<double> { dMuttonqty }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Pork",
                Values = new ChartValues<double> { dPorkqty }
            });
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Meat Total",
                Values = new ChartValues<double> { dMeatTotalqty }
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
