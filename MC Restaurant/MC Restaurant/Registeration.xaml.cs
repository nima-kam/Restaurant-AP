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
    /// Interaction logic for Registeration.xaml
    /// </summary>
    public partial class Registeration : Window
    {
        public Registeration()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            PassMatchingBlock.Text = "";
            try
            {
                if (PasswordBox.Password == RepeatPasswordBox.Password)
                {
                    if (UserNameBox.Text != "" && AddressBox.Text != "" && RepeatPasswordBox.Password != "" && MobileNumBox.Text != "" && EmailBox.Text != "" && IDBox.Text != "" && PasswordBox.Password != "")
                    {
                        PassMatchingBlock.Text = "";
                        Customers.CurrentCusomer = new Customers(UserNameBox.Text, AddressBox.Text, MobileNumBox.Text, EmailBox.Text, IDBox.Text, PasswordBox.Password);
                        buy buy = new buy();
                        this.Visibility = Visibility.Collapsed;
                        buy.Show();
                    }
                    else
                    {
                        throw new Exception ("Please fill the empty boxes");
                    }
                }
                else
                {
                    throw new Exception ( "Password repeat is not match");
                }
            }
            catch(Exception ex)
            {
                PassMatchingBlock.Text += ex.Message+"\n";
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
