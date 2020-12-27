using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Deposit : IValuablePieceOfPaper
    {
        private const double maxQuantity = 1000000;
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

        public Deposit(Market market)
        {
            Quantity = TotalValue = 0;
            Price = 1;
            Bankrupt = false;
            var random = new Random();
            int index = 0;
            do index = random.Next(0, market.BankNames.Count - 1); while (market.BankNames[index].isTaken);
            Name = market.BankNames[index].Value;
            market.BankNames.RemoveAt(index);
            market.BankNames.Add(new Name(Name, true));
            BankruptcyProbability = random.Next((int)minBankruptcyProbability, (int)maxBankruptcyProbability);
            Percent = MinPercent + BankruptcyProbability * 3;
            if (Player.Difficulty == 0)
                minBankruptcyProbability = 1;
            if (Player.Difficulty == 1)
                minBankruptcyProbability = 3;
            if (Player.Difficulty == 2)
                minBankruptcyProbability = 5;
        }
        public Deposit(string name, double quantity, double bp)
        {
            Quantity = TotalValue = quantity;
            Price = 1;
            Name = name;
            Bankrupt = false;
            BankruptcyProbability = bp;
            Percent = MinPercent + BankruptcyProbability * 3;
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
            Quantity = Quantity * ((100 + Percent) / 100);
            TotalValue = Quantity;
            var random = new Random();
            int result = random.Next(1, 100);
            if (result <= BankruptcyProbability)
            {
                Bankrupt = true;
            }
        }
    }
}
