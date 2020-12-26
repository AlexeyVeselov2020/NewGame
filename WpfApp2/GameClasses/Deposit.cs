using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Deposit : IValuablePieceOfPaper
    {
        public const double MaxQuantity = 1000000;
        public const double MinBankruptcyProbability = 1;
        public const double MaxBankruptcyProbability = 10;
        public const double MinPercent = 5;
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double TotalValue { get; set; }
        public double Percent { get; set; }
        public double BankruptcyProbability { get; set; }
        public bool Bankrupt { get; set; }

        public Deposit(string name)
        {
            Quantity = TotalValue = 0;
            Price = 1;
            Name = name;
            Bankrupt = false;
            var random = new Random();
            BankruptcyProbability = random.Next((int)MinBankruptcyProbability, (int)MaxBankruptcyProbability);
            Percent = MinPercent + BankruptcyProbability * 3;
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
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Name, Percent, BankruptcyProbability);
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
