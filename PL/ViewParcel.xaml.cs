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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ViewParcel.xaml
    /// </summary>
    public partial class ViewParcel : Window
    {
        IBL db = BlFactory.GetBl();
        public Parcel MyParcel { get; set; }
        public Customer MyCustomer { get; set; }
        private bool exit;
        /// <summary>
        /// View parcel window constractor
        /// </summary>
        /// <param name="parcel">the parcel to show</param>
        /// <param name="customer">custome who view call this window (null if admin mode)</param>
        public ViewParcel(Parcel parcel,Customer customer)
        {
            MyParcel = parcel;
            MyCustomer = customer;
            this.DataContext = this;
            InitializeComponent();
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            prioritySelector.ItemsSource= Enum.GetValues(typeof(Priorities));
            senderComboBox.ItemsSource = db.GetAllCustomers();
            reciverComboBox.ItemsSource = db.GetAllCustomers();
            senderComboBox.IsEnabled = false;
            reciverComboBox.IsEnabled = false;
            IdBox.IsReadOnly = true;
            weightSelector.IsEnabled = false;
            prioritySelector.IsEnabled = false;
            confBtn.Visibility = Visibility.Hidden;
            AddBtn.Content = "Delete parcel";
            AddBtn.Click -= AddBtn_Click;
            AddBtn.Click += DeleteBtn_Click;



            if (parcel.Drone.Id == 0)
            { 
                drnDlsBtn.Visibility = Visibility.Hidden;

            }
            if ((customer == null || customer.Id == parcel.Sender.Id) && parcel.DatePickup == null && parcel.DateScheduled != null)
            {
                confBtn.Content = "Confirm pick-up";
                confBtn.Click += confPickUpClk;
                confBtn.Visibility = Visibility.Visible;

            }
            if ((customer == null || customer.Id == parcel.Target.Id) && parcel.DateDeliverd == null && parcel.DatePickup != null)
            {
                confBtn.Content = "Confirm delivery";
                confBtn.Click += confDeliveryClk;
                confBtn.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Add parcel window constractor
        /// </summary>
        /// <param name="customer">custome who view call this window (null if admin mode)</param>
        public ViewParcel(Customer customer)
        {
            MyCustomer = customer;

            MyParcel = new();
            MyParcel.Sender = new();
            MyParcel.Target = new();
            MyParcel.Drone = new();


            this.DataContext = this;
            InitializeComponent();
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            prioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            senderComboBox.ItemsSource = db.GetAllCustomers();
            reciverComboBox.ItemsSource = db.GetAllCustomers();

            IdBox.IsEnabled = false;

            drnDlsBtn.Visibility = Visibility.Hidden;
            confBtn.Visibility = Visibility.Hidden;
            sndDtlsBtn.Visibility = Visibility.Hidden;
            recvDtlsBtn.Visibility = Visibility.Hidden;


            senderChange(null, null);//updates errorsBox


            if (customer != null) //customer Mode
            {
                senderComboBox.SelectedValue = customer.Id;
                MyParcel.Sender.Id = customer.Id;
                senderComboBox.IsEnabled = false;
            }
        }   

        private void senderDetails_clk(object sender, RoutedEventArgs e)
        {
            new ViewCustomer(db.GetCustomer(MyParcel.Sender.Id)).ShowDialog();
            MyCustomer = db.GetCustomer(MyCustomer.Id);
            this.DataContext = this;


        }

        private void reciverDetails_clk(object sender, RoutedEventArgs e)
        {
            new ViewCustomer(db.GetCustomer(MyParcel.Target.Id)).ShowDialog();
            MyCustomer = db.GetCustomer(MyCustomer.Id);
            this.DataContext = this;
        }

        private void droneDetails_clk(object sender, RoutedEventArgs e)
        {
            if (MyParcel.Drone.Id !=0 )
            {
                new ViewDrone(db.GetDrone(MyParcel.Drone.Id)).ShowDialog();

            }
            else
            {
                MessageBox.Show("Parcel doesn't link to any drone.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void confPickUpClk(object sender, RoutedEventArgs e)
        {
            db.PickParcel(MyParcel.Drone.Id);
            MyParcel = db.GetParcel(MyParcel.Id.Value);
            this.DataContext = null;
            this.DataContext = this;

            if (MyCustomer == null)//admin
            {
                confBtn.Click -= confPickUpClk;
                confBtn.Click += confDeliveryClk;
                confBtn.Content = "Confirm delivery";
            }
            else
                confBtn.Visibility = Visibility.Hidden;
        }


        private void confDeliveryClk(object sender, RoutedEventArgs e)
        {
            try
            {
                db.ParcelToCustomer(MyParcel.Drone.Id);
                MyParcel = db.GetParcel(MyParcel.Id.Value);
                this.DataContext = null;
                this.DataContext = this;
                MessageBox.Show("The parcel was deliverd successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                confBtn.Visibility = Visibility.Hidden;
                MyParcel = db.GetParcel(MyParcel.Id.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Can't add parcel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            MyParcel.DateCreated = DateTime.Now;

            try
            {
                db.AddParcel(MyParcel);
                MessageBox.Show("The parcel was added successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                exit = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Can't add parcel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.DeleteParcel(MyParcel.Id.Value);
                MessageBox.Show("The parcel was deleted successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                exit = true;
                Close();
                this.DataContext = this;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Can't delete parcel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void senderChange(object sender, SelectionChangedEventArgs e)
        {
            if (senderComboBox.SelectedItem == null || reciverComboBox.SelectedItem==null)
            {
                senderErrorBox.Text = senderComboBox.SelectedItem == null ? "Error: Choose sender!" : "";
                reciverErrorBox.Text = reciverComboBox.SelectedItem == null ? "Error: Choose sender!" : "";
            }
            else if (senderComboBox.SelectedValue.Equals(reciverComboBox.SelectedValue))
            {
                reciverErrorBox.Text = "Error: Reciver must by diffrent from sender!";
                senderErrorBox.Text = "Error:  Sender must by diffrent from reciver!";
            }
            else
            {
                senderErrorBox.Text = "";
                reciverErrorBox.Text = "";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {         
            e.Cancel = !exit;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }
    }
}
