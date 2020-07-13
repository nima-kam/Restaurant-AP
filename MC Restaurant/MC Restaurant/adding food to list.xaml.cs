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
                if (Title.Text == "Manager edit")
                {
                    var menu = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                    if (listOfFood.Text != "" && menu != null)
                    {
                        var te = menu.Where(x => x.Name == listOfFood.Text).First();
                        NumberOfFood.Text = $"{te.RemainingNumber}";
                    }
                    else
                    {
                        NumberOfFood.Text = 0.ToString();
                    }

                }
               

            }
        }
        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "manager add" )
            {
                if (listOfFood.Text != "" && DateList.SelectedDate != null)
                {
                    NumberOfFood.Text = (int.Parse(NumberOfFood.Text) + 1).ToString();
                }
                else
                {
                   MessageBox.Show("Please select date and food first.");
                }
            }
            else
            {
                if (listOfFood.Text != "" && DateList.SelectedDate != null)
                {
                    var menu = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                    var te = menu.Where(x => x.Name == listOfFood.Text).First();
                    if (NumberOfFood.Text == te.RemainingNumber.ToString()) 
                    {
                        MessageBox.Show($"The max number of the specified date is {NumberOfFood.Text} and no more food can be deleted.");
                    }
                }
            }
        }
        private void mines_Click(object sender, RoutedEventArgs e)
        {
            if (NumberOfFood.Text == "1")
            {
                mines.IsEnabled = false;
                NumberOfFood.Text = "0";
            }
            else
            {
                NumberOfFood.Text = (int.Parse(NumberOfFood.Text) - 1).ToString();
            }
        }

        private void changeFoodNumber_Click(object sender, RoutedEventArgs e)
        {
            if (listOfFood.Text != "" && DateList.SelectedDate != null)
            {
                if (Title.Text == "manager add")
                {

                    var menu = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                    var te = menu.Where(x => x.Name == listOfFood.Text).First();
                    
                    Restaurant.AddFood(Food.findFoodByName(listOfFood.Text, int.Parse(NumberOfFood.Text)-te.RemainingNumber), DateList.SelectedDate ?? default);
                    MessageBox.Show($"{listOfFood.Text} for {NumberOfFood} number added to Date {DateList.SelectedDate}. ");

                }
                else//for the deleting numbers of food***.
                {
                    var menu = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                    if (menu.Any(x => x.Name == listOfFood.Text))
                    {
                        var te = menu.Where(x => x.Name == listOfFood.Text).First();
                        te.RemainingNumber = te.RemainingNumber - int.Parse(NumberOfFood.Text);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select date and food first.");
            }
        }
    }
}
