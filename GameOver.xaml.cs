using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GeorgePairs
{
    
    public partial class GameOver : Window
    {
        public GameOver()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            

        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window window in Application.Current.Windows.OfType<LevelOne>())
            {
                LevelOne openAgainLevelOne = new LevelOne();
                App.Current.MainWindow = openAgainLevelOne;
                this.Close();
                openAgainLevelOne.Show();
                if (window != openAgainLevelOne)
                    window.Close();
            }

            foreach (Window window in Application.Current.Windows.OfType<LevelTwo>())
            {
                LevelTwo openAgainLevelTwo = new LevelTwo();
                App.Current.MainWindow = openAgainLevelTwo;
                this.Close();
                openAgainLevelTwo.Show();
                if (window != openAgainLevelTwo)
                    window.Close();
            }



            foreach (Window window in Application.Current.Windows.OfType<LevelThree>())
            {
                LevelThree openAgainLevelThree = new LevelThree();
                App.Current.MainWindow = openAgainLevelThree;
                this.Close();
                openAgainLevelThree.Show();
                if (window != openAgainLevelThree)
                    window.Close();
            }

        }

        private void btnInfirm_Click(object sender, RoutedEventArgs e)
        {


            App.Current.Shutdown();
        }
    }
}
