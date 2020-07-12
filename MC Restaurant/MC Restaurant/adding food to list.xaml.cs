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
    /// Interaction logic for adding_food_to_list.xaml
    /// </summary>
    public partial class adding_food_to_list : Window
    {
        static List<Food> sourceOfFood;
        public adding_food_to_list()
        {
            InitializeComponent();

        }

        private void listOfFood_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateList.SelectedDate == null)
            {
                MessageBox.Show("Please select a date first.");
            }
            else
            {
                var menu = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                if (listOfFood.Text != "")
                {
                    var te = menu.Where(x => x.Name == listOfFood.Text).First();
                    NumberOfFood.Text = $"{te.RemainingNumber}";
                }

            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mines_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
