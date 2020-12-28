using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApp2
{
    public class Player : INotifyPropertyChanged
    {
        public const double StartingMoney = 500000;
        private static double expences = 0;
        private double goal;
        private ObservableCollection<IValuablePieceOfPaper> ownings;
        private double money;
        private double investedMoney;
        public static int Difficulty { get; set; }
        public static int Random = 0;
        public static Market OurMarket;
        private ObservableCollection<Bond> bondList = new ObservableCollection<Bond>();
        private ObservableCollection<Deposit> depositList = new ObservableCollection<Deposit>();
        private ObservableCollection<Stock> stockList = new ObservableCollection<Stock>();

        public Player(string difficulty) // использовать вместо конструктора
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("random.bin", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                    Random = (int)formatter.Deserialize(fs);
            }
            Random++;
            using (FileStream fs = new FileStream("random.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Random);
            }
            Money = StartingMoney;
            InvestedMoney = 0;
            ownings = new ObservableCollection<IValuablePieceOfPaper>();
            switch (difficulty)
            {
                case "Easy":
                    Difficulty = 0;
                    expences = 25000;
                    goal = 1000000;
                    break;
                case "Normal":
                    Difficulty = 1;
                    expences = 50000;
                    goal = 3000000;
                    break;
                case "Hard":
                    Difficulty = 2;
                    expences = 75000;
                    goal = 5000000;
                    break;
                default:
                    break;
            }
            OurMarket = new Market();
            var l = new List<IValuablePieceOfPaper>();
            OurMarket.Fill(ref l, this);
            foreach (var el in OurMarket.MarketPapers)
            {
                if (Names.CountryNames().Contains(el.Name))
                {
                    bondList.Add(el as Bond);
                }
                if (Names.BankNames().Contains(el.Name))
                {
                    depositList.Add(el as Deposit);
                }
                if (Names.CompanyNames().Contains(el.Name))
                {
                    stockList.Add(el as Stock);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public double Money
        {
            get { return money; }
            set
            {
                money = value;
                RaisePropertyChanged("Money");
            }
        }
        public double InvestedMoney
        {
            get { return investedMoney; }
            set
            {
                investedMoney = value;
                RaisePropertyChanged("InvestedMoney");
            }
        }
        public double Goal
        {
            get { return goal; }
            set
            {
                goal = value;
                RaisePropertyChanged("Goal");
            }
        }
        public ObservableCollection<IValuablePieceOfPaper> Ownings
        {
            get { return ownings; }
            set
            {
                ownings = value;
                RaisePropertyChanged("Ownings");
            }
        }
        public ObservableCollection<Bond> BondList
        {
            get { return bondList; }
            set
            {
                bondList = value;
                RaisePropertyChanged("BondList");
            }
        }
        public ObservableCollection<Stock> StockList
        {
            get { return stockList; }
            set
            {
                stockList = value;
                RaisePropertyChanged("StockList");
            }
        }
        public ObservableCollection<Deposit> DepositList
        {
            get { return depositList; }
            set
            {
                depositList = value;
                RaisePropertyChanged("DepositList");
            }
        }

        private IValuablePieceOfPaper Find(string name, ObservableCollection<IValuablePieceOfPaper> list)
        {
            IValuablePieceOfPaper returnPaper = null;
            foreach (var p in list)
                if (p.Name == name)
                {
                    returnPaper = p;
                    break;
                }
            return returnPaper;
        }

        public void Buy(IValuablePieceOfPaper paper, double quantity)
        {
            paper.Quantity -= quantity;
            if (Money >= quantity * paper.Price)
            {
                Money = Math.Round(Money-quantity * paper.Price,2);
                InvestedMoney += quantity * paper.Price;
                if (!ownings.Contains(Find(paper.Name, ownings)))
                    ownings.Add(paper.CreateAPair(quantity)); //incapsulated
                else
                {
                    var paper0 = Find(paper.Name, ownings);
                    paper0.Quantity += quantity;
                    paper0.TotalValue = paper0.Price * paper0.Quantity;
                }

            }
        }
        public void Sell(IValuablePieceOfPaper paper, double quantity) // changed
        {
            int i = ownings.IndexOf(paper);
            if (quantity == ownings[i].Quantity)
            {
                Money += ownings[i].TotalValue;
                InvestedMoney -= ownings[i].TotalValue;
                OurMarket.MarketPapers.Add(ownings[i]);
                ownings.RemoveAt(i);
            }
            else
            {
                ownings[i].Quantity -= quantity;
                Money += quantity * ownings[i].Price;
                InvestedMoney -= quantity * ownings[i].Price;
                OurMarket.MarketPapers.Add(ownings[i].CreateAPair(quantity));
            }
        }
        public string RenewAll() // End Turn Button
        {
            Money -= expences;
            if (Money < 0)
            {
                // W A S T E D
                ownings.Clear();
                Money = -1000000000;
                InvestedMoney = -1000000000;
                var answer = new StringBuilder("You have lost. Try again.\r\n  In real life maybe...");
                return answer.ToString();
            }
            else
                if (Money >= goal)
                {
                    // V I C T O R Y
                    var answer = new StringBuilder("Congratulations!!! You have won.\r\n P.S. \r\nIt's just pure luck.");
                    return answer.ToString();
                }
                else
                {
                    var bankrupts = new List<IValuablePieceOfPaper>();
                    OurMarket.Fill(ref bankrupts, this);
                    bondList.Clear();
                        depositList.Clear();
                    stockList.Clear();
                    foreach (var el in OurMarket.MarketPapers)
                    {
                        if (Names.CountryNames().Contains(el.Name))
                        {
                            bondList.Add(el as Bond);
                        }
                        if (Names.BankNames().Contains(el.Name))
                        {
                            depositList.Add(el as Deposit);
                        }
                        if (Names.CompanyNames().Contains(el.Name))
                        {
                            stockList.Add(el as Stock);
                        }
                    }
                    InvestedMoney = 0;
                    for (int i = 0; i < ownings.Count; i++)
                    {
                        ownings[i].Renew(OurMarket);
                        if (ownings[i].Bankrupt && !OurMarket.MarketPapers.Contains(OurMarket.MarketPapers.Find(a => a.Name == Ownings[i].Name)))
                            bankrupts.Add(ownings[i]);
                        else if (ownings[i].Bankrupt && OurMarket.MarketPapers.Contains(OurMarket.MarketPapers.Find(a => a.Name == Ownings[i].Name)))
                            ownings[i].Bankrupt = false;
                        else
                            InvestedMoney += ownings[i].TotalValue;
                    }
                    var Bankrupts = new StringBuilder();
                    if (bankrupts.Count > 0)
                        Bankrupts.AppendFormat("New day - new bankrupts...");
                    else
                        Bankrupts.AppendFormat("What a beautiful day - not a single bankrupt!");
                    Bankrupts.AppendLine();
                    for (int i = 0; i < bankrupts.Count; i++)
                    {
                        ownings.Remove(Find(bankrupts[i].Name, ownings));
                        Bankrupts.AppendFormat("   {0} ", bankrupts[i].Name);
                        Bankrupts.AppendLine();
                        OurMarket.BankNames.Remove(new Name(bankrupts[i].Name, true));
                        OurMarket.MarketPapers.Remove(OurMarket.MarketPapers.Find(a => a.Name == bankrupts[i].Name));
                    }
                    return Bankrupts.ToString();
                }
        }
    }
}
