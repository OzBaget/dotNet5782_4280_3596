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
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        static IBL db;
        public LogWindow()
        {
            InitializeComponent();
            db = BlFactory.GetBl();
        } 
        private void Register(object sender, RoutedEventArgs e)
        {
            int a;
        }

        private void logIn(object sender, RoutedEventArgs e)
        {
            int id;
            if (!int.TryParse(logInBox.Text, out id))
            {
                errorBox.Text = "Id not valid, try again";
                return;
            }
            try
            {
                new ViewParcelList(db.GetCustomer(id)).Show();
                errorBox.Text = "";
            }
            catch (IdNotFoundException)
            {
                errorBox.Text = "ID not found in the system, try again";
                return;
            }
            

            
        }

        private void loginAdmin(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }
    }
}
