using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public interface IValuablePieceOfPaper
    {
        double Quantity { get; set; }
        double Price { get; set; }
        double TotalValue { get; set; }
        string Name { get; set; }
        string ToString();
        void Renew(Market market);

        bool Bankrupt { get; set; }
        IValuablePieceOfPaper CreateAPair(double quantity);
    }

    public class Stock : IValuablePieceOfPaper
    {
        private static double minQuantity = 500;
        private static double maxQuantity = 10000;
        const double Multiplication = 10000000;
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double TotalValue { get; set; }
        public bool Bankrupt { get; set; }
        public Stock(Market market)
        {
            var random = new Random();
            int index = 0;
            do index = random.Next(0, market.CompanyNames.Count - 1); while (market.CompanyNames[index].isTaken);
            Name = market.CompanyNames[index].Value;
            market.CompanyNames.RemoveAt(index);
            market.CompanyNames.Add(new Name(Name, true));
            Quantity = random.Next((int)minQuantity, (int)maxQuantity);
            Price = Multiplication / Quantity;
            TotalValue = Quantity * Price;
            Bankrupt = false;
            if (Player.Difficulty == 0)
            {
                minQuantity = 500;
                maxQuantity = 50000; // MAX = 20k, MIN = 200
            }
            if (Player.Difficulty == 1)
            {
                minQuantity = 500;
                maxQuantity = 10000; // MAX = 20k, MIN = 1k
            }
            if (Player.Difficulty == 2)
            {
                minQuantity = 1000;
                maxQuantity = 10000; // MAX = 10k, MIN = 1k
            }
        }
        public Stock(string name)
        {
            var random = new Random();
            Name = name;
            Quantity = random.Next((int)minQuantity, (int)maxQuantity);
            Price = Multiplication / Quantity;
            TotalValue = Quantity * Price;
            Bankrupt = false;
        }
        public Stock(string name, double quantity, double price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            TotalValue = Quantity * Price;
            Bankrupt = false;
        }
        public IValuablePieceOfPaper CreateAPair(double quantity)
        {
            return new Stock(Name, quantity, Price);
        }
        public override string ToString()
        {
            return Name; // изменил
        }
        public void Renew(Market market)
        {
            if (market.MarketPapers.Contains(this))
            {
                var random = new Random();
                Quantity = random.Next((int)minQuantity, (int)maxQuantity);
                Price = Multiplication / Quantity;
                TotalValue = Quantity * Price;
            }
            else
                SearchForMarketPrice(market);
        }
        private void SearchForMarketPrice(Market market)
        {
            foreach (var s in market.MarketPapers)
            {
                if (s.Name == Name)
                {
                    Price = s.Price;
                    break;
                }
            }
            TotalValue = Quantity * Price;
        }
    }
}
