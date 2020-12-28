using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Deposit : IValuablePieceOfPaper
    {
        private static int amount;
        public double MaxQuantity { get; set; }
        private static double minBankruptcyProbability = 1;
        private const double maxBankruptcyProbability = 10;
        public const double MinPercent = 5;
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double TotalValue { get; set; }
        public double Percent { get; set; }
        public double BankruptcyProbability { get; set; }
        public bool Bankrupt { get; set; }
        public double Change { get; set; }

        public Deposit(Market market)
        {
            amount++;
            Quantity = TotalValue = 0;
            Price = 1;
            Bankrupt = false;
            var random = new Random(Player.Random + market.MarketPapers.Count + market.MarketPapers.Count + amount);
            int index = 0;
            do index = random.Next(0, market.BankNames.Count - 1); while (market.BankNames[index].isTaken);
            Name = market.BankNames[index].Value;
            market.BankNames.RemoveAt(index);
            market.BankNames.Add(new Name(Name, true));
            DifficultyOptions();
            BankruptcyProbability = random.Next((int)minBankruptcyProbability, (int)maxBankruptcyProbability);
            Percent = PercentCount(BankruptcyProbability);

        }
        private void DifficultyOptions()
        {
            if (Player.Difficulty == 0)
            {
                minBankruptcyProbability = 1;
                MaxQuantity = 1000000;
            }
            if (Player.Difficulty == 1)
            {
                minBankruptcyProbability = 3;
                MaxQuantity = 700000;
            }
            if (Player.Difficulty == 2)
            {
                minBankruptcyProbability = 5;
                MaxQuantity = 300000;
            }
        }
        public Deposit(string name, double quantity, double bp)
        {
            Quantity = TotalValue = quantity;
            Price = 1;
            Name = name;
            Bankrupt = false;
            BankruptcyProbability = bp;
            Percent = PercentCount(BankruptcyProbability);
        }
        private double PercentCount(double bp)
        {
            if (Player.Difficulty == 0)
                return MinPercent + bp * 3;
            else if (Player.Difficulty == 1)
                return MinPercent + bp * 2;
            else if (Player.Difficulty == 2)
                return MinPercent + bp;
            else
                return 0;
        }
        public IValuablePieceOfPaper CreateAPair(double quantity)
        {
            return new Deposit(Name, quantity, BankruptcyProbability);
        }
        public override string ToString()
        {
            return Name; // изменил
        }
        public void Renew(Market market)
        {
            amount++;
            double lastQuantity = Quantity;
            Quantity = Quantity * ((100 + Percent) / 100);
            Change = Quantity - lastQuantity;
            TotalValue = Quantity;
            var random = new Random(Player.Random + 1000 + market.MarketPapers.Count + amount);
            int result = random.Next(1, 100);
            if (result <= BankruptcyProbability)
            {
                Bankrupt = true;
            }
        }
    }
}
