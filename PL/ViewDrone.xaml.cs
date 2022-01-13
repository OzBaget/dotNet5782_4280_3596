using BlApi;
using BO;
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
using System.Media;
using System.ComponentModel;
using System.Media;


namespace PL
{
    /// <summary>
    /// Interaction logic for ViewDrone.xaml
    /// </summary>
    public partial class ViewDrone : Window
    {
        IBL db;
        public Drone Cdrone { get; set; }
        bool exit = false;
        BackgroundWorker work;


        /// <summary>
        /// Add new drone menu
        /// </summary>
        /// <param name="db">dataBase to add too</param>
        public ViewDrone()
        {
            Cdrone = new();
            Cdrone.CurrentLocation = new();
            this.DataContext = Cdrone;
            InitializeComponent();
            this.db = BlFactory.GetBl();            
            AddMaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            AddMaxWeight.SelectedIndex = 0;
            work = new();

            StationCombo.ItemsSource = db.GetStationsWithFreeSlots();
            StationCombo.SelectedIndex = 0;

            LocationBox.IsEnabled = false;
            BatteryBox.IsEnabled = false;
            AddMaxWeight.IsEnabled = true;

            StationLable.Visibility = Visibility.Visible;
            StationCombo.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// View drone menu
        /// </summary>
        /// <param name="drone">the drone to view</param>
        /// <param name="db"></param>
        public ViewDrone(Drone drone)
        {
            this.db = BlFactory.GetBl();
            Cdrone = drone;
            this.DataContext = Cdrone;
            InitializeComponent();
            work = new();
            work.WorkerReportsProgress = true;
            work.WorkerSupportsCancellation = true;
            work.RunWorkerCompleted += SimulatorComplete;
            work.DoWork += startSimulator;
            work.ProgressChanged += UpdateView;

             AddMaxWeight.IsEnabled = false;

            StatusCombo.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            AddMaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            StationLable.Visibility = Visibility.Collapsed;
            StationCombo.Visibility = Visibility.Collapsed;
            AddButton.Visibility = Visibility.Collapsed;
            AddImage.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;

            ChargeButton.Visibility = Visibility.Collapsed;
            ReleaseButton.Visibility = Visibility.Collapsed;
            PickUpButton.Visibility = Visibility.Collapsed;
            LinkButton.Visibility = Visibility.Collapsed;
            DeliverButton.Visibility = Visibility.Collapsed;

            switch (Cdrone.Status)
            {
                case DroneStatus.Available:
                    ChargeButton.Visibility = Visibility.Visible;
                    ReleaseButton.Visibility = Visibility.Hidden;
                    PickUpButton.Visibility = Visibility.Hidden;
                    LinkButton.Visibility = Visibility.Visible;
                    DeliverButton.Visibility = Visibility.Hidden;
                    break;
                case DroneStatus.UnderMaintenance:
                    ChargeButton.Visibility = Visibility.Hidden;
                    ReleaseButton.Visibility = Visibility.Visible;
                    PickUpButton.Visibility = Visibility.Hidden;
                    LinkButton.Visibility = Visibility.Hidden;
                    DeliverButton.Visibility = Visibility.Hidden;
                    break;
                case DroneStatus.Delivery:
                    if (Cdrone.Parcel.IsInTransfer)
                    {
                        ChargeButton.Visibility = Visibility.Hidden;
                        ReleaseButton.Visibility = Visibility.Hidden;
                        PickUpButton.Visibility = Visibility.Hidden;
                        LinkButton.Visibility = Visibility.Hidden;
                        DeliverButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ChargeButton.Visibility = Visibility.Hidden;
                        ReleaseButton.Visibility = Visibility.Hidden;
                        PickUpButton.Visibility = Visibility.Visible;
                        LinkButton.Visibility = Visibility.Hidden;
                        DeliverButton.Visibility = Visibility.Hidden;
                    }
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// update all textboxs to the currnt status of Cdrone
        /// </summary>
        
        
        /// <summary>
        /// Close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }



        /// <summary>
        /// Show parcel detaitls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PacelDetaitls_Click(object sender, RoutedEventArgs e)
        {
            if (Cdrone.Parcel.Id != 0) 
            {
                //TODO: show parcel ditalis
                MessageBox.Show(Cdrone.Parcel.ToString(), "Parcel ditalis", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Drone is not linked to parcel", "Error!",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

                
       
        /// <summary>
        /// Add new Drone to db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Drone myDrone = new();
            int id;
            if (!int.TryParse(IdBox.Text, out id) || id < 1)
            {
                MessageBox.Show("Drone ID is not vaild!", "Can't add dorne", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ModelBox.Text=="")
            {
                MessageBox.Show("Model is not valid!", "Can't add dorne", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                db.AddDrone(Cdrone, (StationCombo.SelectedItem as BaseStationToList).Id);
                MessageBox.Show("The drone was added successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                exit = true;
                Close();
            }
            catch (BlApi.IdAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message, "Can't add dorne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BlApi.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't add dorne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// update drone model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                db.UpdateDrone(Cdrone.Id, ModelBox.Text);
                MessageBox.Show("The drone was updated successfully!", "success!", MessageBoxButton.OK, MessageBoxImage.Information);
                updateButton.Visibility = Visibility.Hidden;

            }
            catch (BlApi.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't update dorne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
  
        /// <summary>
        /// Show confirm button if the model changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModelChanged(object sender, TextChangedEventArgs e)
        {
            if (Cdrone == null)
                return;
            if (updateButton == null)
                return;
            if (ModelBox.Text != Cdrone.Model)
            {
                updateButton.Visibility = Visibility.Visible;
            }
            else
            {
                updateButton.Visibility = Visibility.Hidden;
            }
        }

        private void Deliver_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                db.ParcelToCustomer(Cdrone.Id);
                MessageBox.Show("The parcel deliverd successfully!", "success!", MessageBoxButton.OK, MessageBoxImage.Information);
                DeliverButton.Visibility = Visibility.Hidden;
                LinkButton.Visibility = Visibility.Visible;
                ChargeButton.Visibility = Visibility.Visible;
                Cdrone = db.GetDrone(Cdrone.Id);
                this.DataContext = Cdrone;

            }
            catch (BlApi.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't deliver parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BlApi.CantDeliverParcelException ex)
            {
                MessageBox.Show(ex.Message, "Can't deliver parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Deliver parcel to customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChargeDrone_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int stationId = db.DroneToStation(Cdrone.Id);
                MessageBox.Show($"The drone sent to charge at station #{stationId} successfully!", "success!", MessageBoxButton.OK, MessageBoxImage.Information);
                ChargeButton.Visibility = Visibility.Hidden;
                ReleaseButton.Visibility = Visibility.Visible;
                LinkButton.Visibility = Visibility.Hidden;
                Cdrone = db.GetDrone(Cdrone.Id);
                this.DataContext = Cdrone;


            }
            catch (BlApi.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't send drone to charge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BlApi.CantSendDroneToChargeException ex)
            {
                MessageBox.Show(ex.Message, "Can't send drone to charge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Release drone from charging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Release_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                double newBattery = db.FreeDrone(Cdrone.Id);
                MessageBox.Show("The drone released successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                ReleaseButton.Visibility = Visibility.Hidden;
                ChargeButton.Visibility = Visibility.Visible;
                LinkButton.Visibility = Visibility.Visible;
                Cdrone = db.GetDrone(Cdrone.Id);
                this.DataContext = Cdrone;

            }
            catch (BlApi.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't release dorne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BlApi.CantReleaseDroneFromChargeException ex)
            {
                MessageBox.Show(ex.Message, "Can't release dorne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// pick up parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickUp_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                db.PickParcel(Cdrone.Id);
                MessageBox.Show("The parcel picked-up successfully!", "success!", MessageBoxButton.OK, MessageBoxImage.Information);
                PickUpButton.Visibility = Visibility.Hidden;
                DeliverButton.Visibility = Visibility.Visible;
                Cdrone = db.GetDrone(Cdrone.Id);
                this.DataContext = Cdrone;

            }
            catch (BlApi.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't pickup parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BlApi.CantPickUpParcelException ex)
            {
                MessageBox.Show(ex.Message, "Can't pickup parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// link drone to parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Link_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                db.linkParcel(Cdrone.Id);
                MessageBox.Show("The drone linked successfully!", "success!", MessageBoxButton.OK, MessageBoxImage.Information);
                LinkButton.Visibility = Visibility.Hidden;
                ChargeButton.Visibility = Visibility.Hidden;
                PickUpButton.Visibility = Visibility.Visible;
                Cdrone = db.GetDrone(Cdrone.Id);
                this.DataContext = Cdrone;

            }
            catch (BlApi.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't link parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BlApi.CantLinkParcelException ex)
            {
                MessageBox.Show(ex.Message, "Can't link parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// verify model is okey, if not-set the background 100
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyModel(object sender, TextChangedEventArgs e)
        {
            if (AddModel.Text=="")
            {
                AddModel.Background = Brushes.Red;
            }
            else
            {
                AddModel.Background = Brushes.Transparent;
            }
        }

        /// <summary>
        /// verify ID is okey, if not-set the background 100
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void verifyID(object sender, TextChangedEventArgs e)
        {
            int id;
            if (!int.TryParse(AddID.Text, out id) || id < 1)
            {
                AddID.Background = Brushes.Red;
            }
            else
            {
                AddID.Background = Brushes.Transparent;
            }
        }


        /// <summary>
        /// Close the window only if the cencel button preased
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CencelClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (work.IsBusy)
            {
                exit = true;
                work.CancelAsync();
            }                                       
        }

        private void SimulatorButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!work.IsBusy)
                work.RunWorkerAsync();
        }
        private void SimulatorButton_UnChecked(object sender, RoutedEventArgs e)
        {
            work.CancelAsync();
        }
        private void startSimulator(object? sender, DoWorkEventArgs e)
        {
            db.StartSimulator(Cdrone.Id, () => work.ReportProgress(0), () => work.CancellationPending);
        }
        private void SimulatorComplete(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (exit)
                this.Close();
        }
        private void UpdateView(object? sender, ProgressChangedEventArgs e)
        {
            Cdrone = db.GetDrone(Cdrone.Id);
            this.DataContext = Cdrone;
        }
        private void Exit_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
 