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
using Microsoft.Win32;


namespace MC_Restaurant
{
    /// <summary>
    /// Interaction logic for Managment.xaml
    /// </summary>
    public partial class Managment : Window
    {
        public static string ImagePath;
        public Managment()
        {
            InitializeComponent();
            TypefoodCombo.ItemsSource = FooDType.Foodtype.Values;
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            adding_food_to_list adding_Food_To_List = new adding_food_to_list();
            adding_Food_To_List.Title.Text = "manager add";

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowDateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Food> datelist;
                if (MenuDate.SelectedDate != null)
                {
                    datelist = Restaurant.ReadDateFood(MenuDate.SelectedDate ?? default);
                    foreach (var f in datelist)
                    {
                        if (f.RemainingNumber != 0)
                        {
                            var text = new TextBlock();
                            text.Text = $"Food name:{f.Name}  Amount:{f.RemainingNumber}";
                            text.FontWeight = FontWeight.FromOpenTypeWeight(3);
                            ChangeFoodList.Items.Add(text);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter the date first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }      

        private void NewFoodButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                if (!Food.IsMatchFoodName(NameFoodBox.Text))
                {
                    if (FooDType.Foodtype.Any(x => x.Value == TypefoodCombo.Text))
                    {
                        var newFood = new Food(NameFoodBox.Text, TypefoodCombo.Text, double.Parse(PriceBox.Text), ingredientBox.Text);
                        if (PhotoCheck.IsChecked == true)
                        {
                            newFood.HaveImage = true;
                            newFood.ImagePath = ImagePath;
                            newFood.FoodImage = NewFoodImage;
                        }
                    }
                    else
                    {
                        FooDType.add(TypefoodCombo.Text);
                        var newFood = new Food(NameFoodBox.Text, TypefoodCombo.Text, double.Parse(PriceBox.Text), ingredientBox.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Uri imageUri = new Uri(openFileDialog.FileName, UriKind.Relative);
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                ImagePath = openFileDialog.FileName;
                NewFoodImage.Source = imageBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PhotoCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (PhotoCheck.IsChecked == true)
            {
                OpenFileButton.IsEnabled = true;
            }
            else
            {
                OpenFileButton.IsEnabled = false;
            }
        }
    }
}
