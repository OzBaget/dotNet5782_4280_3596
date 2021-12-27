using BO;
using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Customer customer { get; set; }
        IBL db = BlFactory.GetBl();

        

        public ViewParcelList()
        {
            InitializeComponent();
            this.DataContext = this;

            PrioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            statusSelector.ItemsSource = Enum.GetValues(typeof(ParcelStatus));


            ListViewParcels.ItemsSource = db.GetAllParcels();
        }

        public ViewParcelList(Customer customer)
        {
            this.customer = customer;
            this.DataContext = this;

            InitializeComponent();

            PrioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            ListViewParcels.ItemsSource = db.GetFilterdParcels(customer, null, null, null, null, null);
        }


        private void ResetFilters_btn(object sender, MouseButtonEventArgs e)
        {
            PrioritySelector.SelectedItem = null;
            WeightSelector.SelectedItem = null;
            statusSelector.SelectedItem = null;
            datePickerStart.SelectedDate = null;
            datePickerEnd.SelectedDate = null;
        }
        private void updateFilters(object sender, SelectionChangedEventArgs e)
        {
            ListViewParcels.ItemsSource = null;
            ListViewParcels.ItemsSource = db.GetFilterdParcels(customer, 
                datePickerStart.SelectedDate,
                datePickerEnd.SelectedDate,
                (Priorities?)PrioritySelector.SelectedItem,
                (WeightCategories?)WeightSelector.SelectedItem,
                (ParcelStatus?)statusSelector.SelectedItem);
        }

        private void AddDrone_clk(object sender, MouseButtonEventArgs e)
        {
            new ViewParcel(customer).ShowDialog();
            updateFilters(null, null);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }

        private void viewDrone(object sender, MouseButtonEventArgs e)
        {
            new ViewParcel(db.GetParcel(((sender as ListBox).SelectedItem as ParcelToList).Id), customer).ShowDialog();
            updateFilters(null, null);
        }

        private void Again_Gif(object sender, RoutedEventArgs e)
        {
            GifDrones.Position = new TimeSpan(0, 0, 1);
            GifDrones.Play();
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
