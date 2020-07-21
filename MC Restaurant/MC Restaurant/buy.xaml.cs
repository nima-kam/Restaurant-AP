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
    /// Interaction logic for buy.xaml
    /// </summary>
    public partial class buy : Window
    {
        public buy()
        {
            InitializeComponent();
        }
        private void ShowItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllOrderCheck.IsChecked == false)
            {
                if (MenuDate.SelectedDate != null)
                {
                    if (DateTime.Today.CompareTo(MenuDate.SelectedDate) > 0)
                    {
                        MessageBox.Show("Selected date is no longer accessable.\nPlease select another one.");
                    }
                    else
                    {
                        ChangeFoodList.Items.Clear();
                        var DateFood = Customers.CurrentCusomer.OrderedFood.Where(x => x.Date == MenuDate.SelectedDate);
                        if (DateFood.Count() > 0)
                        {
                            int i = 0;
                            var dateText = new TextBlock();
                            var date = MenuDate.SelectedDate ?? default;
                            dateText.Text = $"Orders for {date.Year}-{date.Month}-{date.Day} are : ";
                            dateText.FontWeight = FontWeight.FromOpenTypeWeight(40);
                            dateText.FontSize = 14;
                            ChangeFoodList.Items.Add(dateText);

                            foreach (var item in DateFood)
                            {
                                ++i;
                                var str = new TextBlock();
                                str.FontWeight = FontWeight.FromOpenTypeWeight(40);
                                str.FontSize = 12;
                                str.Text = $"{i}. Name : {item.food.Name} \nAmount : {item.FoodNumber} \nPrice : {item.FoodNumber * item.food.FinalPrice }\n";
                                ChangeFoodList.Items.Add(str);
                            }
                        }
                        else
                        {
                            var dateText = new TextBlock();
                            var date = MenuDate.SelectedDate ?? default;
                            dateText.Text = $"No order for {date.Year}-{date.Month}-{date.Day} is available. ";
                            dateText.FontWeight = FontWeight.FromOpenTypeWeight(40);
                            dateText.FontSize = 14;
                            ChangeFoodList.Items.Add(dateText);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a date.");
                }
            }
            else
            {
                var allorder = Customers.CurrentCusomer.OrderedFood.OrderBy(x => x.Date).GroupBy(x => x.Date).ToList();
                if (allorder.Count() > 0)
                {
                    int j = 0;
                    foreach (var item in allorder)
                    {
                        j++;
                        var dateText = new TextBlock();
                        var date = item.Key;
                        dateText.Text = $"{j}.Orders for {date.Year}-{date.Month}-{date.Day} are : ";
                        dateText.FontWeight = FontWeight.FromOpenTypeWeight(40);
                        dateText.FontSize = 14;
                        ChangeFoodList.Items.Add(dateText);
                        
                        int i = 0;
                        foreach (var val in item)
                        {
                            ++i;
                            var str = new TextBlock();
                            str.FontWeight = FontWeight.FromOpenTypeWeight(40);
                            str.FontSize = 12;
                            str.Text = $"{i}. Name : {val.food.Name} \nAmount : {val.FoodNumber} \nPrice : {val.FoodNumber * val.food.FinalPrice }\n";
                            ChangeFoodList.Items.Add(str);
                        }
                    }
                }
                else
                {
                    var dateText = new TextBlock();
                    dateText.Text = $"No order is available. ";
                    dateText.FontWeight = FontWeight.FromOpenTypeWeight(40);
                    dateText.FontSize = 14;
                    ChangeFoodList.Items.Add(dateText);
                }
            }
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            customers_ordering ordering = new customers_ordering();
            ordering.Title.Text = "Order new food";
            ordering.Show();
            
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            customers_ordering ordering = new customers_ordering();
            ordering.Title.Text = "Edit order";
            ordering.Show();

        }
        private void DiscountCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (DiscountCheck.IsChecked == true)
            {
                DiscountCodeBox.IsEnabled = true;
            }
            else
            {
                DiscountCodeBox.IsEnabled = false;
            }
        }

        private void AllOrderCheck_Checked(object sender, RoutedEventArgs e)
        {
            MenuDate.IsEnabled = false;
        }

        private void ChangeFoodList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(ChangeFoodList, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {

            }
        }
        private void AllOrderCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            MenuDate.IsEnabled = true;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in Customers.CurrentCusomer.OrderedFood)
                {
                    item.food.ChangeRemainingNumber(item.FoodNumber);
                    Restaurant.AddFood(item.food, item.Date);
                    var temp = Restaurant.ReservedOrder.Where(x => x.food.Name == item.food.Name && x.Date == item.Date).First();
                    temp.ChangeFoodNum(-1 * item.FoodNumber);
                    item.ChangeFoodNum(item.FoodNumber * -1);
                }
                for (int i = Customers.CurrentCusomer.OrderedFood.Count - 1; i >= 0; --i)
                {
                    if (Customers.CurrentCusomer.OrderedFood[i].FoodNumber == 0)
                    {
                        Customers.CurrentCusomer.OrderedFood.RemoveAt(i);
                    }
                }
                MainWindow main = new MainWindow();
                this.Visibility = Visibility.Collapsed;
                main.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in Customers.CurrentCusomer.OrderedFood)
                {
                    item.food.ChangeRemainingNumber(item.FoodNumber);
                    Restaurant.AddFood(item.food, item.Date);
                    var temp = Restaurant.ReservedOrder.Where(x => x.food.Name == item.food.Name && x.Date == item.Date).First();
                    temp.FoodNumber = 0;
                    //temp.ChangeFoodNum(-1 * item.FoodNumber);
                    item.ChangeFoodNum(item.FoodNumber * -1);
                }
                for (int i = Customers.CurrentCusomer.OrderedFood.Count - 1; i >= 0; --i) 
                {
                    if(Customers.CurrentCusomer.OrderedFood[i].FoodNumber == 0)
                    {
                        Customers.CurrentCusomer.OrderedFood.RemoveAt(i);
                    }
                }
                MessageBox.Show("Reseting Orders.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void RestaurantInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.restaurant.ShowRestaurantInfo();
        }

        private void FilterType_Checked(object sender, RoutedEventArgs e)
        {
            FoodTypesCombo.IsEditable = true;
            FoodTypesCombo.ItemsSource = FooDType.Foodtype.Values;
        }

        private void FilterType_Unchecked(object sender, RoutedEventArgs e)
        {
            FoodTypesCombo.IsEditable = false;
            FoodNamesCombo.ItemsSource = Food.FoodsMenu.Select(x => x.Name).ToList();
        }       

        private void BuyOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var factor = new Show_factor();
            this.Visibility = Visibility.Collapsed;
            factor.Show();

        }

        private void FoodTypesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FoodTypesCombo.Text != null)
            {
                FoodNamesCombo.ItemsSource = Food.FoodsMenu.Where(x => x.FoodType == FooDType.FindKey(FoodTypesCombo.Text)).Select(x => x.Name).ToList();

            }
        }

        private void FoodInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FoodNamesCombo.Text != null)
                {
                    var x = Food.findFoodByName(FoodNamesCombo.Text);
                    x.showinfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
