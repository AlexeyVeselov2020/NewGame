using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Market
    {
        public List<IValuablePieceOfPaper> MarketPapers = new List<IValuablePieceOfPaper>();
        public List<string> BankNames = Names.BankNames();
        public List<string> CompanyNames = Names.CompanyNames();
        public List<string> CountryNames = Names.CountryNames();
        public void RenewAll()
        {
            foreach(var p in MarketPapers)
                p.Renew(this);            
        }
        public void Fill(Player player)
        {
            MarketPapers.Clear();           
            foreach (var p in player.Ownings)
            {
                if (p is Stock)
                    MarketPapers.Add(new Stock(p.Name));
            }
            var random = new Random();
            var numbers = new List<int>();
            while(numbers.Count < 15)
            {
                int number = random.Next(0, BankNames.Count - 1);
                if (!numbers.Contains(number))
                    numbers.Add(number);
            }
            for (int i = 0; i < 15 - MarketPapers.Count; i++)
            {
                MarketPapers.Add(new Stock(CompanyNames[numbers[i]]));
            }
            foreach (var n in numbers)
            {
                MarketPapers.Add(new Deposit(BankNames[n]));
            }
            numbers.Clear();
            while (numbers.Count < 15)
            {
                int number = random.Next(0, CountryNames.Count - 1);
                if (!numbers.Contains(number))
                    numbers.Add(number);
            }
            foreach (var n in numbers)
            {
                MarketPapers.Add(new Bond(CountryNames[n]));
            }
        }
    }
}
