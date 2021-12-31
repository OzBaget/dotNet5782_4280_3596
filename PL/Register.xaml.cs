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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            NameErrorBox.Text = "";
            PhoneErrorBox.Text = "";
            IdErrorBox.Text = "";
            LatErrorBox.Text = "";
            LongErrorBox.Text = "";
            int id,phone;
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
            if (PhoneBox.Text == "")
            {
                PhoneErrorBox.Text = "Write a name, try again";
                error = true;
            }
            else if (!int.TryParse(PhoneBox.Text, out phone))
            {
                PhoneErrorBox.Text = "Only digits, try again";
                error = true;
            }
            if (error)
                return;

            BO.Customer cust = new BO.Customer();
            cust.Name = NameBox.Text;
            cust.Id = int.Parse(IdBox.Text);
            cust.Phone = PhoneBox.Text;
            cust.Location = new();
            cust.Location.Latitude = double.Parse(LatBox.Text);
            cust.Location.Longitude= double.Parse(LongBox.Text);
            
            BlApi.BlFactory.GetBl().AddCustomer(cust);  
            this.Close();
        }
    }
}
