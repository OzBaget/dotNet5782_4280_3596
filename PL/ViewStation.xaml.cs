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
using System.Windows.Navigation;
using System.Media;


namespace PL
{
    /// <summary>
    /// Interaction logic for ViewStation.xaml
    /// </summary>
    public partial class ViewStation : Window
    {
        
        public BaseStation station { get; set; }
        IBL db;
        bool exit = false;
        public ViewStation(BaseStation Cstation)
        {
            station = Cstation;
            this.DataContext = station;
            InitializeComponent();
            db = BlFactory.GetBl();
            showStation();            
        } 
        public ViewStation()
        {
            station = new();
            station.Location = new();
            this.DataContext = station;
            InitializeComponent();
            db = BlFactory.GetBl();
            GoAddView();
        }
        private void showStation()
        {
            IdBox.IsReadOnly = true;
            LocationBox.Visibility = Visibility.Visible;
            LocationLable.Visibility = Visibility.Visible;
            DroneList.Visibility = Visibility.Visible;
            DroneLable.Visibility = Visibility.Visible;
            Update.Visibility = Visibility.Visible;
            ExitButton.Visibility = Visibility.Visible;

            LatBox.Visibility = Visibility.Collapsed;
            LongBox.Visibility = Visibility.Collapsed;
            LatLable.Visibility = Visibility.Collapsed;
            LongLable.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            AddButton.Visibility = Visibility.Collapsed;
            AddImage.Visibility = Visibility.Collapsed;
           

        }


        private void Exit()
        {
            this.Close();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int free;

                if (!int.TryParse(FreeBox.Text, out free) || free <= 0)
                {
                    FreeErrorBox.Text = "Num of free chargers not valid, try again";
                    return;
                }                                 
            db.UpdateStation(station.Id, station.Name, station.NumFreeChargers.ToString());
            MessageBox.Show("Update succeed", "Update station", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            FreeErrorBox.Text = "";
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            this.Close();
        }
        private void AddStationToDb(object sender, RoutedEventArgs e)
        {
            NameErrorBox.Text = "";
            IdErrorBox.Text = "";
            LatErrorBox.Text = "";
            LongErrorBox.Text = "";
            int id,free;
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
            else if (!int.TryParse(IdBox.Text, out id) || id <= 0)
            {
                IdErrorBox.Text = "Id not valid, try again";
                error = true;
            }
            if (FreeBox.Text == "")
            {
                FreeErrorBox.Text = "Write digits, try again";
                error = true;
            }
            else if (!int.TryParse(FreeBox.Text, out free) || free <= 0)
            {
                FreeErrorBox.Text = "Num of free chargers not valid, try again";
                error = true;
            }
            if (LatBox.Text == "")
            {
                LatErrorBox.Text = "Write digits, try again";
                error = true;
            }
            else if (!double.TryParse(LatBox.Text, out lat) || lat < -90 || lat > 90)
            {
                LatErrorBox.Text = "Lat not valid, try again";
                error = true;
            }
            if (LongBox.Text == "")
            {
                LongErrorBox.Text = "Write digits, try again";
                error = true;
            }
            else if (!double.TryParse(LongBox.Text, out longint)|| longint < -180 || longint > 180)
            {
                LongErrorBox.Text = "Long not valid, try again";
                error = true;
            }
            if (error)
                return;

            

            try
            {
                BlApi.BlFactory.GetBl().AddStation(station);
                Exit_Click(sender,e);
            }
            catch (BlApi.IdAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message, "Can't add station", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
       
        private void GoAddView()
        {
            IdBox.IsReadOnly = false;
            LocationBox.Visibility = Visibility.Collapsed;
            LocationLable.Visibility = Visibility.Collapsed;
            DroneList.Visibility = Visibility.Collapsed;
            DroneLable.Visibility = Visibility.Collapsed;
            Update.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Collapsed;

            LatBox.Visibility = Visibility.Visible;
            LongBox.Visibility = Visibility.Visible;
            LatLable.Visibility = Visibility.Visible;
            LongLable.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Visible;
            AddImage.Visibility = Visibility.Visible;





        }
        


        private void DroneList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null)
                return;
            new ViewDrone(db.GetDrone(((sender as ListView).SelectedItem as BO.DroneInCharging).Id)).ShowDialog();
            station = db.GetStation(station.Id);
            this.DataContext = station;
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
