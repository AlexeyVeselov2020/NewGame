using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfApp2
{
    public class Bond : IValuablePieceOfPaper,INotifyPropertyChanged
    {
        public double MaxQuantity { get; set; }
        private static int amount = 0;
        private const double minPercent = 1;
        private static double maxPercent = 7;
        private double change;

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double TotalValue { get; set; }
        public double Percent { get; set; }
        public bool Bankrupt { get; set; }
        public double Change
        {
            get { return change; }
            set
            {
                change = value;
                RaisePropertyChanged("Change");
            }
        }

        public Bond(Market market)
        {
            amount++;
            DifficultyOptions();
            Quantity = TotalValue = 0;
            Price = 1;
            Bankrupt = false;
            var random = new Random(Player.Random + 6000 + amount);
            int index = 0;
            do index = random.Next(0, market.CountryNames.Count - 1); while (market.CountryNames[index].isTaken);
            Name = market.CountryNames[index].Value;
            market.CountryNames.RemoveAt(index);
            market.CountryNames.Add(new Name(Name, true));
            Percent = random.Next((int)minPercent, (int)maxPercent);

        }
        private void DifficultyOptions()
        {
            if (Player.Difficulty == 0)
            {
                maxPercent = 7;
                MaxQuantity = 1000000;
            }
            if (Player.Difficulty == 1)
            {
                maxPercent = 6;
                MaxQuantity = 700000;
            }
            if (Player.Difficulty == 2)
            {
                maxPercent = 4;
                MaxQuantity = 300000;
            }
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
            double lastQuantity = Quantity;
            Quantity = Math.Round(Quantity * ((100 + Percent) / 100),2);
            Change = Math.Round(Quantity - lastQuantity,2);
            TotalValue = Quantity;
        }
    }
}
