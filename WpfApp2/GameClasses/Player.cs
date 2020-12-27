using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfApp2
{
    public static class Player
    {
        public const double StartingMoney = 1000000;
        private static double expences = 0;
        private static double goal = 0;
        public static List<IValuablePieceOfPaper> Ownings = new List<IValuablePieceOfPaper>();
        public static double Money { get; set; }
        public static double InvestedMoney { get; set; }
        public static int Difficulty { get; set; }
        public static int Turn = 0;
        public static Market OurMarket;

        public static void SetValues(string difficulty) // использовать вместо конструктора
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("random.bin", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                    Turn = (int)formatter.Deserialize(fs);
            }
            Turn++;
            using (FileStream fs = new FileStream("random.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Turn);
            }
            Money = StartingMoney;
            InvestedMoney = 0;
            switch (difficulty)
            {
                case "Easy":
                    Difficulty = 0;
                    expences = 5000;
                    goal = 1000000;
                    break;
                case "Normal":
                    Difficulty = 1;
                    expences = 10000;
                    goal = 3000000;
                    break;
                case "Hard":
                    Difficulty = 2;
                    expences = 15000;
                    goal = 5000000;
                    break;
                default:
                    break;
            }
            OurMarket = new Market();
            var l = new List<IValuablePieceOfPaper>();
            OurMarket.Fill(ref l);
        }
        public static void Buy(IValuablePieceOfPaper paper, double quantity)
        {
            paper.Quantity -= quantity;
            if (Money >= quantity * paper.Price)
            {
                Money -= quantity * paper.Price;
                InvestedMoney += quantity * paper.Price;
                if (!Ownings.Contains(Ownings.Find(a => a.Name == paper.Name)))
                    Ownings.Add(paper.CreateAPair(quantity)); //incapsulated
                else
                {
                    var paper0 = Ownings.Find(a => a.Name == paper.Name);
                    paper0.Quantity += quantity;
                    paper0.TotalValue = paper0.Price * paper0.Quantity;
                }

            }
        }
        public static void Sell(int i, double quantity) // changed
        {
            if (quantity == Ownings[i].Quantity)
            {
                Money += Ownings[i].TotalValue;
                InvestedMoney -= Ownings[i].TotalValue;
                OurMarket.MarketPapers.Add(Ownings[i]);
                Ownings.RemoveAt(i);
            }
            else
            {
                Ownings[i].Quantity -= quantity;
                Money += quantity * Ownings[i].Price;
                InvestedMoney -= quantity * Ownings[i].Price;
                OurMarket.MarketPapers.Add(Ownings[i].CreateAPair(quantity));
            }
        }
        public static void RenewAll() // End Turn Button
        {
            Money -= expences;
            if (Money < 0)
            {
                // W A S T E D
                Ownings.Clear();
                Money = -1000000000;
                InvestedMoney = -1000000000;
            }
            if (Money >= goal)
            {
                // V I C T O R Y
            }
            var bankrupts = new List<IValuablePieceOfPaper>();
            OurMarket.Fill(ref bankrupts);
            InvestedMoney = 0;
            for (int i = 0; i < Ownings.Count; i++)
            {
                Ownings[i].Renew(OurMarket);
                if (Ownings[i].Bankrupt && !OurMarket.MarketPapers.Contains(OurMarket.MarketPapers.Find(a => a.Name == Ownings[i].Name)))
                    bankrupts.Add(Ownings[i]);
                else if (Ownings[i].Bankrupt && OurMarket.MarketPapers.Contains(OurMarket.MarketPapers.Find(a => a.Name == Ownings[i].Name)))
                    Ownings[i].Bankrupt = false;
                else
                    InvestedMoney += Ownings[i].TotalValue;
            }
            for (int i = 0; i < bankrupts.Count; i++)
            {
                Ownings.Remove(Ownings.Find(a => a.Name == bankrupts[i].Name));
                OurMarket.BankNames.Remove(new Name(bankrupts[i].Name, true));
                OurMarket.MarketPapers.Remove(OurMarket.MarketPapers.Find(a => a.Name == bankrupts[i].Name));
            }
        }
    }
}
