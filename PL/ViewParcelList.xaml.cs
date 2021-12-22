using BO;
using BlApi;
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
    /// Interaction logic for ViewParcelList.xaml
    /// </summary>
    public partial class ViewParcelList : Window
    {
        bool exit = false;
        Customer customer;
        IBL db = BlFactory.GetBl();

        

        public ViewParcelList()
        {
            InitializeComponent();
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            ListViewParcels.ItemsSource = db.GetAllCustomers();
        }

        public ViewParcelList(Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
        }


        private void ResetFilters_btn(object sender, MouseButtonEventArgs e)
        {

        }
        private void updateFilters(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddDrone_clk(object sender, MouseButtonEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }

        private void ListViewDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Again_Gif(object sender, RoutedEventArgs e)
        {

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
