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
using System.Data;
using System.Data.SqlClient; //add
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;

namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for UpdateBuyer.xaml
    /// </summary>
    public partial class UpdateBuyer : Window
    {
        public UpdateBuyer()
        {
            InitializeComponent();
        }
        string strName, imageName;



        UpdateDatabase db = new UpdateDatabase();



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = db.loadData("Select name, address, tel, email, type, password, pwdConfirm from buyerRegistration where fid='" + UpdateDatabase.id + "';");
            txt_name.Text = dt.Rows[0]["name"].ToString();
            txt_address.Text = dt.Rows[0]["address"].ToString();
            txt_tp.Text = dt.Rows[0]["tel"].ToString();
            txt_email.Text = dt.Rows[0]["email"].ToString();
            cmb_type.SelectedItem = dt.Rows[0]["type"].ToString();
            pwd.Password = dt.Rows[0]["password"].ToString();
            pwd_confirm.Password = dt.Rows[0]["pwdConfirm"].ToString();



            loadImage();
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                fldlg.ShowDialog();
                {
                    strName = fldlg.SafeFileName;
                    imageName = fldlg.FileName;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    image.SetValue(System.Windows.Controls.Image.SourceProperty, isc.ConvertFromString(imageName));
                }
                fldlg = null;
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Please select Images only", "Errors", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Please select a File", "Errors", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            int i = 6; 

            if (txt_name.Text.Length == 0)
            {
                lbl_name.Content = "* Please Enter your Name";
                txt_name.Focus();
            }
            else if (txt_name.Text.Any(char.IsDigit))
            {
                lbl_name.Content = "* Name cannot contain any number";
                txt_name.Focus();
            }
            else
            {
                lbl_name.Content = "";
                i--;
            }



            if (txt_address.Text.Length == 0)
            {
                lbl_address.Content = "* Please Enter your address";
                txt_address.Focus();
            }
            else
            {
                lbl_address.Content = "";
                i--;
            }



            if (txt_tp.Text.Length == 0)
            {
                lbl_tp.Content = "* Please Enter your Telphone number";
                txt_tp.Focus();
            }
            else if (!Regex.IsMatch(txt_tp.Text, @"^(?:7|0|(?:\+94))[0-9]{8,9}$"))
            {
                lbl_tp.Content = "* Invalid Telephone number";
                txt_tp.Focus();
            }
            else
            {
                lbl_tp.Content = "";
                i--;
            }



            if (txt_email.Text.Length == 0)
            {
                lbl_email.Content = "* Please enter your email";
                txt_email.Focus();
            }
            else if (!Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                lbl_email.Content = "* Invalid Email(ex: abc@gamil.com)";
                txt_email.Focus();
            }
            else
            {
                lbl_email.Content = "";
                i--;
            }



            if (pwd.Password.Length == 0)
            {
                lbl_pwd.Foreground = System.Windows.Media.Brushes.Red;
                lbl_pwd.Content = "* Please enter a Password";
                lbl_pwd.Focus();
            }
            else if (pwd.Password.Length < 6)
            {
                lbl_pwd.Foreground = System.Windows.Media.Brushes.Red;
                lbl_pwd.Content = "* Not enough length";
                lbl_pwd.Focus();
            }
            else if (!pwd.Password.Any(char.IsDigit))
            {
                lbl_pwd.Foreground = System.Windows.Media.Brushes.Red;
                lbl_pwd.Content = "* Password should contain numbers";
                lbl_pwd.Focus();
            }
            else if (!pwd.Password.Any(char.IsLetter))
            {
                lbl_pwd.Foreground = System.Windows.Media.Brushes.Red;
                lbl_pwd.Content = "* Password should contain letters";
                lbl_pwd.Focus();
            }
            else
            {
                lbl_pwd.Content = "";
                i--;
            }



            if (pwd_confirm.Password.Length == 0)
            {
                lbl_confirm.Content = "* Confirm your password";
                lbl_confirm.Focus();
            }
            else if (pwd_confirm.Password != pwd.Password)
            {
                lbl_confirm.Content = "* Password is not matching";
                lbl_confirm.Focus();
            }
            else
            {
                lbl_confirm.Content = "";
                i--;
            }

            if (i == 0)
            {
                string q = "update buyerRegistration set name='" + txt_name.Text + "', address='" + txt_address.Text + "',tel =" + txt_tp.Text + ", email ='" + txt_email.Text + "', type='" + cmb_type.Text + "',password ='" + pwd.Password.ToString() + "',pwdConfirm = '" + pwd_confirm.Password.ToString() + "' where fid = '" + UpdateDatabase.id + "';";
                int l = db.updateDelete(q);
                saveImage();



                if (l == 1)
                {
                    MessageBox.Show("Succesfully updated", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    DataTable dt = db.loadData("Select name, address, tel, email, type, password, pwdConfirm from buyerRegistration where fid='" + UpdateDatabase.id + "';");
                    txt_name.Text = dt.Rows[0]["name"].ToString();
                    txt_address.Text = dt.Rows[0]["address"].ToString();
                    txt_tp.Text = dt.Rows[0]["tel"].ToString();
                    txt_email.Text = dt.Rows[0]["email"].ToString();
                    cmb_type.SelectedItem = dt.Rows[0]["type"].ToString();
                    pwd.Password = dt.Rows[0]["password"].ToString();
                    pwd_confirm.Password = dt.Rows[0]["pwdConfirm"].ToString();



                    loadImage();
                }
                else
                    MessageBox.Show("Sorry..Something went wrong, Try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);



            }

        }  

        public void loadImage()
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataAdapter adpt;
            DataSet ds;



            con = new SqlConnection("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");


            con.Open();
            using (adpt = new SqlDataAdapter("select * from userImages;", con))
            {
                ds = new DataSet("myDataSet");
                adpt.Fill(ds);
                DataTable dt = ds.Tables[0];



                foreach (DataRow row in dt.Rows)
                {
                    if (row[0].ToString() == UpdateDatabase.id)
                    {
                        byte[] blob = (byte[])row[2];
                        MemoryStream stream = new MemoryStream();
                        stream.Write(blob, 0, blob.Length);
                        stream.Position = 0;



                        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();



                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        ms.Seek(0, SeekOrigin.Begin);
                        bi.StreamSource = ms;
                        bi.EndInit();
                        image.Source = bi;
                    }
                }
                
            }
            con.Close();
        }



        public void saveImage()
        {
            try
            {
                SqlConnection con;
                SqlCommand cmd;
                SqlDataAdapter adpt;
                DataSet ds;



                String constring = ("Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True");


                
                FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                if (fs.Length != 0)
                {
                    byte[] imgByteArry = new byte[fs.Length];
                    fs.Read(imgByteArry, 0, Convert.ToInt32(fs.Length));
                    fs.Close();



                    using (con = new SqlConnection(constring))
                    {
                        con.Open();
                        string sql = "update userImages set name='" + strName + "', img = @img where id='" + UpdateDatabase.id + "';";
                        using (cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.Add(new SqlParameter("img", imgByteArry));
                            int i = cmd.ExecuteNonQuery();
                            if (i == 1)
                            {
                                MessageBox.Show("Image added succesfully.");
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (ArgumentNullException)
            {



            }
            catch (SqlException)
            {
                MessageBox.Show("Some Database Error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Some Error occured!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
