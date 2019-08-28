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
  



    public partial class Login : Window
    {

        public static string StringUserForNextForm = "";
        

        public Login()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            
        }

        

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
        private void btnToMain1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }


        private void btnApproveLogin_Click(object sender, RoutedEventArgs e)
        {

           
            SqlConnection co = new SqlConnection(@"HIDE");
            co.Open();
            SqlCommand cmd = new SqlCommand("select * from players where username='"+TbxUsernameLogin.Text+"' and password='"+tbxPasswordLogin.Password +"'", co);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            
            int count = 0;
            while (dr.Read())
            {
                
                count += 1;
            }

            if (count == 1) {
                StringUserForNextForm = TbxUsernameLogin.Text;
                AfterLogin main = new AfterLogin();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
            else
            {
                LabelErrorLogin.Content = "Datele nu au fost introduse corect!";
            }
            


        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);


            this.DragMove();
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

        
    }
}
