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
    /// Interaction logic for Show_factor.xaml
    /// </summary>
    public partial class Show_factor : Window
    {
        public Show_factor()
        {
            InitializeComponent();
            ShowFactor();
            DatePar = new Paragraph(new Run($"{DateTime.Now}"));
            NamePar = new Paragraph(new Run($"{Customers.CurrentCusomer.FullName}"));
        }
        private void ShowFactor()
        {
            try
            {
                int i = 0;
                var group = new TableRowGroup();
                Customers.CurrentCusomer.TotalPrice = 0;
                int numberOfItems = 0;
                foreach(var item in Customers.CurrentCusomer.OrderedFood)
                {
                    if (item.FoodNumber > 0)
                    {
                        ++i;
                        var row = new TableRow();
                        if (i % 2 == 0)
                        {
                            row.Background = Brushes.LightGray;
                        }
                        row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.food.Name}"))));
                        row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.FoodNumber}"))));
                        numberOfItems += item.FoodNumber;
                        row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.food.FinalPrice * item.FoodNumber}"))));
                        Customers.CurrentCusomer.TotalPrice += item.food.FinalPrice * item.FoodNumber;
                        row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.Date.Year}-{item.Date.Month}-{item.Date.Day}"))));
                        group.Rows.Add(row);
                    }
                }
                var Row = new TableRow();
                Row.Background = Brushes.Bisque;
                var cell = new TableCell(new Paragraph(new Run($"Total Price(including Tax):")));
                cell.ColumnSpan = 2;
                Row.Cells.Add(cell);
                cell = new TableCell(new Paragraph(new Run($"{ Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Tax + 1)}")));
                cell.ColumnSpan = 2;
                Row.Cells.Add(cell);
                group.Rows.Add(Row);

                Row = new TableRow();
                cell = new TableCell(new Paragraph(new Run($"Total discount:")));
                cell.ColumnSpan = 2;
                Row.Background = Brushes.FloralWhite;
                Row.Cells.Add(cell);
                cell = new TableCell(new Paragraph(new Run($"{ Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Discount )}")));
                cell.ColumnSpan = 2;
                Row.Cells.Add(cell);
                group.Rows.Add(Row);

                Row = new TableRow();
                var par = new Paragraph(new Run($"Number of all foods : {numberOfItems}"));
                par.FontSize = 13;
                par.FontWeight = FontWeights.DemiBold;
                par.TextAlignment = TextAlignment.Center;
                cell = new TableCell(par) ;
                cell.ColumnSpan =4;
                Row.Background = Brushes.Cornsilk;
                Row.Cells.Add(cell);
                group.Rows.Add(Row);

                Row = new TableRow();
                par = new Paragraph(new Run($"Final Price:"));
                par.FontSize = 13;
                par.FontWeight = FontWeights.DemiBold;
                cell = new TableCell(par);
                cell.ColumnSpan = 2;
                Row.Background = Brushes.Beige;
                Row.Cells.Add(cell);
                par = new Paragraph(new Run($"{Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Tax + 1) - (Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Discount))}"));
                par.FontSize = 13;
                par.FontWeight = FontWeights.DemiBold;
                cell = new TableCell(par);
                cell.ColumnSpan = 2;
                Row.Cells.Add(cell);
                group.Rows.Add(Row);
                TableOfFactor.RowGroups.Add(group);
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"! Alart !");
            }
        }

        private void PayOnline_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Your total buy is {Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Tax + 1) - (Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Discount))}.");
            MessageBox.Show($"{Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Tax + 1) - (Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Discount))} Toman took from your account.\n Thank you for your shop. ");
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Collapsed;
            main.Show();
        }

        private void PayHomeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Your total buy is {Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Tax + 1) - (Customers.CurrentCusomer.TotalPrice * (Customers.CurrentCusomer.Discount))}.");
            MessageBox.Show($" Thank you for your shop. ");
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Collapsed ;
            main.Show();
        }

        private void DiscountCodeButton_Click(object sender, RoutedEventArgs e)
        {



        }
    }
}
