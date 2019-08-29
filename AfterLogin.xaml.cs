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
    



    public partial class AfterLogin : Window
    {

        String imagePath;

        public AfterLogin()
        {
            
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            
            InitializeComponent();
            playerName.Text = Login.StringUserForNextForm;
            SqlConnection con = new SqlConnection(HIDE);

            SqlCommand com = new SqlCommand("select * from players where username='" + playerName.Text + "'", con);
            con.Open();
            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                imagePath = read.GetString(2);

            }
            read.Close();
            con.Close();
            MyImage.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,"+imagePath) as ImageSource;

        }

        private void AfterLogin_Load(object sender, EventArgs e)
        {
            playerName.Text = Login.StringUserForNextForm;
            

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLevel1_Click(object sender, RoutedEventArgs e)
        {
            
            LevelOne main = new LevelOne();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void btnLevel2_Click(object sender, RoutedEventArgs e)
        {
            LevelTwo main = new LevelTwo();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void btnLevel3_Click(object sender, RoutedEventArgs e)
        {
            LevelThree main = new LevelThree();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
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
    }
}
