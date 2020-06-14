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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Text.RegularExpressions;
namespace MC_Restaurant
{
    static class IDcodeSet
    {
        static public bool checkID(this string I)
        {
            int digits = 0;
            int same = 0;
            char ch = I[0];
            foreach (char cc in I)
            {
                if (cc != ch)
                {
                    ++same;
                }

                ++digits;
                switch (cc)
                {
                    case '0':
                    case '1':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        break;
                    default:
                        return false;
                }
            }
            if (same == 0)
            {
                return false;
            }
            if (digits != 10)
            {
                return false;
            }
            int a = (I[0] - '0');
            int b = 0;
            for (int i = 1; i <= 9; ++i)
            {
                b += (I[i] - '0') * (i + 1);
            }
            int c = b % 11;
            if (a == c && (c == 0 || c == 1)) return true;
            else if (c > 1)
            {
                if (a == Math.Abs(c - 11))
                {
                    return true;
                }
            }
            return false;
        }
    }
    class Person
    {
        string _FullName;
        Regex FullNameAlg = new Regex(@"\b[a-z,A-Z]");
        
        public string FullName
        {
            get
            {
                return _FullName; 
            }
            set
            {
                if (FullNameAlg.IsMatch(value))
                {
                    _FullName = value;
                }
                else
                {
                    throw new Exception("Name not valid.");
                }

            }
        }
        string _phoneNum;
        Regex phoneAlg = new Regex(@"\d[0-9]");
        public string PhoneNum
        {
            get
            {
                return _phoneNum;
            }
            set
            {
                if (phoneAlg.IsMatch(value))
                {
                    _phoneNum = value;
                }
                else
                {
                    throw new Exception("Phone number is not in correct format.");
                }
            }
        }
        Regex emailExpression = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (emailExpression.IsMatch(value))
                {
                    this.Email = value;
                }
                else
                {
                    throw new Exception("Wrong Email format.");
                }

            }
        }
        string _ID;
        public string IDataObject
        {
            get
            {
                return _ID;

            }
            set
            {
                if (value.checkID())
                {
                    _ID = value;
                }
                else
                {
                    throw new Exception("ID is not in correct format.");
                }
            }
        }
        string _Password;
        Regex PasswordAlg= new Regex(@"\b({8-32}[a-z0-9!#$%&\-*+/?^_~-])");
        public string Password//***
        {
            protected get
            {
                return _Password;
            }
            set
            {
                if (PasswordAlg.IsMatch(value))
                    _Password = value;
                else
                {
                    throw new Exception("Password format is wrong.");



                }
            }
        }
    }
    class Customers:Person 
    {

    }
    class Manager : Person
    { 

    }
    class Buy
    {
        public List<Food> OrderedFood = new List<Food>();
        public double TotalPrice { get; protected set; }

    }
    enum FoodType
    {
        SeaFood ,Chickenfries,Hamborgar,Pizza,Salad,Sandwich
    }
    class Food
    {

        public string Name { get; private set; }
        public FoodType FoodType; 
        public double Price { get; set; }
        public double Profit { get; set; }
        int _RemainingNumber;

        public int RemainingNumber
        {
            get
            {
                return _RemainingNumber;
            }
            set
            {
                if (value + _RemainingNumber >= 0)
                {
                    _RemainingNumber += value;
                }
                else
                {
                    throw new Exception("Food is not available.");
                }
            }
        }
        public string Type;
        public void ChangeRemainingNumber(int x)
        {

        }
    }



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            customers_login customers_Login = new customers_login();
            this.Visibility = Visibility.Collapsed;
            customers_Login.Show();
            
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            manager_login manager_Login = new manager_login();
            this.Visibility = Visibility.Collapsed;
            manager_Login.Show();

        }
    }
}
