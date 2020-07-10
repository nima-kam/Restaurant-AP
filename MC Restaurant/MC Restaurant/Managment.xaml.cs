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
    /// Interaction logic for Managment.xaml
    /// </summary>
    public partial class Managment : Window
    {
        public Managment()
        {
            InitializeComponent();
            TypefoodCombo.ItemsSource = FooDType.Foodtype.Values;
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowDateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
