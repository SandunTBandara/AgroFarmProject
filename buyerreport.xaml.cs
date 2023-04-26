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
    /// Interaction logic for buyerreport.xaml
    /// </summary>
    public partial class buyerreport : Window
    {
       
        public buyerreport()
        {
            InitializeComponent();
            _reportViewer.Load += _reportViewer_Load;
        }
        private bool _isReportViewerLoaded;
        private void _reportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                FarmSystemDataSet dataset = new FarmSystemDataSet();



                dataset.BeginInit();



                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file



                reportDataSource1.Value = dataset.buyerRegistration;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);



                this._reportViewer.LocalReport.ReportPath = "../../buyer.rdlc";
                dataset.EndInit();



                //fill data into WpfApplication4DataSet
                FarmSystemDataSetTableAdapters.buyerRegistrationTableAdapter
                accountsTableAdapter = new
                FarmSystemDataSetTableAdapters.buyerRegistrationTableAdapter();
                accountsTableAdapter.ClearBeforeFill = true;
                accountsTableAdapter.Fill(dataset.buyerRegistration);




                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
