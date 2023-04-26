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

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for FruitInventory.xaml
    /// </summary>
    public partial class FruitInventory : Window
    {
        public FruitInventory()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }
        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {



                if (!_isReportViewerLoaded)
                {
                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                    Microsoft.Reporting.WinForms.ReportDataSource();
                    FarmSystemDataSet6 dataset = new FarmSystemDataSet6();



                    dataset.BeginInit();



                    reportDataSource1.Name = "DataSet1";
                    //Name of the report dataset in our .RDLC file



                    reportDataSource1.Value = dataset.FruitInventory;
                    this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);



                    this._reportViewer.LocalReport.ReportPath = "../../dairyInventory.rdlc";
                    dataset.EndInit();



                    //fill data into WpfApplication4DataSet
                    FarmSystemDataSet6TableAdapters.FruitInventoryTableAdapter
                    accountsTableAdapter = new
                    FarmSystemDataSet6TableAdapters.FruitInventoryTableAdapter();
                    accountsTableAdapter.ClearBeforeFill = true;
                    accountsTableAdapter.Fill(dataset.FruitInventory);




                    _reportViewer.RefreshReport();
                    _isReportViewerLoaded = true;
                }
            }
        }

        private void Button_Back1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
