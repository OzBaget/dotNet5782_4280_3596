﻿using IBL.BO;
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
        IBL.IBL db;
        Drone Cdrone;
        
        /// <summary>
        /// Add new drone menu
        /// </summary>
        /// <param name="db">dataBase to add too</param>
        public ViewDrone(IBL.IBL db)
        {
            this.db = db;
            InitializeComponent();
            DroneViewGrid.Visibility = Visibility.Collapsed;
            AddDroneGrid.Visibility = Visibility.Visible;
            AddMaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            AddMaxWeight.SelectedIndex = 0;

            AddIdStation.ItemsSource = db.GetStationsWithFreeSlots();
            AddIdStation.SelectedIndex = 0;
            }

        public ViewDrone(Drone drone, IBL.IBL db)
        {
            this.db = db;
            Cdrone = drone;

            InitializeComponent();

            AddDroneGrid.Visibility = Visibility.Collapsed;            
            DroneViewGrid.Visibility = Visibility.Visible;
            updateTextBoxs();

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
                        PickUpButton.Visibility = Visibility.Hidden;
                        LinkButton.Visibility = Visibility.Visible;
                        DeliverButton.Visibility = Visibility.Hidden;
                    }
                    break;
                default:
                    break;
            }

        }

        private void updateTextBoxs()
        {
            Cdrone = db.GetDrone(Cdrone.Id);
            StatusBox.Text = Cdrone.Status.ToString();
            MaxWeighBox.Text = Cdrone.MaxWeight.ToString();
            BatteryBox.Text = Cdrone.Battery.ToString() + "%";
            
            IdBox.Text = Cdrone.Id.ToString();
            ModelBox.Text = Cdrone.Model;

            if (Cdrone.Parcel.Id != 0)
            {
                ParcelId.Text = "ID: " + Cdrone.Parcel.Id;
            }
            else
            {
                ParcelId.Text = "No parcel";
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
      

        
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

                
               
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Drone myDrone = new();
            int id;
            if (!int.TryParse(AddID.Text, out id))
            {
                MessageBox.Show("Drone ID is not vaild!", "Can't add dorne", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            myDrone.Id =id ;
            myDrone.Model = AddModel.Text;
            myDrone.MaxWeight = (WeightCategories)AddMaxWeight.SelectedItem;
            try
            {
                db.AddDrone(myDrone, (AddIdStation.SelectedItem as BaseStationToList).Id);
                MessageBox.Show("The drone was added successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (IBL.BL.IdAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message, "Can't add dorne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BL.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't add dorne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Update_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                db.UpdateDrone(Cdrone.Id, ModelBox.Text);
                MessageBox.Show("The drone was updated successfully!", "success!", MessageBoxButton.OK, MessageBoxImage.Information);
                updateTextBoxs();
                updateButton.Visibility = Visibility.Hidden;

            }
            catch (IBL.BL.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't update dorne", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

  
        private void ModelChanged(object sender, TextChangedEventArgs e)
        {
            if (Cdrone == null)
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
                updateTextBoxs();
                DeliverButton.Visibility = Visibility.Hidden;
                LinkButton.Visibility = Visibility.Visible;
                ChargeButton.Visibility = Visibility.Visible;
            }
            catch (IBL.BL.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't deliver parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BL.CantDeliverParcelException ex)
            {
                MessageBox.Show(ex.Message, "Can't deliver parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChargeDrone_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int stationId = db.DroneToStation(Cdrone.Id);
                MessageBox.Show($"The drone sent to charge at station #{stationId} successfully!", "success!", MessageBoxButton.OK, MessageBoxImage.Information);
                updateTextBoxs();
                ChargeButton.Visibility = Visibility.Hidden;
                ReleaseButton.Visibility = Visibility.Visible;
                LinkButton.Visibility = Visibility.Hidden;
            }
            catch (IBL.BL.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't send drone to charge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BL.CantSendDroneToChargeException ex)
            {
                MessageBox.Show(ex.Message, "Can't send drone to charge", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Release_Click(object sender, MouseButtonEventArgs e)
        {
            TimeSpan? result = ReleaseDroneDialog.GetResult();
            if (result != null)
            {
                try
                {
                    int newBattery = db.FreeDrone(Cdrone.Id, (TimeSpan)result);
                    MessageBox.Show("The drone released successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    updateTextBoxs();
                    ReleaseButton.Visibility = Visibility.Hidden;
                    ChargeButton.Visibility = Visibility.Visible;
                    LinkButton.Visibility = Visibility.Visible;
                }
                catch (IBL.BL.IdNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Can't release dorne", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (IBL.BL.CantReleaseDroneFromChargeException ex)
                {
                    MessageBox.Show(ex.Message, "Can't release dorne", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void PickUp_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                db.PickParcel(Cdrone.Id);
                MessageBox.Show("The parcel picked-up successfully!", "success!", MessageBoxButton.OK, MessageBoxImage.Information);
                updateTextBoxs();
                PickUpButton.Visibility = Visibility.Hidden;
                DeliverButton.Visibility = Visibility.Visible;
            }
            catch (IBL.BL.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't pickup parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BL.CantPickUpParcelException ex)
            {
                MessageBox.Show(ex.Message, "Can't pickup parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Link_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                db.linkParcel(Cdrone.Id);
                MessageBox.Show("The drone linked successfully!", "success!", MessageBoxButton.OK, MessageBoxImage.Information);
                updateTextBoxs();
                LinkButton.Visibility = Visibility.Hidden;
                ChargeButton.Visibility = Visibility.Hidden;
                PickUpButton.Visibility = Visibility.Visible;
            }
            catch (IBL.BL.IdNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Can't link parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IBL.BL.CantLinkParcelException ex)
            {
                MessageBox.Show(ex.Message, "Can't link parcel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
