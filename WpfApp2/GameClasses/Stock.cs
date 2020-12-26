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
    }

    public class Stock : IValuablePieceOfPaper
    {
        const double MinQuantity = 100;
        const double MaxQuantity = 100000;
        const double Multiplication = 10000000;
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double TotalValue { get; set; }
        public bool Bankrupt { get; set; }
        public Stock(string name)
        {
            var random = new Random();
            Quantity = random.Next((int)MinQuantity, (int)MaxQuantity);
            Price = Multiplication / Quantity;
            TotalValue = Quantity * Price;
            Name = name;
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
        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
        public void Renew(Market market)
        {
            if (market.MarketPapers.Contains(this))
            {
                var random = new Random();
                Quantity = random.Next((int)MinQuantity, (int)MaxQuantity);
                Price = Multiplication / Quantity;
                TotalValue = Quantity * Price;
            }
            else
                SearchForMarketPrice(market);
        }
        private void SearchForMarketPrice(Market market)
        {
            foreach(var s in market.MarketPapers)
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
