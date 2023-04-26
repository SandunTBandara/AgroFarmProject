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
    /// Interaction logic for MeatInventory.xaml
    /// </summary>
    public partial class MeatInventory : Window
    {
        public MeatInventory()
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
                    FarmSystemDataSet5 dataset = new FarmSystemDataSet5();



                    dataset.BeginInit();



                    reportDataSource1.Name = "DataSet1";
                    //Name of the report dataset in our .RDLC file



                    reportDataSource1.Value = dataset.MeatInventory;
                    this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);



                    this._reportViewer.LocalReport.ReportPath = "../../MeatInventoryReport.rdlc";
                    dataset.EndInit();



                    FarmSystemDataSet5TableAdapters.MeatInventoryTableAdapter
                   accountsTableAdapter = new
                   FarmSystemDataSet5TableAdapters.MeatInventoryTableAdapter();
                    accountsTableAdapter.ClearBeforeFill = true;
                    accountsTableAdapter.Fill(dataset.MeatInventory);




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
