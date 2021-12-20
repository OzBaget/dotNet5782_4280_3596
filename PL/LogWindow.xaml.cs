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
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
        }
        IBL.IBL db;
        private void Register(object sender, RoutedEventArgs e)
        {
            int a;
        }

        private void logIn(object sender, RoutedEventArgs e)
        {
            int id;
            if (!int.TryParse(logInBox.Text, out id))
                return;
            if (db.GetCustomer(id).permission == IBL.BO.Permissions.Client)
                new ViewParcelList();

            else
                new MainWindow();

        }
    }
}