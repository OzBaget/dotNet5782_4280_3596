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

        

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            NameErrorBox.Text = "";
            PhoneErrorBox.Text = "";
            IdErrorBox.Text = "";
            int id,phone;
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
            BlApi.BlFactory.GetBl().AddCustomer(cust);  
            this.Close();
        }
    }
}
