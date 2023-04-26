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
using System.Data.SqlClient;
using System.Data;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for FarmerQrCode.xaml
    /// </summary>
    public partial class FarmerQrCode : Window
    {

        public string selectid;
        string Fname;
        string Id;
        string address;
        string email;
        string tel;

        SqlConnection Con;
        SqlCommand Com;
        SqlDataReader dr;
        public FarmerQrCode()
        {
            InitializeComponent();
            Con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");

            //select tempory database fid and store in to my selectid variable
            Con.Open();
            Com = new SqlCommand("select fid from farmerRegistrationtemp where Id = 001 ", Con);
            dr = Com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    selectid = dr["fid"].ToString();
                }
                dr.Close();
                Con.Close();
                Com.Dispose();
            }

            //save buyer informations to variables
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
            Con.Open();
            Com = new SqlCommand("select * from farmerRegistration where fid = '" + selectid + "' ", Con);
            dr = Com.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Fname = dr["name"].ToString();
                    Id = dr["fid"].ToString();
                    address = dr["address"].ToString();
                    tel = dr["tel"].ToString();
                    email = dr["email"].ToString();
                }
                dr.Close();
                Con.Close();


            }

            // qr encoding 
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode($"This is Buyer agro farm account                   " +
                $"           your name  : {Fname}                " +
                $"                            your Buyer id  : {Id}                  " +
                $"                            your Address : {address}   " +
                $"                                         your email : {email}   " +
                $"                            your telephone number : {tel}", QRCodeGenerator.ECCLevel.Q);
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
