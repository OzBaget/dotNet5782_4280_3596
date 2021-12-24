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

namespace PL
{
    /// <summary>
    /// Interaction logic for ViewStationList.xaml
    /// </summary>
    public partial class ViewStationList : Window
    {
        IBL db = BlFactory.GetBl();
        bool isChagnes = false;
        public ViewStationList()
        {
            InitializeComponent();
            listViewStations.ItemsSource = db.GetAllStations();
            StationView.Visibility = Visibility.Collapsed;
            Update.IsEnabled = false;
        }

        private void listViewStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            showStation(db.GetStation(((sender as ListView).SelectedItem as BO.BaseStationToList).Id));
        }
        private void showStation(BaseStation s)
        {
            
            StationView.Visibility = Visibility.Visible;
            NameBox.Text = s.Name;
            IdBox.Text = s.Id.ToString();
            LocationBox.Text = s.Location.ToString();
            DroneList.ItemsSource = s.DronesInCharging;

        }

        private void IsChanged(object sender, TextChangedEventArgs e)
        {
            BaseStation Cstation = GetCurrentStation();
            if (Update == null)
                return;
            if (NameBox.Text != Cstation.Name)
            {
                Update.IsEnabled = true;
            }
            else 
            {
                Update.IsEnabled = false;
            }
            
        }
        

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            db.UpdateStation(int.Parse(IdBox.Text), NameBox.Text, ((sender as ListView).SelectedItem as BO.BaseStationToList).NumFreeChragers.ToString());
        }

        private BaseStation GetCurrentStation()
        {
            if (listViewStations.SelectedItem == null)
                return new BaseStation();
            return db.GetStation((listViewStations.SelectedItem as BO.BaseStationToList).Id);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            GoAddView();
        }
        private void AddStationToDb(object sender, RoutedEventArgs e)
        {
            NameErrorBox.Text = "";
            IdErrorBox.Text = "";
            LatErrorBox.Text = "";
            LongErrorBox.Text = "";
            int id;
            double lat, longint;
            bool error = false;
            if (NameBox.Text == "")
            {
                NameErrorBox.Text = "Write a name, try again";
                error = true;
            }

            if (IdBox.Text == "")
            {
                IdErrorBox.Text = "Write digits, try again";
                error = true;
            }
            else if (!int.TryParse(IdBox.Text, out id))
            {
                IdErrorBox.Text = "Id not valid, try again";
                error = true;
            }
            if (LatBox.Text == "")
            {
                LatErrorBox.Text = "Write digits, try again";
                error = true;
            }
            else if (!double.TryParse(LatBox.Text, out lat))
            {
                LatErrorBox.Text = "Lat not valid, try again";
                error = true;
            }
            if (LongBox.Text == "")
            {
                LongErrorBox.Text = "Write digits, try again";
                error = true;
            }
            else if (!double.TryParse(LongBox.Text, out longint))
            {
                LongErrorBox.Text = "Long not valid, try again";
                error = true;
            }
            if (error)
                return;

            BO.BaseStation sBase = new BO.BaseStation();
            sBase.Name = NameBox.Text;
            sBase.Id = int.Parse(IdBox.Text);
            sBase.Location = new();
            sBase.Location.Latitude = double.Parse(LatBox.Text);
            sBase.Location.Longitude = double.Parse(LongBox.Text);

            BlApi.BlFactory.GetBl().AddStation(sBase);
            BackViewStation(null,null);
            
        }
        private void BackViewStation(object sender, RoutedEventArgs e)
        {
            StationView.Visibility = Visibility.Collapsed;
            initializationBoxes();
            IdBox.IsReadOnly = true;
            LocationBox.Visibility = Visibility.Visible;
            DroneList.Visibility = Visibility.Visible;
            LatBox.Visibility = Visibility.Collapsed;
            LongBox.Visibility = Visibility.Collapsed;
            LatLable.Visibility = Visibility.Collapsed;
            LongLable.Visibility = Visibility.Collapsed;
            Update.Content = "Update";
            AddStation.Content = "Add a station";
            Update.IsCancel = false;
            AddStation.Click -= AddStationToDb;
            AddStation.Click += Add_Click;
            Update.Click += Update_Click;
            Update.Click -= BackViewStation;
            listViewStations.ItemsSource = db.GetAllStations();

        }
        private void GoAddView()
        {
            StationView.Visibility = Visibility.Visible;
            initializationBoxes();
            IdBox.IsReadOnly = false;
            LocationBox.Visibility = Visibility.Collapsed;
            DroneList.Visibility = Visibility.Collapsed;
            LatBox.Visibility = Visibility.Visible;
            LongBox.Visibility = Visibility.Visible;
            LatLable.Visibility = Visibility.Visible;
            LongLable.Visibility = Visibility.Visible;
            Update.Content = "Cacnel";
            AddStation.Content = "Add";
            Update.IsEnabled = true;
            AddStation.Click += AddStationToDb;
            AddStation.Click -= Add_Click;
            Update.Click += BackViewStation;
            Update.Click -= Update_Click;
        }
        private void initializationBoxes()
        {
            IdBox.Text = "";
            NameBox.Text = "";
            LocationBox.Text = "";
            LatBox.Text = "";
            LongBox.Text = "";
            NameErrorBox.Text = "";
            IdErrorBox.Text = "";
            LatErrorBox.Text = "";
            LongErrorBox.Text = "";
        }
    }
}
