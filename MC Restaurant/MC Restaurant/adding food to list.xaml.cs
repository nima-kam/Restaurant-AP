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
                else
                {
                    NumberOfFood.Text = "0";
                }
               

            }
        }
        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "Manager add" )
            {
                if (listOfFood.Text != "" && DateList.SelectedDate != null)
                {
                    NumberOfFood.Text = (int.Parse(NumberOfFood.Text) + 1).ToString();
                    if (int.Parse(NumberOfFood.Text) > 0)
                    {
                        mines.IsEnabled = true;
                    }
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
                    if (menu.Any(x => x.Name == listOfFood.Text))
                    {
                        var te = menu.Where(x => x.Name == listOfFood.Text).First();
                        if (NumberOfFood.Text == te.RemainingNumber.ToString())
                        {
                            MessageBox.Show($"The max number of the specified date is {NumberOfFood.Text} and no more food can be deleted.");
                        }
                        else
                        {
                            NumberOfFood.Text = (int.Parse(NumberOfFood.Text) + 1).ToString();
                        }
                        if (int.Parse(NumberOfFood.Text) > 0)
                        {
                            mines.IsEnabled = true;
                        }
                    }                    
                }
                else
                {
                    MessageBox.Show("Please select date and food first.");
                }
            }
        }
        private void mines_Click(object sender, RoutedEventArgs e)
        {
            if (NumberOfFood.Text == "1" || NumberOfFood.Text == "0") 
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
            try
            {
                if (listOfFood.Text != "" && DateList.SelectedDate != null)
                {
                    if (Title.Text == "Manager add")
                    {
                        var menu = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                        if (menu.Any(x => x.Name == listOfFood.Text))
                        {
                            var te = menu.Where(x => x.Name == listOfFood.Text).First();
                            Restaurant.AddFood(Food.findFoodByName(listOfFood.Text, int.Parse(NumberOfFood.Text) + te.RemainingNumber), DateList.SelectedDate ?? default);
                            Restaurant.SaveCalander();
                            MessageBox.Show($"{listOfFood.Text} for {NumberOfFood} number added to Date {DateList.SelectedDate}. ");

                            NumberOfFood.Text = "0";
                        }
                        else
                        {
                            Restaurant.AddFood(Food.findFoodByName(listOfFood.Text, int.Parse(NumberOfFood.Text)), DateList.SelectedDate ?? default);
                            Restaurant.SaveCalander();
                            MessageBox.Show($"{listOfFood.Text} for {NumberOfFood} number added to Date {DateList.SelectedDate}. ");

                            NumberOfFood.Text = "0";
                        }
                        
                    }
                    else//for the deleting numbers of food***.
                    {
                        var menu = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                        if (menu.Any(x => x.Name == listOfFood.Text))
                        {
                            var te = menu.Where(x => x.Name == listOfFood.Text).First();
                            te.RemainingNumber = te.RemainingNumber - int.Parse(NumberOfFood.Text);
                        }
                        NumberOfFood.Text = "0";
                    }
                }
                else
                {
                    MessageBox.Show("Please select date and food first.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
