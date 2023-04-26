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
using System.Text.RegularExpressions;
using System.Data.SqlClient; //add from here
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;


namespace cwainmenuexs1
{
    /// <summary>
    /// Interaction logic for Buyer.xaml
    /// </summary>
    public partial class Buyer : Window
    {
        public Buyer()
        {
            InitializeComponent();
        }
        databaseacces reg = new databaseacces();

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet ds;



        string no;
        string strName, imageName;
        string constring = "Data Source=MSI;Initial Catalog=FarmSystem;Integrated Security=True";
        private void BuyerForm_Loaded(object sender, RoutedEventArgs e)
        {
            reg.load();
        }

        int i = 1;
        private void btn_signUp_Click(object sender, RoutedEventArgs e)
        {
            if (i < 6)
            {
                if (txt_name.Text.Length == 0)
                {
                    lbl_name.Content = "* Please Enter your Name";
                    txt_name.Focus();
                    i = 1;
                }
                else if (txt_name.Text.Any(char.IsDigit))
                {
                    lbl_name.Content = "* Name cannot contain any number";
                    txt_name.Focus();
                    i = 1;
                }
                else
                {
                    lbl_name.Content = "";
                    i++;
                }

                if (txt_address.Text.Length == 0)
                {
                    lbl_address.Content = "* Please Enter your address";
                    txt_address.Focus();
                    i = 1;
                }
                else
                {
                    lbl_address.Content = "";
                    i++;
                }

                if (txt_tp.Text.Length == 0)
                {
                    lbl_tp.Content = "* Please Enter your Telphone number";
                    txt_tp.Focus();
                    i = 1;
                }
                else if (!Regex.IsMatch(txt_tp.Text, @"^(?:7|0|(?:\+94))[0-9]{8,9}$"))
                {
                    lbl_tp.Content = "* Invalid Telephone number";
                    txt_tp.Focus();
                    i = 1;
                }
                else
                {
                    lbl_tp.Content = "";
                    i++;
                }

                if (txt_email.Text.Length == 0)
                {
                    lbl_email.Content = "* Please enter your email";
                    txt_email.Focus();
                    i = 1;
                }
                else if (!Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    lbl_email.Content = "* Invalid Email(ex: abc@gamil.com)";
                    txt_email.Focus();
                    i = 1;
                }
                else
                {
                    lbl_email.Content = "";
                    i++;
                }

                if (pwd.Password.Length == 0)
                {
                    lbl_pwd.Foreground = System.Windows.Media.Brushes.Red;
                    lbl_pwd.Content = "* Please enter a Password";
                    lbl_pwd.Focus();
                    i = 1;
                }
                else if (pwd.Password.Length < 6)
                {
                    lbl_pwd.Foreground = System.Windows.Media.Brushes.Red;
                    lbl_pwd.Content = "* Not enough length";
                    lbl_pwd.Focus();
                    i = 1;
                }
                else if (!pwd.Password.Any(char.IsDigit))
                {
                    lbl_pwd.Foreground = System.Windows.Media.Brushes.Red;
                    lbl_pwd.Content = "* Password should contain numbers";
                    lbl_pwd.Focus();
                    i = 1;
                }
                else if (!pwd.Password.Any(char.IsLetter))
                {
                    lbl_pwd.Foreground = System.Windows.Media.Brushes.Red;
                    lbl_pwd.Content = "* Password should contain letters";
                    lbl_pwd.Focus();
                    i = 1;
                }
                else
                {
                    lbl_pwd.Content = "";
                    i++;
                }

                if (pwd_confirm.Password.Length == 0)
                {
                    lbl_confirm.Content = "* Confirm your password";
                    lbl_confirm.Focus();
                    i = 1;
                }
                else if (pwd_confirm.Password != pwd.Password)
                {
                    lbl_confirm.Content = "* Password is not matching";
                    lbl_confirm.Focus();
                    i = 1;
                }
                else
                {
                    lbl_confirm.Content = "";
                    i++;
                }
            }
            else if (i >= 7)
            {
                MessageBox.Show("Your data are storing.Please wait a moment...", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                string q = "insert into buyerRegistration values ('" + txt_name.Text + "','" + txt_address.Text + "','" + txt_tp.Text + "','" + txt_email.Text + "','" + cmb_type.Text + "','" + pwd.Password + "','" + pwd_confirm.Password + "')";
                int l = reg.insert_data(q);

                string q2 = "select * from buyerRegistration;";
                int count = reg.get_farmerId(q2);
                string id = null;
                if (count == 0)
                    MessageBox.Show("Sorry..Something went wrong, Try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (count < 10)
                    id = "B00" + count;
                else if (count < 100)
                    id = "B0" + count;
                else if (count < 1000)
                    id = "B" + count;

                if (l == 1)
                {
                    MessageBox.Show(" Congratulatons! You registerd to our community succesfully... \n Your USER ID : " + id + " \n (Remeber your user Id to log into your profile.)", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    saveImage(id);
                    txt_name.Clear();
                    txt_address.Clear();
                    txt_tp.Clear();
                    txt_email.Clear();
                    pwd.Clear();
                    pwd_confirm.Clear();

                    lbl_name.Content = "";
                    lbl_address.Content = "";
                    lbl_tp.Content = "";
                    lbl_email.Content = "";
                    lbl_pwd.Content = "";
                    lbl_confirm.Content = "";
                }
                else
                    MessageBox.Show("Sorry..Something went wrong, Try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                i = 1;
            }
        }


        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_name.Clear();
            txt_address.Clear();
            txt_tp.Clear();
            txt_email.Clear();
            pwd.Clear();
            pwd_confirm.Clear();

            lbl_name.Content = "";
            lbl_address.Content = "";
            lbl_tp.Content = "";
            lbl_email.Content = "";
            lbl_pwd.Content = "";
            lbl_confirm.Content = "";
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Close();
            blogin obj = new blogin();
            obj.Show();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Buttopower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
            blogin obj = new blogin();
            obj.Show();
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
    


        public void saveImage(string id)
        {
            try
            {
                FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                byte[] imgByteArry = new byte[fs.Length];
                fs.Read(imgByteArry, 0, Convert.ToInt32(fs.Length));
                fs.Close();


                using (con = new SqlConnection(constring))
                {
                    con.Open();
                    string sql = "Insert into userImages values('" + id + "','" + strName + "', @img);";
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
