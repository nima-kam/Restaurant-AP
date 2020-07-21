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

namespace MC_Restaurant
{
    /// <summary>
    /// Interaction logic for customers_login.xaml
    /// </summary>
    public partial class customers_login : Window
    {
        public customers_login()
        {
            InitializeComponent();
        }

        

        private void Registerbutton_Click(object sender, RoutedEventArgs e)
        {
            Registeration user = new Registeration();
            this.Visibility = Visibility.Collapsed;
            user.Show(); 
        }      

        private void Confirmbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Restaurant.IsStablished == true)
                {
                    Customers.CurrentCusomer = Customers.FindCustomers(userName.Text, CustomerPassword.Password);
                    MessageBox.Show($" {Customers.CurrentCusomer.FullName} \nWelcome to the {Manager.restaurant.Name} restaurant.");
                    buy buy = new buy();
                    this.Visibility = Visibility.Collapsed;
                    buy.Show();
                }
                else
                {
                    MessageBox.Show("This part will be coming soon.");
                }
               

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Collapsed;
            main.Show();
        }
    }
}
