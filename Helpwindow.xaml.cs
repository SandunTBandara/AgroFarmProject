using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Interaction logic for Helpwindow.xaml
    /// </summary>
    public partial class Helpwindow : Window
    {
        public Helpwindow()
        {
            InitializeComponent();

            // qr encoding 
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode("Agro Farm is a Software create for conect farmer and buyer together. " +
                "Buyers Can Request any item they want with this software and Farmers can simply bid Buyers request also Buyers can simply  acces Farmer requst ." +
                "Our developer team allways provide 24/7 hours Online help . you can Contact us through Our Hotlines. " +
                "Give your feedbacks through  our email . Thank You" +
                "- Agro Farm PLC -" , QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qRCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            image.Source = BitMapToImageSource(qrCodeImage);
        }


        //convert qrcode to bitmap

        private ImageSource BitMapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
