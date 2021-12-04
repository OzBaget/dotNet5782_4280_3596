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

namespace PL
{
    /// <summary>
    /// Interaction logic for ViewDroneList.xaml
    /// </summary>
    public partial class ViewDroneList : Window
    {
        private IBL.IBL db;
        public ViewDroneList()
        {
            InitializeComponent();
        }
        public ViewDroneList(IBL.IBL database)
        {
            InitializeComponent();
            db = database;
            ListViewDrones.ItemsSource = null;
            ListViewDrones.ItemsSource = db.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelector.SelectedItem == null)
                ListViewDrones.ItemsSource = db.GetAllDrones();
            else if (StatusSelector.SelectedItem != null)
                ListViewDrones.ItemsSource = db.GetFilterdDrones(d => (IBL.BO.WeightCategories)WeightSelector.SelectedItem == d.MaxWeight && (IBL.BO.DroneStatus)StatusSelector.SelectedItem == d.Status);
            else
                ListViewDrones.ItemsSource = db.GetFilterdDrones(d => (IBL.BO.WeightCategories)WeightSelector.SelectedItem == d.MaxWeight);

        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StatusSelector.SelectedItem == null)
                ListViewDrones.ItemsSource = db.GetAllDrones();
            else if (WeightSelector.SelectedItem != null)
                ListViewDrones.ItemsSource = db.GetFilterdDrones(d => (IBL.BO.DroneStatus)StatusSelector.SelectedItem == d.Status && (IBL.BO.WeightCategories)WeightSelector.SelectedItem == d.MaxWeight);
            else
                ListViewDrones.ItemsSource = db.GetFilterdDrones(d => (IBL.BO.DroneStatus)StatusSelector.SelectedItem == d.Status);

        }
        

        private void ListViewDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                   new ViewDrone(db.GetDrone(((sender as ListView).SelectedItem as IBL.BO.DroneToList).Id)).Show();
        }

        private void Resetbutton_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
            WeightSelector.SelectedItem = null;
            ListViewDrones.ItemsSource = db.GetAllDrones();

        }
    }

}
