using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Bond : IValuablePieceOfPaper
    {
        private const double maxQuantity = 1000000;
        private const double minPercent = 1;
        private static double maxPercent = 7;
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double TotalValue { get; set; }
        public double Percent { get; set; }
        public bool Bankrupt { get; set; }

        public Bond(Market market)
        {
            Quantity = TotalValue = 0;
            Price = 1;
            Bankrupt = false;
            var random = new Random();
            int index = 0;
            do index = random.Next(0, market.CountryNames.Count - 1); while (market.CountryNames[index].isTaken);
            Name = market.CountryNames[index].Value;
            market.CountryNames.RemoveAt(index);
            market.CountryNames.Add(new Name(Name, true));
            Percent = random.Next((int)minPercent, (int)maxPercent);
            if (Player.Difficulty == 0)
                maxPercent = 7;
            if (Player.Difficulty == 1)
                maxPercent = 6;
            if (Player.Difficulty == 2)
                maxPercent = 4;
        }
        public Bond(string name, double quantity, double percent)
        {
            Quantity = TotalValue = quantity;
            Price = 1;
            Name = name;
            Bankrupt = false;
            Percent = percent;
        }
        public IValuablePieceOfPaper CreateAPair(double quantity)
        {
            return new Bond(Name, quantity, Percent);
        }
        public override string ToString()
        {
            return Name; // изменил
        }
        public void Renew(Market market)
        {
            Quantity = Quantity * ((100 + Percent) / 100);
            TotalValue = Quantity;
        }
    }
}
