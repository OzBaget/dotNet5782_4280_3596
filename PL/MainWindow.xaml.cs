using System.Media;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool exit = false;
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
            Close();
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
