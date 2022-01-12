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
using System.Collections.ObjectModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for ViewParcelList.xaml
    /// </summary>
    public partial class ViewParcelList : Window
    {
        bool exit = false;
        public Customer customer { get; set; }
        public bool GroupingMode { get; set; }
        IBL db = BlFactory.GetBl();
        public ObservableCollection<ParcelToList> listItems { get; set; }
        
        public ViewParcelList()
        {
            InitializeComponent();

            PrioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            statusSelector.ItemsSource = Enum.GetValues(typeof(ParcelStatus));
            listItems = new ObservableCollection<ParcelToList>(db.GetAllParcels());
            this.DataContext = this;


        }

        public ViewParcelList(Customer customer)
        {
            this.customer = customer;
            this.DataContext = this;

            InitializeComponent();

            PrioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            statusSelector.ItemsSource = Enum.GetValues(typeof(ParcelStatus));

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
            while (listItems.Count > 0)
                listItems.RemoveAt(0);
            foreach (var item in db.GetFilterdParcels(customer,
                datePickerStart.SelectedDate,
                datePickerEnd.SelectedDate,
                (Priorities?)PrioritySelector.SelectedItem,
                (WeightCategories?)WeightSelector.SelectedItem,
                (ParcelStatus?)statusSelector.SelectedItem))
                    listItems.Add(item);
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


        private void CloseWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!exit)
            {
                e.Cancel = true;
                SystemSounds.Beep.Play();
            }
        }

        private void groupingModeChanged(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListViewParcels.ItemsSource);
            if (GroupingMode)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("SenderName");
                view.GroupDescriptions.Add(groupDescription);
            }
            else
            {
                view.GroupDescriptions.Clear();

            }
        }
    }
}
