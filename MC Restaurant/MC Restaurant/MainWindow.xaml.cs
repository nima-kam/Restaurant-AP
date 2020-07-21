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
        /// <summary>
        /// saving information in a file
        /// </summary>
        void SaveInfo();
        string FullName { get; }
        string PhoneNum { get; }
        string Address { get; }
        string Email { get; }
    }
    class Customers : Buy, Person
    {
        #region Properties
        string _FullName;
        Regex FullNameAlg = new Regex(@"^[A-Z]+([a-z]{2,})$");
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
        Regex phoneAlg = new Regex(@"^\+?(9|0){0,2}\d{9,12}");
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
                    _Email = value;
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
        Regex PasswordAlg = new Regex(@"^.{8,32}$");
        Regex passnum = new Regex(@"\d");
        Regex passAlpha = new Regex(@"\D");
        Regex PassSign = new Regex(@"[!@#$%&*\-_?<>]");
        public string Password
        {
            protected get
            {
                return _Password;
            }
            set
            {
               
                if (PasswordAlg.IsMatch(value)  && passAlpha.Match(value).Success && passnum.Match(value).Success && PassSign.Match(value).Success)
                    _Password = value;
                else
                {
                    throw new Exception("Password format is wrong.");
                }
            }
        }
        public static Customers CurrentCusomer;
        public long TotalBuy { get; protected set; }
        public double Tax { get; protected set; }
        public string Address { get; set; }
        #endregion
        /// <summary>
        /// find a customers with the same name and password
        /// </summary>
        /// <param name="UserEN"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static Customers FindCustomers(string UserEN, string Password)
        {
            if (checkPass(UserEN, Password))
            {
                StreamReader reader = new StreamReader("CustomersInfo.txt");
                List<string[]> lines = new List<string[]>();
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine().Split(' '));
                }
                reader.Close();
                //Name address email password Phonenum id buytimes
                var te = lines.Where(x => (x[2] == UserEN || x[4] == UserEN) && x[3] == Password).First();
                var cus = new Customers(te[0], te[1], te[4], te[2], te[5], te[3],int.Parse(te[6]), true);
                return cus;
            }
            throw new Exception("Email and Phone number or Password not correct.");
        }
        private Customers(string name, string address, string Phone, string Email, string ID, string Password, int BuyTimes, bool savefile)
        {
            this.Address = address;
            this.FullName = name;
            this.PhoneNum = Phone;
            this.Email = Email;
            this.IDataObject = ID;
            this.Password = Password;
            this.buyTimes = BuyTimes;
            Tax = 0.09;
            Discount = 0;
        }

        public Customers(string name, string address, string Phone, string Email, string ID, string Password )
        {
            if (Customers.CheckEmail_Phone(Email, Phone))
            {
                throw new Exception("The email or phone Number has been used before.");
            }
            this.Address = address;
            this.FullName = name;
            this.PhoneNum = Phone;
            this.Email = Email;
            this.IDataObject = ID;
            this.Password = Password;
            this.buyTimes = 0;
            Tax = 0.09;
            SaveInfo();
        }

        public static bool checkPass(string UserEN, string Password)
        {
            if (!File.Exists("CustomersInfo.txt"))
            {
                StreamWriter writer = new StreamWriter("CustomersInfo.txt");
                writer.Close();
                return false;
            }
            else
            {
                StreamReader reader = new StreamReader("CustomersInfo.txt");
                List<string[]> lines = new List<string[]>();
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine().Split(' '));
                }
                reader.Close();
                //Name address email password Phonenum id
                if (lines.Any(x => x[2] == UserEN || x[4] == UserEN))
                {
                    if (lines.Where(x => x[2] == UserEN || x[4] == UserEN).First()[3] == Password)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// check the file for same email and phone number.
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Phone"></param>
        /// <returns>If finds the same Email or phone number, return True, Otherwise, return false.</returns>
        public static bool CheckEmail_Phone(string Email, string Phone)
        {
            if (!File.Exists("CustomersInfo.txt"))
            {
                StreamWriter writer = new StreamWriter("CustomersInfo.txt");
                writer.Close();
                return false;
            }
            else
            {
                StreamReader reader = new StreamReader("CustomersInfo.txt");
                List<string[]> lines = new List<string[]>();
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine().Split(' '));
                }
                reader.Close();
                //Name address email password Phonenum id
                if (lines.Any(x => x[2] == Email || x[4] == Phone))
                {
                    return true;
                }
                return false;
            }
        }
        protected void CalculateProfit()//**
        {

        }
        /// <summary>
        /// save customers info on file.
        /// </summary>
        public void SaveInfo()
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
            //Name address email password Phonenum id
            this.FullName = this.FullName.Replace(' ', '_');
            this.Email = Email.Replace(' ', '\0');
            this.IDataObject = this.IDataObject.Replace(' ', '\0');
            this.Address = this.Address.Replace(' ', '-');
            string Newcustomer = $"{this.FullName} {this.Address} {this.Email} {this.Password} {this.PhoneNum} {this._ID} {this.buyTimes}";
            lines.Add(Newcustomer);
            StreamWriter stream = new StreamWriter("CustomersInfo.txt");
            foreach (var item in lines)
            {
                stream.WriteLine(item);
            }
            stream.Close();
        }
    }
    class Manager : Person
    {
        public static bool isInitialized = false;
        public static Restaurant restaurant;
        static public Manager logedInManager = null;
        public string FullName { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; }
        public string Email { get; set; }
        public int LoginTimes { get; }
        public Manager(string FullName, string PassWord, int LoginTimes = 0)
        {
            if (ManagerNameCheck(FullName))
            {
                if (ShowPassword(FullName, FindManager(FullName)) == PassWord)
                {
                    this.FullName = FullName;
                    this.LoginTimes = FindManager(FullName);
                    this.LoginTimes++;
                    SaveInfo();
                }
                else
                {
                    throw new Exception("Password is wrong.");
                }
            }
            else
            {
                throw new Exception("Username is not in correct format.");
            }

        }
        static string ShowPassword(string Name, int logintime)
        {
            int sound = 0;
            foreach (char c in Name)
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
            int one = logintime % 10;
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
            var arr = pass.ToArray();
            return new string(arr);
        }
        /// <summary>
        /// check if a username is in correct format
        /// </summary>
        /// <param name="InName"></param>
        /// <returns></returns>
        public static bool ManagerNameCheck(string UserName)
        {
            Regex Name = new Regex(@"\b(?:[a-zA-Z])+a+d+m+i+n*\d");
            return Name.IsMatch(UserName);
        }
        struct NameLog
        {
            public string Name;
            public int LoginNum;
            public NameLog(string name, int loginNum)
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
            if (!File.Exists("managerInfo.txt"))
            {
                StreamWriter writer = new StreamWriter("managerInfo.txt");
                writer.WriteLine();
                writer.Close();
            }
            var streamReader = new StreamReader("managerInfo.txt");
            List<string> lines = new List<string>();
            while (!streamReader.EndOfStream)
            {
                lines.Add(streamReader.ReadLine());
            }
            streamReader.Close();
            var nameLog = new List<NameLog>();
            for (int i = 1; i < lines.Count; ++i)
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
                writer.WriteLine();
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
                for (int i = 0; i < nameLog.Count; i++)
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
    #region foodList
    public struct FoodList
    {
        public Food food { get;  set; }
        public int FoodNumber { get;  set; }
        public DateTime Date { get; set; }

        public FoodList(Food food, int N, DateTime date)
        {
            this.food = food;
            this.FoodNumber = N;
            this.Date = date;
        }
        /// <summary>
        /// add or delete a number of foods.
        /// </summary>
        /// <param name="x"></param>
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
    #endregion
    abstract class Buy
    {
        public int buyTimes { get; protected set; }
        public List<FoodList> OrderedFood = new List<FoodList>();
        public double TotalPrice { get; set; } = 0;
        public double Discount { get; protected set; }
        public void SetDiscount(int d)
        {
            if (d < 0 || d >= 100)
            {
                throw new Exception("Discount is not qualified.");
            }
        }
        /// <summary>
        /// Adding food to ordered food
        /// </summary>
        /// <param name="NewOrder">the food is going be add.</param>
        /// <param name="Amount">amount of food to be add.</param>
        
        public void AddFood(Food NewOrder, int Amount,DateTime date)
        {
            if (OrderedFood.Count == 0)
            {
                OrderedFood.Add(new FoodList(NewOrder, Amount,date));
                
                MessageBox.Show($"{NewOrder.FoodType} ordered for date {date} successfully.");
            }
            else
            {
                if (OrderedFood.Any(x => x.food == NewOrder && x.Date == date))
                {
                    var foods = OrderedFood.Where(x => x.food.Name == NewOrder.Name && x.Date == date).Select(x => x).ToList();
                    foods[0].ChangeFoodNum(Amount);
                    MessageBox.Show($"{NewOrder.FoodType} ordered for date {date} successfully.");
                }
                else
                {
                    OrderedFood.Add(new FoodList(NewOrder, Amount,date));
                    MessageBox.Show($"{NewOrder.Name} ordered for date {date} successfully.");
                }
            }
        }
        public void PayOnline()
        {

        }
        public void PayOut()
        {

        }

    }
    static class foodExtention
    {
        public static void showinfo(this Food food)
        {
            MessageBox.Show($"Name : {food.Name} \ningredients : {food.ingredients}\nType : {FooDType.Foodtype[food.FoodType]} \nPrice : {food.FinalPrice}\n");
        }
    }
   
    class FooDType
    {

        public static Dictionary<int, string> Foodtype = new Dictionary<int, string>();
        public int key { get; set; }
        public string Value { get { return this[this.key]; } }
        static int numberoftypes = 0;
        public static int FindKey(string Value)
        {
            if (IsFoodType(Value))
            {
                foreach (var item in Foodtype)
                {
                    if (item.Value == Value)
                    {
                        return item.Key;
                    }
                }
            }
            throw new Exception("No match type found.");
        }
        public static bool IsFoodType(int i)
        {
            return (Foodtype.ContainsKey(i));
        }
        public static bool IsFoodType(string s)
        {
            foreach (var item in Foodtype)
            {
                if (item.Value == s)
                {
                    return true;
                }
            }
            return false;
        }
        public string this[int i]
        {
            get
            {
                if (Foodtype.ContainsKey(i))
                {
                    return Foodtype[i];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
        /// <summary>
        /// Adding a new food type
        /// </summary>
        /// <param name="type"> Name of the new type</param>
        public static void add(string type)
        {
            if (!File.Exists("food type.txt"))
            {
                StreamWriter writer = new StreamWriter("food type.txt");
                writer.Close();
            }
            ++numberoftypes;
            StreamReader reader = new StreamReader("food type.txt");
            List<string> line = new List<string>();
            while (!reader.EndOfStream)
            {
                line.Add(reader.ReadLine());
            }
            reader.Close();
            Foodtype.Add(numberoftypes, type);
            line.Add($"{numberoftypes} {type}");
            StreamWriter stream = new StreamWriter("food type.txt");
            foreach (var it in line)
            {
                stream.WriteLine(it);
            }
            stream.Close();
        }
        static void save()
        {
            StreamWriter stream = new StreamWriter("food type.txt");
            foreach (var it in Foodtype)
            {
                stream.WriteLine($"{it.Key} {it.Value}");
            }
            stream.Close();
        }
        public static void intializefoodType()
        {
            if (!File.Exists("food type.txt"))
            {
                StreamWriter writer = new StreamWriter("food type.txt");
                writer.Close();
            }
            else
            {
                StreamReader reader = new StreamReader("food type.txt");
                List<string> line = new List<string>();
                while (!reader.EndOfStream)
                {
                    line.Add(reader.ReadLine());
                }
                reader.Close();
                foreach (var it in line)
                {
                    ++numberoftypes;
                    var str = it.Split(' ');
                    Foodtype.Add(numberoftypes, str[1]);
                }
            }
        }
    }
    public class Food
    {
        public bool HaveImage;
        public string ImagePath;
        public static List<Food> FoodsMenu = new List<Food>();
        static int foodNumbers = 1;
        public int ID;
        public string Name { get; private set; }
        public string Ex_Name { get; private set; }
        public Image FoodImage { get; set; }
        public int FoodType;
        public double Price { get; set; }
        public double Ex_Price { get; set; }
        public double FinalPrice { get; protected set; }
        public double ProfitPercent { get; set; }
        int _RemainingNumber;
        public string ingredients { get; protected set; }
        public string Ex_ingredients { get; protected set; }
        Food(string Name, string type, double Price, string Ingredient, int ID)//reading from file food
        {
            this.Name = this.Ex_Name = Name;
            this.FoodType = FooDType.FindKey(type);
            this.Price = this.Ex_Price = Price;
            ProfitPercent = 24;
            this.ingredients = this.Ex_ingredients = Ingredient;
            this.ID = ID;
            if (ID >= foodNumbers)
            {
                foodNumbers = ID + 1;
            }
            SetFinalPrice();
        }
        /// <summary>
        /// Find and return a food by its name
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="availableNum"></param>
        /// <returns></returns>
        public static Food findFoodByName(string Name, int availableNum = 0)
        {
            if (!IsMatchFoodName(Name))
            {
                throw new Exception($"No food named {Name} found.");
            }
            else
            {
                for (int i = 0; i < FoodsMenu.Count; ++i)
                {
                    if (FoodsMenu[i].Name == Name)
                    {
                        var ff = new Food(FoodsMenu[i].Name, FooDType.Foodtype[FoodsMenu[i].FoodType], FoodsMenu[i].Price, FoodsMenu[i].ingredients, FoodsMenu[i].ID);
                        ff.RemainingNumber = availableNum;
                        return ff;

                    }
                }
                throw new Exception($"No food named {Name} found.");
            }

        }
        public Food(string Name, string type, double Price, string Ingredient)//making new food
        {
            if (IsMatchFoodName(Name))
            {
                throw new Exception("Name already exists.");
            }
            this.Name = this.Ex_Name = Name;
            this.FoodType = FooDType.FindKey(type);
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
        public static bool IsMatchFoodName(string Name)
        {
            foreach (var item in FoodsMenu)
            {
                if (item.Name == Name || item.Ex_Name == Name)
                {
                    return true;
                }
            }
            return false;
        }

        static Food ReadFood(string line)
        {
            string[] items = line.Split(' ');
            //ID, Name, Type,ingrediant, Remaining Number, Price            
            Food food = null;
            food = new Food(items[1], FooDType.Foodtype[int.Parse(items[2])], double.Parse(items[5]), items[3], int.Parse(items[0]));
            food.RemainingNumber = int.Parse(items[4]);
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
                    for (int i = 1; i < lines.Count; ++i)
                    {
                        FoodsMenu.Add(ReadFood(lines[i]));
                        foodNumbers++;
                    }
                }

            }
            catch (Exception ex)
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
            this.Name = Name.Replace(' ', '_');
            this.ingredients = ingredients.Replace(' ', '-');            
            string info = $"{this.ID} {this.Name} {this.FoodType.ToString()} {this.ingredients} {this.RemainingNumber} {this.Price}";
            lines.Add(info);
            foreach (var s in lines)
            {
                streamWriter.WriteLine(s);
            }
            streamWriter.Close();
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
        public void ShowInfo()
        {
            MessageBox.Show($"Name : {this.Name}\nPrice : {this.Price}\nIngredients : {this.ingredients}\nType : {FooDType.Foodtype[this.FoodType]}");
        }
    }

    class Restaurant
    {
        #region Properties
        static Dictionary<DateTime, List<Food>> CalanderFood = new Dictionary<DateTime, List<Food>>();
        public string Name { get; private set; }
        public string Region { get; private set; }
        public string Address { get; private set; }
        public double RestaurantProfit { get; }
        string _PhoneNum;
        /// <summary>
        /// has restaurant started working.
        /// </summary>
        public static bool IsStablished { get; private set; } 
        public string PhoneNum
        {
            get
            {
                return _PhoneNum;
            }
            private set
            {
                Regex Phone = new Regex(@"^\+?(9|0){0,2}\d{9,12}");
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
        #endregion
        public static List<FoodList> ReservedOrder = new List<FoodList>();
        public static List<FoodList> PayedOrder = new List<FoodList>();
        public void AddFoodReserved(Food NewOrder, int Amount, DateTime date)
        {
            if (ReservedOrder.Count == 0)
            {
                ReservedOrder.Add(new FoodList(NewOrder, Amount, date));
            }
            else
            {
                if (ReservedOrder.Any(x => x.food == NewOrder && x.Date == date))
                {
                    var foods = ReservedOrder.Where(x => x.food.Name == NewOrder.Name && x.Date == date).Select(x => x).ToList();
                    foods[0].ChangeFoodNum(Amount);
                }
                else
                {
                    ReservedOrder.Add(new FoodList(NewOrder, Amount, date));
                }
            }
        }
        static void stablish()
        {
            IsStablished = true;
        }
        public Restaurant(string Name, string Address, string Region, string PhoneNum)
        {
            this.Name = Name;
            this.Region = Region;
            this.Address = Address;
            this.PhoneNum = PhoneNum;
            stablish();
            this.save();
        }

        public static void Initialize()
        {
            if (File.Exists("Restaurant.txt"))
            {                
                StreamReader stream = new StreamReader("Restaurant.txt");
                var str = stream.ReadLine().Split(' ');
                stream.Close();
                Manager.restaurant = new Restaurant(str[0], str[str.Length - 2], str[1], str[str.Length - 1]);
                stablish();                
            }
            else
            {
                MessageBox.Show("Restaurant not stablished.");
                IsStablished = false;
            }
        }
        void save()
        {
            this.Name = this.Name.Replace(' ', '_');
            this.Address = this.Address.Replace(' ', '_');
            StreamWriter writer = new StreamWriter("Restaurant.txt");
            writer.WriteLine($"{Name} {Region} {Address} {PhoneNum}");
            writer.Close();
            stablish();
            MessageBox.Show("Restaurant info saved.");
        }
        public void ShowRestaurantInfo()
        {
            MessageBox.Show($"Restaurant Info \n Name: {this.Name} \n" +
                $" Address: {this.Address} \n Region: {this.Region} \n" +
                $" Phone number: {this.PhoneNum} \n", "Restaurant information");
        }

        /// <summary>
        /// Gets a date and return its food list.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<Food> ReadDateFood(DateTime date)
        {
            if (DateTime.Today.CompareTo(date) <= 0)
            {
                List<Food> foods = new List<Food>();
                if (CalanderFood.TryGetValue(date, out foods))
                {
                     return foods;
                }
                else
                {
                    MessageBox.Show("No food added yet.");
                    foods = new List<Food>();
                    CalanderFood.Add(date, foods);
                    return foods;
                }
            }
            else
            {
                throw new Exception("Entered date is no longer accessable.");
            }
        }
        public static void SaveCalander()
        {
            if (!File.Exists(@"Calander.txt"))
            {
                StreamWriter writer = new StreamWriter(@"Calander.txt");
                writer.Close();
            }
            List<string> filestr = new List<string>();
            foreach (var item in CalanderFood)//reading from dictionary and convert to string
            {
                string date = $"*/{item.Key.Year}/{item.Key.Month}/{item.Key.Day}";
                filestr.Add(date);
                foreach (var T in item.Value)
                {
                    string str = $"{T.Name} {T.RemainingNumber}";
                    filestr.Add(str);
                }
            }
            StreamWriter writer1 = new StreamWriter(@"Calander.txt");
            foreach (var item in filestr)
            {
                writer1.WriteLine(item);
            }
            writer1.Close();
        }
        /// <summary>
        /// in the begining of program reads calander from file.
        /// </summary>
        public static void InitializeCalander()
        {
            if (!File.Exists(@"Calander.txt"))
            {
                StreamWriter stream = new StreamWriter(@"Calander.txt");
                stream.Close();
            }
            else
            {
                IsStablished = true;
                List<string> lines = new List<string>();
                StreamReader reader = new StreamReader(@"Calander.txt");
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());

                }
                reader.Close();
                for (int i = 0; i < lines.Count; ++i)
                {
                    DateTime date;
                    List<Food> foods = new List<Food>();
                    if (lines[i][0] == '*')
                    {
                        var datestr = lines[i].Split('/');
                        date = new DateTime(int.Parse(datestr[1]), int.Parse(datestr[2]), int.Parse(datestr[3]));
                        int j = i + 1;
                        while (j < lines.Count )
                        {
                            if (lines[j][0] == '*') break;
                            var s = lines[j].Split(' ');
                            foods.Add(Food.findFoodByName(s[0], int.Parse(s[1])));
                            ++j;
                        }
                        if (date.CompareTo(DateTime.Today) >= 0)
                        {
                            CalanderFood.Add(date, foods);
                        }
                    }
                }

            }
        }
        /// <summary>
        /// Adding an amount of food to a specific date.
        /// </summary>
        /// <param name="food"></param>
        /// <param name="date"></param>
        public static void AddFood(Food food, DateTime date)
        {
            if (DateTime.Today.CompareTo(date) <= 0)
            {
                List<Food> foods = new List<Food>();
                if (CalanderFood.TryGetValue(date, out foods))
                {
                    if (foods.Any(x => x.Name == food.Name))
                    {
                        for (int i = 0; i < foods.Count; ++i)
                        {
                            if (foods[i].Name == food.Name)
                            {
                                foods[i].ChangeRemainingNumber(food.RemainingNumber);
                                CalanderFood[date] = foods;
                                MessageBox.Show($"food added to {date}.");
                            }
                        }
                    }
                    else
                    {
                        foods.Add(food);
                        CalanderFood[date] = foods;
                        MessageBox.Show($"food added to {date}.");
                    }
                }
                else
                {
                    foods.Add(food);
                    CalanderFood.Add(date, foods);
                    MessageBox.Show($"food added to {date}.");
                }
            }
            else
            {
                throw new Exception("Entered date is no longer accessable.");
            }
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        public static bool isInitialized = false;
        public MainWindow()
        {
            InitializeComponent();
            if (Manager.isInitialized == false)
            {
                FooDType.intializefoodType();
                Restaurant.Initialize();
                Food.IntializeFoods();
                Restaurant.InitializeCalander();
                
                Manager.isInitialized = true;
            }
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
