using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for ViewDroneList.xaml
    /// </summary>
    public partial class ViewDroneList : Window
    {
        private IBL db = BlFactory.GetBl();
        private bool exit = false;
        private List<int> Id = new List<int>();
        public bool GroupingMode { get; set; }

        public ViewDroneList()
        {
            InitializeComponent();
            ListViewDrones.ItemsSource = db.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            DataContext = this;
        }

        private void updateFilters(object sender, SelectionChangedEventArgs e)
        {
            ListViewDrones.ItemsSource = db.GetFilterdDrones((BO.WeightCategories?)WeightSelector.SelectedItem, (BO.DroneStatus?)StatusSelector.SelectedItem);
        }


        private void ListViewDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Drone dr = db.GetDrone(((sender as ListView).SelectedItem as BO.DroneToList).Id);
            if (!Id.Any(id => id == dr.Id))//make sure that the simulator is not run on this drone in another window
            {
                Id.Add(dr.Id);
                ViewDrone window = new ViewDrone(dr);
                window.Closing += (sender, e) => Id.Remove(dr.Id);
                window.DataContextChanged += updateList;
                //refresh listView
                window.Show();
            }

        }

        private void updateList(object sender, DependencyPropertyChangedEventArgs e)
        {
            ListViewDrones.Items.Refresh();
        }

        /// <summary>
        /// Reset filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetFilters_btn(object sender, MouseButtonEventArgs e)
        {
            StatusSelector.SelectedItem = null;
            WeightSelector.SelectedItem = null;
            ListViewDrones.ItemsSource = db.GetAllDrones();
        }
        /// <summary>
        /// add drone function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDrone_clk(object sender, MouseButtonEventArgs e)
        {
            new ViewDrone().ShowDialog();
            //refresh listView
            ListViewDrones.ItemsSource = null;
            ListViewDrones.ItemsSource = db.GetAllDrones();
            updateFilters(null, null);
        }

        /// <summary>
        /// cancel function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }
        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListViewDrones.ItemsSource);
            if (GroupingMode)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
                view.GroupDescriptions.Add(groupDescription);
            }
            else
            {
                view.GroupDescriptions.Clear();

            }
        }
    }

}
