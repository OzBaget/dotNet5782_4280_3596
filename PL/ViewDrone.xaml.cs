using IBL.BO;
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
    /// Interaction logic for ViewDrone.xaml
    /// </summary>
    public partial class ViewDrone : Window
    {
        Drone Cdrone;
        /*TextBox StatusBox = new TextBox();
        TextBox MaxWeighBox = new TextBox();
        TextBox BatteryBox = new TextBox();
        TextBox LatBox = new TextBox();
        TextBox LngBox = new TextBox();
        TextBox IdBox = new TextBox();
        TextBox ModelBox = new TextBox();*/
        public ViewDrone()
        {
            InitializeComponent();
            DroneViewGrid.Opacity = 0;
            AddDroneGrid.Opacity = 100;
        }

        public ViewDrone(Drone drone)
        {
            InitializeComponent();
            AddDroneGrid.Opacity = 0;
            DroneViewGrid.Opacity = 100;
            Cdrone = drone;
            StatusBox.Text =  Cdrone.Status.ToString();
            MaxWeighBox.Text =  Cdrone.MaxWeight.ToString();
            BatteryBox.Text =  Cdrone.Battery.ToString();
            LatBox.Text =  Cdrone.CurrentLocation.Latitude.ToString();
            LngBox.Text =  Cdrone.CurrentLocation.Longitude.ToString();
            IdBox.Text =  Cdrone.Id.ToString();
            ModelBox.Text =  Cdrone.Model;
                
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void textBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Bring_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Collect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PacelDetaitls_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Charge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Realse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
