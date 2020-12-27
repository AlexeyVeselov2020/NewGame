using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public static class Player
    {
        public const double StartingMoney = 1000000;
        private static double expences = 0;
        private static double goal = 0;
        private static string Name { get; set; }
        public static List<IValuablePieceOfPaper> Ownings = new List<IValuablePieceOfPaper>();
        public static double Money { get; set; }
        public static double InvestedMoney { get; set; }
        public static int Difficulty { get; set; }
        public static Market OurMarket;

        public static void SetValues(string name,string difficulty) // использовать вместо конструктора
        {
            Name = name;
            Money = StartingMoney;
            InvestedMoney = 0;
            switch(difficulty)
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
            OurMarket.Fill();
        }
        public static void Buy(IValuablePieceOfPaper paper, double quantity)
        {
            if (Money <= quantity * paper.Price)
            {
                Money -= quantity * paper.Price;
                InvestedMoney += quantity * paper.Price;
                Ownings.Add(paper.CreateAPair(quantity)); //incapsulated
            }
        }
        public static void Sell(int i, double quantity) // changed
        {
            if (quantity == Ownings[i].Quantity)
            {
                Money += Ownings[i].TotalValue;
                InvestedMoney -= Ownings[i].TotalValue;
                Ownings.RemoveAt(i);
            }
            else
            {
                Ownings[i].Quantity -= quantity;
                Money += quantity * Ownings[i].Price;
                InvestedMoney -= quantity * Ownings[i].Price;
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
            if (Money > goal)
            {
                // V I C T O R Y
            }
            var bankrupts = new List<IValuablePieceOfPaper>();
            OurMarket.Fill();
            InvestedMoney = 0;
            for (int i = 0; i < Ownings.Count; i++)
            {
                Ownings[i].Renew(OurMarket);
                if (Ownings[i].Bankrupt)
                    bankrupts.Add(Ownings[i]);
                else
                    InvestedMoney += Ownings[i].TotalValue;
            }
            for (int i = 0; i < bankrupts.Count; i++)
            {
                Ownings.Remove(bankrupts[i]);
                OurMarket.BankNames.Remove(new Name(bankrupts[i].Name, true));
                var bank = OurMarket.MarketPapers.Find(a => a.Name == bankrupts[i].Name);
                if (bank != null) OurMarket.MarketPapers.Remove(bank);
            }
        }
    }
}
