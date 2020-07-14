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
    /// Interaction logic for Restaurant_info_change.xaml
    /// </summary>
    public partial class Restaurant_info_change : Window
    {
        public Restaurant_info_change()
        {
            InitializeComponent();
        }

        private void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Restaurant.IsStablished == false)
                {
                    if (RestaurantNameBox.Text == "" || ResAddressBox.Text == "" || ResRegionBox.Text == "" || ResPhonenumberBox.Text == "") 
                    Manager.restaurant = new Restaurant(RestaurantNameBox.Text, ResAddressBox.Text, ResRegionBox.Text, ResPhonenumberBox.Text);
                    MessageBox.Show("restaurant stablisted");
                }
                else
                {
                    string Name;
                    if (ResNameCheck.IsChecked == true)
                    {
                        if (RestaurantNameBox.Text == "")
                            throw new Exception("please fill the blank.");
                       Name =  RestaurantNameBox.Text;
                    }
                    else
                    {                       
                        Name = Manager.restaurant.Name;
                    }
                    string address;
                    if (ResAddressCheck.IsChecked == true)
                    {
                        if (ResAddressBox.Text == "")
                            throw new Exception("please fill the blank.");

                        address = ResAddressBox.Text;
                    }
                    else
                    {

                        address = Manager.restaurant.Address;
                    }
                    string region;
                    if (ResRegionCheck.IsChecked == true)
                    {
                        if (ResRegionBox.Text == "")
                            throw new Exception("please fill the blank.");

                        region = ResRegionBox.Text;
                    }
                    else
                    {
                        region = Manager.restaurant.Region;
                    }
                    string Phone;
                    if (ResNumberCheck.IsChecked == true)
                    {
                        if (ResPhonenumberBox.Text == "")
                            throw new Exception("please fill the blank.");
                        Phone = ResPhonenumberBox.Text;
                    }
                    else
                    {
                        Phone = Manager.restaurant.PhoneNum;
                    }
                    Manager.restaurant = new Restaurant(Name,address,region, Phone);                    
                }
                Managment manager_Login = new Managment();
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
