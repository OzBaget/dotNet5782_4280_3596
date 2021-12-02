﻿using System;
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
    /// Interaction logic for ViewDroneList.xaml
    /// </summary>
    public partial class ViewDroneList : Window
    {
        private IBL.IBL db;
        public ViewDroneList()
        {
            InitializeComponent();
        }
        public ViewDroneList(IBL.IBL database)
        {
            InitializeComponent();
            db = database;
            ListViewDrones.ItemsSource = db.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //StatusSelector.SelectedItem
        }

        private void ListViewDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var a=(ListView)sender ;
            
            Console.WriteLine(sender);
            InitializeComponent();

        }
    }

}
