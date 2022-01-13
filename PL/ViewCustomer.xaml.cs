using BlApi;
using BO;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


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
        private List<ParcelInCustomer> midList;
        private bool exit = false;

        public Customer customer { get; set; }

        private IBL db;
        public ViewCustomer(Customer Cstation)
        {
            customer = Cstation;
            DataContext = customer;
            InitializeComponent();
            db = BlFactory.GetBl();
            ViewACustomer(null, null);
        }
        public ViewCustomer()
        {
            customer = new();
            customer.Location = new();
            DataContext = customer;
            InitializeComponent();
            db = BlFactory.GetBl();
            GoAddView();
        }


        private void IsChanged(object sender, TextChangedEventArgs e)
        {

            if (customer == null)
                return;
            if (NameBox.Text == customer.Name && PhoneBox.Text == customer.Phone)
            {
                UpdateButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                UpdateButton.Visibility = Visibility.Visible;
            }
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            db.UpdateCustomer(int.Parse(IdBox.Text), NameBox.Text, PhoneBox.Text);
            MessageBox.Show("Update succeed", "Update customer", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
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


            try
            {
                BlApi.BlFactory.GetBl().AddCustomer(customer);
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
            RecivevdParcelList.Visibility = Visibility.Visible;
            SendParcelList.Visibility = Visibility.Visible;
            ParcelLable.Visibility = Visibility.Visible;
            ExitButton.Visibility = Visibility.Visible;

            LatBox.Visibility = Visibility.Collapsed;
            LongBox.Visibility = Visibility.Collapsed;
            LatLable.Visibility = Visibility.Collapsed;
            LongLable.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            AddButton.Visibility = Visibility.Collapsed;
            AddImage.Visibility = Visibility.Collapsed;




        }
        private void GoAddView()
        {
            IdBox.IsReadOnly = false;
            LocationBox.Visibility = Visibility.Collapsed;
            LocationLable.Visibility = Visibility.Collapsed;
            RecivevdParcelList.Visibility = Visibility.Collapsed;
            SendParcelList.Visibility = Visibility.Collapsed;
            ParcelLable.Visibility = Visibility.Collapsed;
            LatBox.Visibility = Visibility.Visible;
            LongBox.Visibility = Visibility.Visible;
            LatLable.Visibility = Visibility.Visible;
            LongLable.Visibility = Visibility.Visible;



            AddImage.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Visible;

            UpdateButton.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Collapsed;
        }

        private void ParcelList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (db.GetParcel(((sender as ListView).SelectedItem as BO.ParcelInCustomer).Id) == null)
                return;
            new ViewParcel(db.GetParcel(((sender as ListView).SelectedItem as BO.ParcelInCustomer).Id), customer).ShowDialog();

            customer = db.GetCustomer(customer.Id);
            DataContext = customer;


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
