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
        public ViewParcel(Parcel parcel,Customer customer)
        {
            MyParcel = parcel;
            MyCustomer = customer;
            this.DataContext = this;
            InitializeComponent();
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            prioritySelector.ItemsSource= Enum.GetValues(typeof(Priorities));
            IDTextBox.IsEnabled = false;
            weightSelector.IsEnabled = false;
            prioritySelector.IsEnabled = false;
            senderIdBox.IsEnabled = false;
            reciverIdBox.IsEnabled = false;
        }


        public ViewParcel(Customer customer)
        {
            MyCustomer = customer;
            this.DataContext = this;
            InitializeComponent();
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            prioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            
            if (customer != null) //customer Mode
            {
                senderIdBox.Text = customer.Id.ToString();
                senderIdBox.IsEnabled = false;
            }
        }

        private void senderDetails_clk(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MyParcel.Sender.ToString(), "Sender ditalis", MessageBoxButton.OK);
        }

        private void reciverDetails_clk(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MyParcel.Target.ToString(), "Target ditalis", MessageBoxButton.OK);
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
    }
}
