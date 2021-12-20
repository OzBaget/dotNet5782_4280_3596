using IBL.BO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBL.IBL db = new BL.BL();

        public Customer Customer { get; }
        public Permissions Permission { get; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Customer customer, Permissions permission)
        {
            Customer = customer;
            Permission = permission;
            if (permission == Permissions.Administrator)
                AdministatorWindow();
            else
                ClientWindow();
        }

        /// <summary>
        /// Button to drone list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdministatorWindow()
        {

        }
        private void ClientWindow()
        {

        }
        private void openDroneList(object sender, RoutedEventArgs e)
        {
            new ViewDroneList(db).Show();
        }
        /// <summary>
        /// Play gif agian
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
       
        private void GoToUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
