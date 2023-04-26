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
using System.Windows.Threading;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for Welcome_Screen.xaml
    /// </summary>
    public partial class Welcome_Screen : UserControl
    {
        public Welcome_Screen()
        {
            InitializeComponent();

            media.Source = new Uri(Environment.CurrentDirectory + @"\load223.mp4");
            loading();

        }

        DispatcherTimer timer = new DispatcherTimer();



        private void MediaElement_MediaEnded(Object sender, RoutedEventArgs e)
        {
            media.Position = new TimeSpan(0, 0, 1);
            media.Play();
        }



        private void timer_tick(Object senser, RoutedEventArgs e)
        {
            timer.Stop();
            media.Visibility = Visibility.Hidden;
            lab.Visibility = Visibility.Hidden;
            lab1.Visibility = Visibility.Hidden;

        }



        void loading()
        {

            timer.Interval = new TimeSpan(0, 0, 18);
            timer.Start();
        }
    }
}
