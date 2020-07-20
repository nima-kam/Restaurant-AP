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
            
        }
        private void ShowFactor()
        {
            try
            {
                int i = 0;
                var group = new TableRowGroup();
                Customers.CurrentCusomer.TotalPrice = 0;
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
                        row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.food.FinalPrice * item.FoodNumber}"))));
                        Customers.CurrentCusomer.TotalPrice += item.food.FinalPrice * item.FoodNumber;
                        row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.Date.Year}{item.Date.Month}-{item.Date.Day}"))));
                        group.Rows.Add(row);
                    }  
                }
                TableOfFactor.RowGroups.Add(group);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"! Alart !");
            }

        }

        private void PayOnline_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PayHomeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
