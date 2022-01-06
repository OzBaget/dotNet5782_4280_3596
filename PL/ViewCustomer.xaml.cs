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

namespace PL
{
    /// <summary>
    /// Interaction logic for ViewCustomer.xaml
    /// </summary>
    public partial class ViewCustomer : Window
    {
        /// <summary>
        /// Interaction logic for ViewStation.xaml
        /// </summary>
        List<ParcelInCustomer> midList;
        Customer customer;
        IBL db;
        bool changed;
        public ViewCustomer(Customer Cstation)
        {
            InitializeComponent();
            customer = Cstation;
            db = BlFactory.GetBl();
            showCustomer();
        }
        public ViewCustomer()
        {
            InitializeComponent();
            db = BlFactory.GetBl();
            GoAddView();
        }
        private void showCustomer()
        {
            ViewACustomer(null, null);
            NameBox.Text = customer.Name;
            PhoneBox.Text = customer.Phone;
            IdBox.Text = customer.Id.ToString();
            LocationBox.Text = customer.Location.ToString();
        }

        private void IsChanged(object sender, TextChangedEventArgs e)
        {

            if (customer == null)
                return;
            if (NameBox.Text == customer.Name && PhoneBox.Text == customer.Phone)
            {
                changed = false;
            }
            else
            {
                changed = true;
            }
            if(changed)
                Update.IsEnabled = true;
            else
                Update.IsEnabled = false;
        

        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            db.UpdateCustomer(int.Parse(IdBox.Text), NameBox.Text, PhoneBox.Text);
            MessageBox.Show("Update succeed", "Update customer", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AddCustomerToDb(object sender, RoutedEventArgs e)
        {
            NameErrorBox.Text = "";
            IdErrorBox.Text = "";
            LatErrorBox.Text = "";
            LongErrorBox.Text = "";
            PhoneErrorBox.Text = "";
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
            if (PhoneBox.Text == "")
            {
                PhoneErrorBox.Text = "Write digits, try again";
                error = true;
            }
            else if (!int.TryParse(PhoneBox.Text, out id))
            {
                PhoneErrorBox.Text = "Id not valid, try again";
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

            BO.Customer tmpCustomer = new BO.Customer();
            tmpCustomer.Name = NameBox.Text;
            tmpCustomer.Id = int.Parse(IdBox.Text);
            tmpCustomer.Phone = PhoneBox.Text;
            tmpCustomer.Location = new();
            tmpCustomer.Location.Latitude = double.Parse(LatBox.Text);
            tmpCustomer.Location.Longitude = double.Parse(LongBox.Text);

            try
            {
                BlApi.BlFactory.GetBl().AddCustomer(tmpCustomer);
                Exit_Click(sender, e);
            }
            catch (BlApi.IdAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message, "Can't add customer", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void ViewACustomer(object sender, RoutedEventArgs e)
        {
            IdBox.IsReadOnly = true;
            LocationBox.Visibility = Visibility.Visible;
            LocationLable.Visibility = Visibility.Visible;
            ParcelList.Visibility = Visibility.Visible;
            ParcelLable.Visibility = Visibility.Visible;
            LatBox.Visibility = Visibility.Collapsed;
            LongBox.Visibility = Visibility.Collapsed;
            LatLable.Visibility = Visibility.Collapsed;
            LongLable.Visibility = Visibility.Collapsed;
            
            NameBox.Text = customer.Name;
            IdBox.Text = customer.Id.ToString();
            LocationBox.Text = customer.Location.ToString();
            PhoneBox.TextChanged += IsChanged;
            NameBox.TextChanged += IsChanged;
            IntilaizeList();


        }
        private void GoAddView()
        {
            initializationBoxes();
            IdBox.IsReadOnly = false;
            LocationBox.Visibility = Visibility.Collapsed;
            LocationLable.Visibility = Visibility.Collapsed;
            ParcelList.Visibility = Visibility.Collapsed;
            ParcelLable.Visibility = Visibility.Collapsed;
            LatBox.Visibility = Visibility.Visible;
            LongBox.Visibility = Visibility.Visible;
            LatLable.Visibility = Visibility.Visible;
            LongLable.Visibility = Visibility.Visible;
            
            PhoneBox.TextChanged -= IsChanged;
            NameBox.TextChanged -= IsChanged;

        }
        private void initializationBoxes()
        {
            IdBox.Text = "";
            NameBox.Text = "";
            PhoneBox.Text = "";
            LocationBox.Text = "";
            LatBox.Text = "";
            LongBox.Text = "";
            NameErrorBox.Text = "";
            IdErrorBox.Text = "";
            LatErrorBox.Text = "";
            LongErrorBox.Text = "";
        }

        private void IntilaizeList()
        {
            midList = customer.ReceivedParcels;
            midList.AddRange(customer.SentParcels);
            ParcelList.ItemsSource = midList;
        }


        private void ParcelList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ViewParcel(db.GetParcel(((sender as ListView).SelectedItem as BO.ParcelInCustomer).Id), customer).ShowDialog();
        }

    }
}
