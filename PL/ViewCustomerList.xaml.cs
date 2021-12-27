﻿using BlApi;
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
    /// Interaction logic for ViewCustomerList.xaml
    /// </summary>
    public partial class ViewCustomerList : Window
    {
        IBL db = BlFactory.GetBl();
        public ViewCustomerList()
        {
            InitializeComponent();
            ResetList();
        }

        private void listViewStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ViewCustomer(db.GetCustomer(((sender as ListView).SelectedItem as BO.CustomerToList).Id)).ShowDialog();
            ResetList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new ViewCustomer    ().ShowDialog();
            ResetList();
        }

        private void ResetList()
        {
            listViewStations.ItemsSource = db.GetAllCustomers();
        }
    }
}
