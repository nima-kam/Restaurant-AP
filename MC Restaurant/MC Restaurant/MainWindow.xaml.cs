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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace MC_Restaurant
{




    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            customers_login customers_Login = new customers_login();
            this.Visibility = Visibility.Collapsed;
            customers_Login.Show();
            
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            manager_login manager_Login = new manager_login();
            this.Visibility = Visibility.Collapsed;
            manager_Login.Show();

        }
    }
}
