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
            IDTextBox.IsReadOnly = true;
            weightSelector.IsEnabled = false;
            prioritySelector.IsEnabled = false;
            confBtn.Visibility = Visibility.Hidden;
            AddBtn.Visibility = Visibility.Collapsed;

            if (parcel.Drone.Id==0)
                drnDlsBtn.Visibility = Visibility.Hidden;
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

            IDTextBox.IsEnabled = false;

            drnDlsBtn.Visibility = Visibility.Hidden;
            confBtn.Visibility = Visibility.Hidden;
            sndDtlsBtn.Visibility = Visibility.Hidden;
            recvDtlsBtn.Visibility = Visibility.Hidden;


            if (customer != null) //customer Mode
            {
                senderComboBox.SelectedValue = customer.Id;
                MyParcel.Sender.Id = customer.Id;
                senderComboBox.IsEnabled = false;
            }
        }   

        private void senderDetails_clk(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(MyParcel.Sender.ToString(), "Sender ditalis", MessageBoxButton.OK);
        }

        private void reciverDetails_clk(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(MyParcel.Target.ToString(), "Target ditalis", MessageBoxButton.OK);
        }

        private void droneDetails_clk(object sender, RoutedEventArgs e)
        {
            if (MyParcel.Drone.Id !=0 )
            {

                MessageBox.Show(MyParcel.Drone.ToString(), "Drone ditalis", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Parcel is not linked to drone", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void confPickUpClk(object sender, RoutedEventArgs e)
        {
            db.PickParcel(MyParcel.Drone.Id);
            MyParcel = null;
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
            db.ParcelToCustomer(MyParcel.Drone.Id);
            confBtn.Visibility = Visibility.Hidden;
            MyParcel = db.GetParcel(MyParcel.Id.Value);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            MyParcel.DateCreated = DateTime.Now;

            try
            {
                db.AddParcel(MyParcel);
                MessageBox.Show("The parcel was added successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                //exit = true;
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Can't add parcel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void senderChange(object sender, SelectionChangedEventArgs e)
        {
            if (senderComboBox.SelectedValue == null)
                senderErrorBox.Text = "Error: Choose sender!";
            else senderErrorBox.Text = "";
        }

        private void reciverChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reciverComboBox.SelectedItem == null)
                reciverComboBox.Text = "Error: Choose reciver!";
            else if (reciverComboBox.SelectedItem == senderComboBox.SelectedItem)
                reciverComboBox.Text = "Error: Reciver must by diffrent from sender!";
            else reciverComboBox.Text = "";

        }
    }
}
