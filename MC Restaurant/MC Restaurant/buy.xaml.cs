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
        private void Filter_food_check_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Filter_food_check_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ShowItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuDate.SelectedDate != null)
            {
                if (DateTime.Today.CompareTo(MenuDate.SelectedDate) < 0)
                {
                    MessageBox.Show("Selected date no longer accessable.\nPlease select another one.");
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
                            str.Text = $"{i}. Name : {item.food.Name} \nAmount : {item.FoodNumber} \nPrice : {item.FoodNumber*item.food.FinalPrice }\n";
                            ChangeFoodList.Items.Add(str);
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select a date.");
            }
           
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void ChangeFoodList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(ChangeFoodList, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {

            }
        }
    }
}
