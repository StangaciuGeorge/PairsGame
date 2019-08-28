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
    
    public partial class Help : Window
    {
        public Help()
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
            this.Close();
            
                
        }

        private void btnInfirm_Click(object sender, RoutedEventArgs e)
        {
            
            App.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);


            this.DragMove();
        }
    }
}
