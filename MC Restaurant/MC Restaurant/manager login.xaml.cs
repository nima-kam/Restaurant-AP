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
    /// Interaction logic for manager_login.xaml
    /// </summary>
    public partial class manager_login : Window
    {
        public manager_login()
        {
            InitializeComponent();
        }
        private void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            if (Restaurant.IsStablished==true)
            {
                string Name = ManagerName.Text;
                string Pass = ManagerPassword.Password;
                try
                {
                    var manager = new Manager(Name, Pass);
                    Manager.logedInManager = manager;
                    Managment manager_Login = new Managment();
                    this.Visibility = Visibility.Collapsed;
                    manager_Login.Show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please establish the restaurant.");
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Collapsed;
            main.Show();
        }

        private void restaurant_Click(object sender, RoutedEventArgs e)
        {
            string Name = ManagerName.Text;
            string Pass = ManagerPassword.Password;
            try
            {
                var manager = new Manager(Name, Pass);
                Manager.logedInManager = manager;
                var manager_Login = new Restaurant_info_change();
                this.Visibility = Visibility.Collapsed;
                manager_Login.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
