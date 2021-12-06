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
        public MainWindow()
        {
            InitializeComponent();
        }
        private void openDroneList(object sender, RoutedEventArgs e)
        {
            new ViewDroneList(db).Show();
        }
        private void Again_Gif(object sender, RoutedEventArgs e)
        {
            GifDrones.Position = new TimeSpan(0, 0, 1);
            GifDrones.Play();
        }
    }
}
