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
using System.Drawing;
using System.Data;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for veginventoryreport.xaml
    /// </summary>
    public partial class veginventoryreport : Window
    {
        public veginventoryreport()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }
        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {

                {
                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                    Microsoft.Reporting.WinForms.ReportDataSource();
                    FarmSystem dataset = new FarmSystem();



                    dataset.BeginInit();



                    reportDataSource1.Name = "DataSet1";
                    //Name of the report dataset in our .RDLC file



                    reportDataSource1.Value = dataset.VegInventory;
                    this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                    this._reportViewer.LocalReport.ReportPath = "../../veginventory.rdlc";
                    dataset.EndInit();



                    //fill data into WpfApplication4DataSet
                    FarmSystemTableAdapters.VegInventoryTableAdapter
                    accountsTableAdapter = new
                  FarmSystemTableAdapters.VegInventoryTableAdapter();
                    accountsTableAdapter.ClearBeforeFill = true;
                    accountsTableAdapter.Fill(dataset.VegInventory);




                    _reportViewer.RefreshReport();
                    _isReportViewerLoaded = true;
                }
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
