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
            listOfTypesCombo.ItemsSource = FooDType.Foodtype.Values;
        }
        private void ChangeOrderNumber_Click(object sender, RoutedEventArgs e)
        {
            
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
                        MessageBox.Show("Please select a date.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"! Alart !");
            }
            
        }
    }
}
