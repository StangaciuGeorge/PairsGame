using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GeorgePairs
{

    

    public partial class LevelOne : Window
    {

        String imagePath;
        int showCardsTime = 5;
        int playTime = 45;
        DispatcherTimer Timer;
        DispatcherTimer flipTime;
        DispatcherTimer gameTime;
        Image GetValueFlippedImage1;
        Image GetValueFlippedImage2;


        Thickness[] buttonsMargins = new Thickness[16];

        

        public LevelOne()
        {

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
            
            playerName.Text = Login.StringUserForNextForm;
            SqlConnection con = new SqlConnection(@"Data Source=den1.mssql5.gear.host;Initial Catalog=georgepairs;User ID=georgepairs;Password=Tx46W-0l37?h");

            // Greseala: predispunere SQL Injection

            SqlCommand com = new SqlCommand("select * from players where username='" + playerName.Text + "'", con);
            con.Open();
            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                imagePath = read.GetString(2);

            }
            read.Close();
            con.Close();
            MyImage.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,," + imagePath) as ImageSource;

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();

            flipTime = new DispatcherTimer();
            flipTime.Interval = new TimeSpan(0, 0, 0, 0, 500);
            flipTime.Tick += flipTime_Tick;

            gameTime = new DispatcherTimer();
            gameTime.Interval = new TimeSpan(0, 0, 1);
            gameTime.Tick += gameTime_Tick;


            int i = 0;
            foreach (Button btn in FindVisualChildren<Button>(imageTableGrid))
            {
                buttonsMargins[i] = btn.Margin;
                
                i++;
            }

            Random random = new Random();
            Thickness[] randomButtonsMargins = buttonsMargins.OrderBy(x => random.Next()).ToArray();

            int index = 0;
            foreach (Button btn in FindVisualChildren<Button>(imageTableGrid))
            {
                btn.Margin = randomButtonsMargins[index];
                index++;
            }


        }


        // Find all controls in WPF Window by type
        // https://stackoverflow.com/questions/974598/find-all-controls-in-wpf-window-by-type

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


        public void flipTime_Tick(object sender, EventArgs e)
        {
            
            flipTime.Stop();
            GetValueFlippedImage1.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
            GetValueFlippedImage2.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;

            GetValueFlippedImage1= null;
            GetValueFlippedImage2 = null;
            

           
            

        }

        public void gameTime_Tick(object sender, EventArgs e)
        {
            if (playTime > 0)
            {
                playTime--;
                gameCountDownBeforeStart.Text = playTime.ToString();

                if (btnImage1.IsEnabled == false && btnImage2.IsEnabled == false && btnImage3.IsEnabled == false && btnImage4.IsEnabled == false
                       && btnImage5.IsEnabled == false && btnImage6.IsEnabled == false && btnImage7.IsEnabled == false && btnImage8.IsEnabled == false
                       && btnImage9.IsEnabled == false && btnImage10.IsEnabled == false && btnImage11.IsEnabled == false && btnImage12.IsEnabled == false
                       && btnImage13.IsEnabled == false && btnImage14.IsEnabled == false && btnImage15.IsEnabled == false && btnImage16.IsEnabled == false)
                {
                    gameTime.Stop();
                    
                    System.IO.Stream gameWinStream = Properties.Resources.gameWin;
                    System.Media.SoundPlayer gameWinPlayer = new System.Media.SoundPlayer(gameWinStream);
                    gameWinPlayer.Play();
                    GameWin main = new GameWin();
                    App.Current.MainWindow = main;
                    main.Show();
                    
                }
                else if (playTime == 0)
                {
                    btnImage1.IsEnabled = false;
                    btnImage2.IsEnabled = false;
                    btnImage3.IsEnabled = false;
                    btnImage4.IsEnabled = false;
                    btnImage5.IsEnabled = false;
                    btnImage6.IsEnabled = false;
                    btnImage7.IsEnabled = false;
                    btnImage8.IsEnabled = false;
                    btnImage9.IsEnabled = false;
                    btnImage10.IsEnabled = false;
                    btnImage11.IsEnabled = false;
                    btnImage12.IsEnabled = false;
                    btnImage13.IsEnabled = false;
                    btnImage14.IsEnabled = false;
                    btnImage15.IsEnabled = false;
                    btnImage16.IsEnabled = false;
                    gameCountDownBeforeStart.Text = "";
                    gameCountDownPlay.TextAlignment = TextAlignment.Center;
                    gameCountDownPlay.Foreground = new SolidColorBrush(Colors.Red);
                    gameCountDownPlay.Text = "Joc terminat";

                    GameOver main = new GameOver();
                    App.Current.MainWindow = main;
                    main.Show();
                    
                    System.IO.Stream gameOverStream = Properties.Resources.gameOver;
                    System.Media.SoundPlayer gameOverPlayer = new System.Media.SoundPlayer(gameOverStream);
                    gameOverPlayer.Play();



                }
                


            }
           
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            if (showCardsTime > 0)
            {
                    showCardsTime--;
                countdown.Text = showCardsTime.ToString();
                if (showCardsTime == 0)
                {
                    countdown.Text = " ";
                    wishGoodLuck.FontSize = 28;
                    wishGoodLuck.Text = "Succes!";
                    
                }

            
            }
            else
            {
                Timer.Stop();
                
                gameTime.Start();
                image1.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image2.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image3.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image4.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image5.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image6.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image7.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image8.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image9.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image10.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image11.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image12.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image13.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image14.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image15.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                image16.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/cover.png") as ImageSource;
                
                
            }
        }

        private void LevelOne_Load(object sender, EventArgs e)
        {
            playerName.Text = Login.StringUserForNextForm;
          
        }

        

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        
        
        

        private void btnNextLevel_Click(object sender, RoutedEventArgs e)
        {
            gameTime.Stop();
            playTime = 0;
           LevelTwo main = new LevelTwo();
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

       

        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            gameTime.Stop();
            playTime = 0;
            AfterLogin main = new AfterLogin();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();

        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
           
            Help main = new Help();
            App.Current.MainWindow = main;
            main.Show();


        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);


            this.DragMove();
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                e.Handled = true;
            }
        }

        #region Flip la click
        private void btnImage1_Click(object sender, RoutedEventArgs e)
        {
            image1.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare1.png") as ImageSource;
            if (GetValueFlippedImage1 == null)
            {

                GetValueFlippedImage1 = image1;
            }
            else if(GetValueFlippedImage1!=null && GetValueFlippedImage2==null){

                GetValueFlippedImage2 = image1;

            }
            if(GetValueFlippedImage1!=null && GetValueFlippedImage2 != null)
            {
                if(GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {

                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    
                    snd.Play();
                    btnImage1.IsEnabled = false;
                    btnImage9.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                }
                else
                {
                    
                    flipTime.Start();
                }

                
            }
        }

        private void btnImage2_Click(object sender, RoutedEventArgs e)
        {
            image2.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare2.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image2;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image2;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage2.IsEnabled = false;
                    btnImage10.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;

                  

                }
                else
                {

                   
                    flipTime.Start();
                }

               
            }
        }

        private void btnImage3_Click(object sender, RoutedEventArgs e)
        {
            image3.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare3.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image3;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image3;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage3.IsEnabled = false;
                    btnImage11.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                        
                    
                }
                else
                {
                    
                    flipTime.Start();
                }

               
            }
        }

        private void btnImage4_Click(object sender, RoutedEventArgs e)
        {
            image4.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare4.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image4;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image4;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage4.IsEnabled = false;
                    btnImage12.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                        
                    
                }
                else
                {
                    flipTime.Start();
                }
               

            }
        }

        private void btnImage5_Click(object sender, RoutedEventArgs e)
        {
            image5.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare5.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image5;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image5;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage5.IsEnabled = false;
                    btnImage13.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                       
                    
                }
                else
                {
                    flipTime.Start();
                }

                
            }
        }

        private void btnImage6_Click(object sender, RoutedEventArgs e)
        {
            image6.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare6.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image6;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image6;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage6.IsEnabled = false;
                    btnImage14.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                       
                    
                }
                else
                {
                    flipTime.Start();
                }
                

            }
        }

        private void btnImage7_Click(object sender, RoutedEventArgs e)
        {
            image7.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare7.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image7;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image7;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage7.IsEnabled = false;
                    btnImage15.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                    
                        
                    
                }
                else
                {
                    
                    flipTime.Start();
                }

               
            }
        }

        private void btnImage8_Click(object sender, RoutedEventArgs e)
        {
            image8.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare8.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image8;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image8;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage8.IsEnabled = false;
                    btnImage16.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                       
                    
                }
                else
                {
                    flipTime.Start();
                }
               

            }
        }

        private void btnImage9_Click(object sender, RoutedEventArgs e)
        {
            image9.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare1.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image9;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image9;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage1.IsEnabled = false;
                    btnImage9.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                       
                    
                }
                else
                {
                    flipTime.Start();
                }

                
            }
        }

        private void btnImage10_Click(object sender, RoutedEventArgs e)
        {
            image10.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare2.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image10;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image10;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage2.IsEnabled = false;
                    btnImage10.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                        
                    
                }
                else
                {
                    flipTime.Start();
                }
                

            }
        }

        private void btnImage11_Click(object sender, RoutedEventArgs e)
        {
            image11.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare3.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image11;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image11;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
        if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
        {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage3.IsEnabled = false;
            btnImage11.IsEnabled = false;
            GetValueFlippedImage1 = null;
            GetValueFlippedImage2 = null;


        }

        else
        {
            flipTime.Start();
        }
                

            }
        }

        private void btnImage12_Click(object sender, RoutedEventArgs e)
        {
            image12.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare4.png") as ImageSource;

            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image12;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image12;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage4.IsEnabled = false;
                    btnImage12.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                       
                    
                }
                else
                {
                    flipTime.Start();
                }

               
            }
        }

        private void btnImage13_Click(object sender, RoutedEventArgs e)
        {
            image13.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare5.png") as ImageSource;
            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image13;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image13;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage5.IsEnabled = false;
                    btnImage13.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                   
                        
                    
                }
                else
                {

                    flipTime.Start();
                }


            }
        }

        private void btnImage14_Click(object sender, RoutedEventArgs e)
        {
            image14.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare6.png") as ImageSource;
            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image14;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image14;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage6.IsEnabled = false;
                    btnImage14.IsEnabled = false;

                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                    
                        
                    
                }
                else
                {

                    flipTime.Start();
                }


            }
        }

        private void btnImage15_Click(object sender, RoutedEventArgs e)
        {
            image15.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare7.png") as ImageSource;
            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image15;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image15;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage7.IsEnabled = false;
                    btnImage15.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                    
                }
                else
                {

                    flipTime.Start();
                }


            }
        }

        private void btnImage16_Click(object sender, RoutedEventArgs e)
        {
            image16.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/imagini/pasare8.png") as ImageSource;
            if (GetValueFlippedImage1 == null)
            {
                GetValueFlippedImage1 = image16;
            }
            else if (GetValueFlippedImage1 != null && GetValueFlippedImage2 == null)
            {

                GetValueFlippedImage2 = image16;

            }
            if (GetValueFlippedImage1 != null && GetValueFlippedImage2 != null)
            {
                if (GetValueFlippedImage1.Uid == GetValueFlippedImage2.Uid)
                {
                    System.IO.Stream str = Properties.Resources.succes;
                    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                    snd.Play();
                    btnImage8.IsEnabled = false;
                    btnImage16.IsEnabled = false;
                    GetValueFlippedImage1 = null;
                    GetValueFlippedImage2 = null;
                    
                       
                    
                }
                else
                {

                    flipTime.Start();
                }


            }
        }

        #endregion

        
    }
}
