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
    /// Interaction logic for customers_ordering.xaml
    /// </summary>
    public partial class customers_ordering : Window
    {
        public customers_ordering()
        {
            InitializeComponent();
            if(Title.Text== "Order new food")
            {
                listOfTypesCombo.ItemsSource = FooDType.Foodtype.Values;
            }
            else
            {
                listOfTypesCombo.IsEnabled = false;
                FilterTypeCheck.IsEnabled = false;
            }
        }
        private void ChangeOrderNumber_Click(object sender, RoutedEventArgs e)//**
        {
            try
            {
                if (DateList.SelectedDate != null)
                {
                    if (Title.Text == "Order new food")// for add to list.
                    {
                        if ("" != listOfFoodCombo.Text && "Names" != listOfFoodCombo.Text)
                        {
                            var listOfFood = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                            if (listOfFood.Count > 0)
                            {
                                var foo = listOfFood.Where(x => x.Name == listOfFoodCombo.Text).First();
                                Customers.CurrentCusomer.AddFood(Food.findFoodByName(listOfFoodCombo.Text), int.Parse(NumberOfFood.Text), DateList.SelectedDate ?? default);
                                foo.RemainingNumber -= int.Parse(NumberOfFood.Text);
                                Manager.restaurant.AddFoodReserved(Food.findFoodByName(listOfFoodCombo.Text), int.Parse(NumberOfFood.Text), DateList.SelectedDate ?? default);
                            }
                        }
                        else
                        {
                            throw new Exception("Please select a food.");
                        }
                    }
                    else// for deleting from list.
                    {
                        if ("" != listOfFoodCombo.Text && "Names" != listOfFoodCombo.Text)
                        {
                            var listOfFood = Customers.CurrentCusomer.OrderedFood.Where(x => x.Date == DateList.SelectedDate).ToList();
                            var listOfFoodres = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                            if (listOfFood.Count > 0)
                            {
                                var food = listOfFoodres.Where(x => x.Name == listOfFoodCombo.Text).First();
                                food.RemainingNumber += int.Parse(NumberOfFood.Text);
                                var foo = listOfFood.Where(x => x.food.Name == listOfFoodCombo.Text).First();
                                foo.ChangeFoodNum(-1 * int.Parse(NumberOfFood.Text));
                                var temp = Restaurant.ReservedOrder.Where(x => x.food.Name == listOfFoodCombo.Text && x.Date == DateList.SelectedDate).First();
                                temp.ChangeFoodNum(-1 * int.Parse(NumberOfFood.Text));
                            }
                        }
                        else
                        {
                            throw new Exception("Please select a food.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a date.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "! Alart !");
            }                
        }

        private void FilterTypeCheck_Checked(object sender, RoutedEventArgs e)
        {
            listOfTypesCombo.IsEnabled = true;
        }

        private void FilterTypeCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            listOfTypesCombo.IsEnabled = false ;
            if (DateList.SelectedDate != null)
            {
                var source = Restaurant.ReadDateFood(DateList.SelectedDate ?? default).Select(x => x.Name);
                if (source.Count() > 0)
                {
                    listOfFoodCombo.ItemsSource = source;
                }               

            }
            else
            {
                MessageBox.Show("Please select a date.");
            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DateList.SelectedDate != null)
                {
                    if (Title.Text == "Order new food")// for add to list.
                    {
                        if ("" != listOfFoodCombo.Text && "Names" != listOfFoodCombo.Text)
                        {
                            var listOfFood = Restaurant.ReadDateFood(DateList.SelectedDate ?? default);
                            if (listOfFood.Count > 0)
                            {
                                var foo = listOfFood.Where(x => x.Name == listOfFoodCombo.Text).First();
                                int Max = foo.RemainingNumber;
                                if (Max > 0)
                                {
                                    if (int.Parse(NumberOfFood.Text) < Max)
                                    {
                                        NumberOfFood.Text = (int.Parse(NumberOfFood.Text) + 1).ToString();
                                    }
                                    else
                                    {
                                        throw new Exception("The Maximum limit of food is reached. No more availeble.");
                                    }
                                }
                                else if (Max == 0)
                                {
                                    throw new Exception("This food is finished for the selected date.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Please select a food.");
                        }
                    }
                    else// for deleting from list.
                    {
                        var source = Customers.CurrentCusomer.OrderedFood.Where(x => x.Date == DateList.SelectedDate).Select(y => y.food.Name).ToList();
                        if (source.Count() > 0)
                        {
                            if ("" != listOfFoodCombo.Text && "Names" != listOfFoodCombo.Text)
                            {
                                var foods = Customers.CurrentCusomer.OrderedFood.Where(x => x.food.Name == listOfFoodCombo.Text && x.Date == DateList.SelectedDate);
                                int Max = foods.First().FoodNumber;
                                if (Max > 0)
                                {
                                    if (int.Parse(NumberOfFood.Text) < Max)
                                    {
                                        NumberOfFood.Text = (int.Parse(NumberOfFood.Text) + 1).ToString();
                                    }
                                    else
                                    {
                                        throw new Exception("The Maximum limit of food is reached. No more availeble.");
                                    }
                                }
                                else if (Max == 0)
                                {
                                    throw new Exception("This food is not ordered.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Please select a food.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a date.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "! Alart !");
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

        private void DateList_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (DateTime.Today.CompareTo(DateList.SelectedDate) < 0)
                {
                    DateList.SelectedDate = DateTime.Today;
                    throw new Exception("Selected Date is no longer accessable.");
                }
                else
                {
                    if (DateList.SelectedDate != null)
                    {
                        if (Title.Text == "Order new food")// for add to list.
                        {
                            var source = Restaurant.ReadDateFood(DateList.SelectedDate ?? default).Select(x => x.Name);
                            if (source.Count() > 0)
                            {
                                listOfFoodCombo.ItemsSource = source;
                            }
                            else
                            {
                                MessageBox.Show($"No food for {DateList.SelectedDate} is added.");
                            }
                        }
                        else
                        {
                            var source = Customers.CurrentCusomer.OrderedFood.Where(x => x.Date == DateList.SelectedDate).Select(y => y.food.Name).ToList();
                            if (source.Count() > 0)
                            {
                                listOfFoodCombo.ItemsSource = source;
                            }
                            else
                            {
                                MessageBox.Show($"No food for {DateList.SelectedDate} is ordered.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a date.", "! Alart !");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "! Alart !");
            }            
        }
        private void listOfFoodCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NumberOfFood.Text = "0";
        }
        private void listOfTypesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DateList.SelectedDate != null)
                {
                    if (listOfTypesCombo.Text != null)
                    {
                        int n=  FooDType.FindKey(listOfTypesCombo.Text);
                        var source = Restaurant.ReadDateFood(DateList.SelectedDate ?? default).Where(y => y.FoodType == n).Select(x => x.Name);
                        if (source.Count() > 0)
                        {
                            listOfFoodCombo.ItemsSource = source;
                        }
                        else
                        {
                            MessageBox.Show($"No food with type of {listOfTypesCombo.Text} in Date {DateList.SelectedDate} is added.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a date.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "! Alart !");
            }
        }
    }
}
