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
    /// Interaction logic for ViewStation.xaml
    /// </summary>
    public partial class ViewStation : Window
    {
        BaseStation station;
        IBL db;
        public ViewStation(BaseStation Cstation)
        {
            InitializeComponent();
            station = Cstation;
            db = BlFactory.GetBl();
            showStation();
        } 
        public ViewStation()
        {
            InitializeComponent();
            db = BlFactory.GetBl();
            GoAddView();
        }
        private void showStation()
        {
            ViewAStation(null, null);
            NameBox.Text = station.Name;
            IdBox.Text = station.Id.ToString();
            LocationBox.Text = station.Location.ToString();
            DroneList.ItemsSource = station.DronesInCharging;

        }

        private void IsChanged(object sender, TextChangedEventArgs e)
        {
            
            if (station == null)
                return;
            if (NameBox.Text != station.Name)
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
            db.UpdateStation(int.Parse(IdBox.Text), NameBox.Text, station.NumFreeChargers.ToString());
            MessageBox.Show("Update succeed", "Update station", MessageBoxButton.OK, MessageBoxImage.Asterisk);


        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

            try
            {
                BlApi.BlFactory.GetBl().AddStation(sBase);
                Exit_Click(sender,e);
            }
            catch (BlApi.IdAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message, "Can't add station", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void ViewAStation(object sender, RoutedEventArgs e)
        {
            IdBox.IsReadOnly = true;
            LocationBox.Visibility = Visibility.Visible;
            DroneList.Visibility = Visibility.Visible;
            DroneInChargingLable.Visibility = Visibility.Visible;
            DroneLable.Visibility = Visibility.Visible; 
            LatBox.Visibility = Visibility.Collapsed;
            LongBox.Visibility = Visibility.Collapsed;
            LatLable.Visibility = Visibility.Collapsed;
            LongLable.Visibility = Visibility.Collapsed;
            Update.Content = "Update";
            Update.IsCancel = false;
            Update.Click += Update_Click;
            Update.Click -= AddStationToDb;
            NameBox.Text = station.Name;
            IdBox.Text = station.Id.ToString();
            LocationBox.Text = station.Location.ToString();
            DroneList.ItemsSource = station.DronesInCharging;


        }
        private void GoAddView()
        {
            initializationBoxes();
            IdBox.IsReadOnly = false;
            LocationBox.Visibility = Visibility.Collapsed;
            DroneList.Visibility = Visibility.Collapsed;
            DroneInChargingLable.Visibility = Visibility.Collapsed;
            DroneLable.Visibility = Visibility.Collapsed;
            LatBox.Visibility = Visibility.Visible;
            LongBox.Visibility = Visibility.Visible;
            LatLable.Visibility = Visibility.Visible;
            LongLable.Visibility = Visibility.Visible;
            Update.Content = "Add";
            Update.IsEnabled = true;
            Update.Click += AddStationToDb;
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



        private void DroneList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ViewDrone(db.GetDrone(((sender as ListView).SelectedItem as BO.DroneInCharging).Id)).ShowDialog();
        }
    }
}
