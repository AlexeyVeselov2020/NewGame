using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Bond : IValuablePieceOfPaper
    {
        public const double MaxQuantity = 1000000;
        public const double MinPercent = 1;
        public const double MaxPercent = 7;
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double TotalValue { get; set; }
        public double Percent { get; set; }
        public bool Bankrupt { get; set; }

        public Bond(string name)
        {
            Quantity = TotalValue = 0;
            Price = 1;
            Name = name;
            Bankrupt = false;
            var random = new Random();
            Percent = random.Next((int)MinPercent, (int)MaxPercent);
        }
        public Bond(string name,double quantity, double percent)
        {
            Quantity = TotalValue = quantity;
            Price = 1;
            Name = name;
            Bankrupt = false;
            Percent = percent;
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Percent);
        }
        public void Renew(Market market)
        {
            Quantity = Quantity * ((100 + Percent) / 100);
            TotalValue = Quantity;
        }
    }
}
