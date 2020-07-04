﻿using System;
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
using System.IO; 

using System.Text.RegularExpressions;
using System.Net.Http.Headers;

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
    interface Person
    {
        void SaveInfo();
        string FullName { get; }
        string PhoneNum { get;}
        string Address { get;}
        string Email { get; }

    }
    class Customers : Buy,Person
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
        Regex phoneAlg = new Regex(@"{8-12}((?:\+)+\d[0-9])");
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
        Regex PasswordAlg = new Regex(@"\b({8-32}[a-z0-9!#$%&\-*+/?^_~-])\z");
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
        public static Customers CurrentCusomer;
        public long TotalBuy { get; protected set; }
        public double Tax{ get; protected set; }

        public string Address { get ; set; }

        protected void CalculateProfit()
        {
            
        }
        public void SaveInfo()//**
        {
            StreamReader streamReader;
            if (!File.Exists("CustomersInfo.txt"))
            {
                StreamWriter writer = new StreamWriter("CustomersInfo.txt");
                writer.Close();
            }
            streamReader = new StreamReader("CustomersInfo.txt");
            List<string> lines = new List<string>();
            while (!streamReader.EndOfStream)
            {
                lines.Add(streamReader.ReadLine());
            }
            streamReader.Close();
        }
    }
    class Manager : Person
    {

        public string FullName { get ; set ; }
        public string PhoneNum { get ; set; }
        public string Address { get;  }
        public string Email { get ; set ; }
        private string Password {  get; set; }
        public int LoginTimes { get; } = 0;

        public Manager(string FullName, string PassWord, int LoginTimes = 0)
        {
            if (ManagerNameCheck(FullName))
            {
                if (FindManager(FullName) == 0)
                {
                    this.FullName = FullName;
                    
                }
            }
            else
            {
                throw new Exception("Username is not in correct format.");
            }
            
        }
        string ShowPassword(string Name,int logintime)
        {
            int sound = 0;
            foreach(char c in Name)
            {
                switch (c)
                {
                    case 'i':
                    case 'o':
                    case 'a':
                    case 'u':
                    case 'e':
                        ++sound;
                        break;
                }
            }
            int one = this.LoginTimes%10;
            List<char> pass = new List<char>();
            while (one > 0)
            {
                --one;
                pass.Add('1');
            }
            while (sound > 0)
            {
                pass.Add('0');
                --sound;
            }
            var arr= pass.ToArray();
            return new string(arr);
        }

        /// <summary>
        /// check if a username is in correct format
        /// </summary>
        /// <param name="InName"></param>
        /// <returns></returns>
        public static bool ManagerNameCheck(string UserName)
        {
            Regex Name = new Regex(@"\b(?:[a-zA-Z])+admin*\d\z");
            return Name.IsMatch(UserName);
        }
        struct NameLog
        {
            public string Name;
            public int LoginNum;
            public NameLog(string name,int loginNum)
            {
                this.Name = name;
                this.LoginNum = loginNum;
            }
        }
        /// <summary>
        /// find a manager info from File
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>
        /// Return the manager loginTimes.
        /// </returns>
        public static int FindManager(string Name)
        {
            
            var streamReader = new StreamReader("managerInfo.txt");
            List<string> lines = new List<string>();
            while (!streamReader.EndOfStream)
            {
                lines.Add(streamReader.ReadLine());
            }
            streamReader.Close();
            var nameLog = new List<NameLog>();
            for(int i = 1; i < lines.Count; ++i)
            {
                var ss = lines[i].Split(' ');
                nameLog.Add(new NameLog(ss[0], int.Parse(ss[1])));
            }
            var log = nameLog.Where(x => x.Name == Name).Select(x => x.LoginNum);
            if (log.Count() >= 1)
            {
                return log.First();

            }
            return 0;
        }
        /// <summary>
        /// Saving data on a File
        /// </summary>
        public void SaveInfo()
        {
            StreamReader streamReader;
            if (!File.Exists("managerInfo.txt"))
            {
                StreamWriter writer = new StreamWriter("managerInfo.txt");
                writer.Close();
            }

            streamReader = new StreamReader("managerInfo.txt");
            List<string> lines = new List<string>();
            while (!streamReader.EndOfStream)
            {
                lines.Add(streamReader.ReadLine());
            }
            string FirstLine = "UserName LoginTimes";
            lines[0] = FirstLine;
            streamReader.Close();
            if (this.LoginTimes == 0 || this.LoginTimes == 1)
            {
                string thisInfo = $"{this.FullName} {this.LoginTimes}";
                lines.Add(thisInfo);
            }
            else
            {
                var nameLog = new List<NameLog>();
                for (int i = 1; i < lines.Count; ++i)
                {
                    var ss = lines[i].Split(' ');
                    nameLog.Add(new NameLog(ss[0], int.Parse(ss[1])));
                }
                for(int i = 0; i < nameLog.Count; i++)
                { 
                    if (nameLog[i].Name == this.FullName)
                    {
                        lines[i + 1] = $"{this.FullName} {this.LoginTimes}";
                    }
                }
            }
            StreamWriter writer1 = new StreamWriter("managerInfo.txt");
            foreach (var item in lines)
            {
                writer1.WriteLine(item);
            }
            writer1.Close();
        }
    }

    abstract class Buy
    {
        public struct FoodList
        {
            public Food food { get; private set; }
            public int FoodNumber { get; private set; }

            public FoodList(Food food,int N)
            {
                this.food = food;
                this.FoodNumber = N;

            } 
            public void ChangeFoodNum(int x)
            {
                if (x + this.FoodNumber > food.RemainingNumber)
                {
                    throw new Exception("Not enough food available to order.");
                }
                else if (x + this.FoodNumber < 0)
                {
                    throw new Exception("Not enough food available in your list.");
                }
                else
                { 
                    this.FoodNumber += x;
                }
            }
        }
        public List<FoodList> OrderedFood = new List<FoodList>();        
        public double TotalPrice { get; protected set; }
        public void AddFood(Food NewOrder, int Amount)//***
        {
            if (OrderedFood.Count == 0)
            {
                OrderedFood.Add(new FoodList(NewOrder, Amount));
                MessageBox.Show($"{NewOrder.FoodType} ordered successfully.");
            }
            else
            {
                if (OrderedFood.Where(x => x.food == NewOrder).Count() == 1)
                {
                    var foods =  OrderedFood.Where(x => x.food == NewOrder).Select(x => x) ;
                    
                }                 
            }
        }
    }
    enum FoodType
    {
        SeaFood ,Chickenfries,Hamborgar,Pizza,Salad,Sandwich
    }
    class Food
    {
        static List<Food> FoodsMenu = new List<Food>();
        static int foodNumbers = 1;
        public int ID ;
        public string Name { get; private set; }
        public string Ex_Name { get; private set; }
        public Image FoodImage { get; protected set; }
        public FoodType FoodType; 
        public double Price { get; set; }
        public double Ex_Price { get; set; }
        public double FinalPrice { get; protected set; }
        public double ProfitPercent  { get; set; }
        int _RemainingNumber;
        public string ingredients { get; protected set; }
        public string Ex_ingredients { get; protected set; }
        Food(string Name, FoodType type, double Price, string Ingredient,int ID)//reading from file food
        {
            this.Name = this.Ex_Name = Name;
            this.FoodType = type;
            this.Price = this.Ex_Price = Price;
            ProfitPercent = 24;
            this.ingredients = this.Ex_ingredients = Ingredient;
            this.ID = ID;
            SetFinalPrice();            
        }
        public Food (string Name,FoodType type,double Price,string Ingredient)//making new food
        {
            if (IsMatchFoodName(Name))
            {
                throw new Exception("Name already exists.");
            }
            this.Name =this.Ex_Name= Name;
            this.FoodType = type;
            this.Price = this.Ex_Price = Price;
            ProfitPercent = 24;
            this.ingredients = this.Ex_ingredients = Ingredient;
            SetID();
            SetFinalPrice();
            PrintInfo();
            FoodsMenu.Add(this);
        }
        /// <summary>
        /// check if there are food with same name.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        static bool IsMatchFoodName(string Name)
        {
            foreach(var item in FoodsMenu)
            {
                if (item.Name == Name || item.Ex_Name == Name)
                {
                    return true;
                }
            }
            return false;
        }
        static Food ReadFood(string line)//**
        {
            string[] items = line.Split(' ');
            //ID, Name, Type,ingrediant, Remaining Number, Price
            int i = 0;
            Food food=null;
            while (true) 
            {
                FoodType foodType = (FoodType)i;
                if (foodType.ToString() == items[2])
                {
                     food = new Food(items[1], foodType, double.Parse(items[5]), items[3], int.Parse(items[0]));
                    food.RemainingNumber = int.Parse(items[4]);
                    return food;
                }
                ++i;
                if (i >= 6) break;
            }
            return food;
            
        }
        /// <summary>
        /// initialize the foods info in a list.
        /// </summary>
        public static void IntializeFoods()
        {
            StreamReader streamReader;
            try
            {
                if (!File.Exists("FoodInfo.txt"))
                {
                    StreamWriter writer = new StreamWriter("FoodInfo.txt");
                    writer.Close();
                }
                else
                {
                    streamReader = new StreamReader("FoodInfo.txt");
                    List<string> lines = new List<string>();
                    while (!streamReader.EndOfStream)
                    {
                        lines.Add(streamReader.ReadLine());
                    }
                    streamReader.Close();
                    for(int i = 1; i < lines.Count; ++i)
                    {
                        FoodsMenu.Add(ReadFood(lines[i]));
                        foodNumbers++;
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
        public void SetFinalPrice()
        {
            this.FinalPrice = this.Price / 100 * ProfitPercent + this.Price;
        }
        protected void SetID()
        {
            this.ID = foodNumbers;
            ++foodNumbers;
        }
        protected void PrintInfo()
        {
            StreamReader streamReader;
            if (!File.Exists("FoodInfo.txt"))
            {
                StreamWriter writer = new StreamWriter("FoodInfo.txt");
                writer.Close();
            }
            streamReader = new StreamReader("FoodInfo.txt");       
            List<string> lines = new List<string>();
            while (!streamReader.EndOfStream)
            {
                lines.Add(streamReader.ReadLine());
            }
            streamReader.Close();
            StreamWriter streamWriter = new StreamWriter("FoodInfo.txt");
            if (lines.Count == 0)
            {
                string firstline = "ID, Name, Type, ingridiants, Remaining Number, Price ";
                lines.Add(firstline);
            }
            string info = $"{this.ID} {this.Name} {this.FoodType.ToString()} {this.ingredients} {this.RemainingNumber} {this.Price}";
            lines.Add(info);
            foreach(var s in lines)
            {
                streamWriter.WriteLine(s);
            }
        }
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
        public void ChangeRemainingNumber(int x)
        {
            RemainingNumber += x;
        }
    }

    class Restaurant
    {
        public string Name { get; private set; }
        public string Region { get; private set; }
        public string Address { get; private set; }
        public double RestaurantProfit { get; }
        string _PhoneNum;
        public string PhoneNum
        {
            get
            {
                return _PhoneNum;
            }
            private set
            {
                Regex Phone = new Regex(@"\A*\++\A[09]{9-12}\d\z");
                if (Phone.IsMatch(value))
                {
                    _PhoneNum = value;
                }
                else
                {
                    throw new Exception("Phone number is not correct.");
                }
            }

        }
        List<Food> ReservedOrder = new List<Food>();
        List<Food> PayedOrder = new List<Food>();
        public Restaurant(string Name,string Address,string Region ,string PhoneNum)
        {
            this.Name = Name;
            this.Region = Region;
            this.Address = Address;
            this.PhoneNum = PhoneNum;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Food.IntializeFoods();
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
