using System;
using BlApi;
using BO;
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
    /// Interaction logic for ViewDroneList.xaml
    /// </summary>
    public partial class ViewDroneList : Window
    {
        private IBL db;
        bool exit = false;
        public ViewDroneList(IBL database)
        {
            InitializeComponent();
            db = database;
            ListViewDrones.ItemsSource = db.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelector.SelectedItem == null)
                ListViewDrones.ItemsSource = db.GetAllDrones();
            else if (StatusSelector.SelectedItem != null)
                ListViewDrones.ItemsSource = db.GetFilterdDrones((BO.WeightCategories)WeightSelector.SelectedItem, (BO.DroneStatus)StatusSelector.SelectedItem);
            else
                ListViewDrones.ItemsSource = db.GetFilterdDrones((BO.WeightCategories)WeightSelector.SelectedItem, (BO.DroneStatus)StatusSelector.SelectedItem);

        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StatusSelector.SelectedItem == null)
                ListViewDrones.ItemsSource = db.GetAllDrones();
            else if (WeightSelector.SelectedItem != null)
                ListViewDrones.ItemsSource = db.GetFilterdDrones((BO.WeightCategories)WeightSelector.SelectedItem, (BO.DroneStatus)StatusSelector.SelectedItem);
            else
                ListViewDrones.ItemsSource = db.GetFilterdDrones((BO.WeightCategories)WeightSelector.SelectedItem, (BO.DroneStatus)StatusSelector.SelectedItem);

        }
        

        private void ListViewDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ViewDrone(db.GetDrone(((sender as ListView).SelectedItem as BO.DroneToList).Id), db).ShowDialog();
            //refresh listView
            ListViewDrones.ItemsSource = null;
            ListViewDrones.ItemsSource =db.GetAllDrones();
            WeightSelector_SelectionChanged(null, null);
            StatusSelector_SelectionChanged(null, null);
        }

        /// <summary>
        /// Play Gif
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Again_Gif(object sender, RoutedEventArgs e)
        {
            GifDrones.Position = new TimeSpan(0, 0, 1);
            GifDrones.Play();
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
            new ViewDrone(db).ShowDialog();
            //refresh listView
            ListViewDrones.ItemsSource = null;
            ListViewDrones.ItemsSource = db.GetAllDrones();
            WeightSelector_SelectionChanged(null, null);
            StatusSelector_SelectionChanged(null, null);
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
    }

}
