using Microsoft.Win32;
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
using System.Data.SqlClient;

namespace GeorgePairs
{

    public partial class Register : Window
    {
        
        

        public Register()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            App.Current.MainWindow = log;
            this.Close();
            log.Show();
        }

        private void btnToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);


            this.DragMove();
        }

        String imagePathForDb;
        SqlConnection con = new SqlConnection(HIDE);

        private void btnImg1_Click(object sender, RoutedEventArgs e)
        {
            imagePathForDb = "/imagini/1.png";
        }

        private void btnImg2_Click(object sender, RoutedEventArgs e)
        {
            imagePathForDb = "/imagini/2.png";
        }

        private void btnImg3_Click(object sender, RoutedEventArgs e)
        {
            imagePathForDb = "/imagini/3.png";
        }

        private void btnImg4_Click(object sender, RoutedEventArgs e)
        {
            imagePathForDb = "/imagini/4.png";
        }

        private void btnImg5_Click(object sender, RoutedEventArgs e)
        {
            imagePathForDb = "/imagini/5.png";
        }

        private void btnImg6_Click(object sender, RoutedEventArgs e)
        {
            imagePathForDb = "/imagini/6.png";
        }

        private void btnImg7_Click(object sender, RoutedEventArgs e)
        {
            imagePathForDb = "/imagini/7.png";
        }

        private void btnRegSucces_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxUsername.Text) && !String.IsNullOrEmpty(tbxParola.Password) && !String.IsNullOrEmpty(imagePathForDb)) { 
            try
            {
                con.Open();
                String query = "INSERT into players(username,password,avatar) VALUES('" + tbxUsername.Text + "','" + tbxParola.Password + "','" + imagePathForDb + "')";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.ExecuteNonQuery();
                con.Close();
                    

                    LabelSucces.Content = "Contul s-a creat cu succes!";
                    LabelError.Content = "";

                    
                
                    
            }
            catch
            {
                LabelError.Content = "E R O A R E!";
            }
            }
            else
            {
                LabelError.Content = "Completeaza campurile si selecteaza avatarul!";
            }
        }   
    }
}
