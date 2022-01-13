using BlApi;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL
{
    /// <summary>
    /// Interaction logic for ViewCustomerList.xaml
    /// </summary>
    public partial class ViewCustomerList : Window
    {
        private IBL db = BlFactory.GetBl();
        private bool exit = false;

        public ViewCustomerList()
        {
            InitializeComponent();
            ResetList();
        }

        private void listViewStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ViewCustomer(db.GetCustomer(((sender as ListView).SelectedItem as BO.CustomerToList).Id)).ShowDialog();
            ResetList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new ViewCustomer().ShowDialog();
            ResetList();
        }

        private void ResetList()
        {
            listViewStations.ItemsSource = db.GetAllCustomers();
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
