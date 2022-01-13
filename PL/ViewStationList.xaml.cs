using BlApi;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace PL
{
    /// <summary>
    /// Interaction logic for ViewStationList.xaml
    /// </summary>
    public partial class ViewStationList : Window
    {
        private IBL db = BlFactory.GetBl();
        public bool GroupingMode { get; set; }

        private bool exit = false;
        public ViewStationList()
        {
            InitializeComponent();
            listViewStations.ItemsSource = db.GetAllStations();
            DataContext = this;
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
            exit = true;
            Close();
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
