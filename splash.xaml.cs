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
using System.Windows.Threading;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for splash.xaml
    /// </summary>
    public partial class splash : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        public splash()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 2);
            dt.Start();
        }
        int startpoint = 0;
        private void dt_Tick(Object sender,EventArgs e)
        {
            startpoint += 33;
            prbar.Value = startpoint;
            if(prbar.Value == 100)
            {
                prbar.Value = 0;
                dt.Stop();
                MainWindow obj = new MainWindow();
                obj.Show();
                this.Close();

            }

        }
    }
}
