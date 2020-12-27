using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public struct Name
    {
        public string Value;
        public bool isTaken;
        public Name(string s, bool b)
        {
            Value = s;
            isTaken = b;
        }
    }
    public class Market
    {
        public List<IValuablePieceOfPaper> MarketPapers = new List<IValuablePieceOfPaper>();
        public List<Name> BankNames = new List<Name>();
        public List<Name> CompanyNames = new List<Name>();
        public List<Name> CountryNames = new List<Name>();
        public Market()
        {
            var helpList = Names.BankNames();

            for (int i = 0; i < helpList.Count; i++)
                BankNames.Add(new Name(helpList[i], false));
            helpList = Names.CompanyNames();

            for (int i = 0; i < helpList.Count; i++)
                CompanyNames.Add(new Name(helpList[i], false));
            helpList = Names.CountryNames();

            for (int i = 0; i < helpList.Count; i++)
                CountryNames.Add(new Name(helpList[i], false));
        }
        public void RenewAll(ref List<IValuablePieceOfPaper> bankrupts)
        {
            foreach (var p in MarketPapers)
            {
                p.Renew(this);
                if (p.Bankrupt)
                    bankrupts.Add(p);
            }

        }
        public void Fill(ref List<IValuablePieceOfPaper> bankrupts) // фабричный метод?
        {
            RenewAll(ref bankrupts);
            MarketPapers.Clear();
            var deplorables = new List<string>();
            for (int i = 0; i < CompanyNames.Count; i++)
            {
                if (CompanyNames[i].isTaken)
                {
                    var stock = Player.Ownings.Find(a => a.Name == CompanyNames[i].Value);
                    if (stock == null)
                        deplorables.Add(CompanyNames[i].Value);
                    else
                        MarketPapers.Add(new Stock(stock.Name, this));
                }
            }
            for (int i = 0; i < deplorables.Count; i++)
            {
                CompanyNames.Remove(new Name(deplorables[i], true));
                CompanyNames.Add(new Name(deplorables[i], false));
            }
            ChangeList(BankNames);
            ChangeList(CountryNames);
            Random random = new Random(Player.Turn + 5000 + MarketPapers.Count + (int)Player.InvestedMoney);
            while (MarketPapers.Count < 45)
            {
                int choice = random.Next(0, 3);
                if (choice == 0)
                    MarketPapers.Add(new Stock(this));
                if (choice == 1)
                    MarketPapers.Add(new Deposit(this));
                if (choice == 2)
                    MarketPapers.Add(new Bond(this));
            }
        }
        private void ChangeList(List<Name> list)
        {
            var deplorables = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].isTaken)
                {
                    deplorables.Add(list[i].Value);
                }
            }
            for (int i = 0; i < deplorables.Count; i++)
            {
                list.Remove(new Name(deplorables[i], true));
                list.Add(new Name(deplorables[i], false));
            }
        }
    }
}
