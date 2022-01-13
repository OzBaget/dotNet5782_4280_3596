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

namespace PL
{
    /// <summary>
    /// Interaction logic for ViewStationList.xaml
    /// </summary>
    public partial class ViewStationList : Window
    {
        IBL db = BlFactory.GetBl();
        public bool GroupingMode { get; set; }

        public ViewStationList()
        {
            InitializeComponent();
            listViewStations.ItemsSource = db.GetAllStations();
            this.DataContext = this;
        }

        private void listViewStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null)
                return;
            new ViewStation(db.GetStation(((sender as ListView).SelectedItem as BO.BaseStationToList).Id)).ShowDialog();
            ResetList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new ViewStation().ShowDialog();
            ResetList();
        }  
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ResetList()
        {
            listViewStations.ItemsSource = null;
            listViewStations.ItemsSource = db.GetAllStations();

        }
        private void groupingModeChanged(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewStations.ItemsSource);
            if (GroupingMode)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("NumFreeChragers");
                view.GroupDescriptions.Add(groupDescription);
            }
            else
            {
                view.GroupDescriptions.Clear();

            }
        }
    }
}
