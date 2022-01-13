using BO;
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
using System.Media;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool exit = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewDroneList(object sender, RoutedEventArgs e)
        {

            new ViewDroneList().Show();
        }

        private void viewParcelList(object sender, RoutedEventArgs e)
        {
            new ViewParcelList().Show();
        }
        private void viewStationlList(object sender, RoutedEventArgs e)
        {
            new ViewStationList().Show();
        }

        private void viewCustomerlList(object sender, RoutedEventArgs e)
        {                       
           new ViewCustomerList().ShowDialog();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            this.Close();
        }
        private void CloseWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!exit)
            {
                e.Cancel = true;
                SystemSounds.Beep.Play();
            }
        }
    }
}
