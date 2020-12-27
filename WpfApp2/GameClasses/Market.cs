﻿using System;
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
        public void RenewAll()
        {
            foreach (var p in MarketPapers)
                p.Renew(this);

        }
        public void Fill() // фабричный метод?
        {
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
                        MarketPapers.Add(new Stock(stock.Name));
                }
            }
            for (int i = 0; i < deplorables.Count; i++)
            {
                CompanyNames.Remove(new Name(deplorables[i], true));
                CompanyNames.Add(new Name(deplorables[i], false));
            }
            ChangeList(BankNames);
            ChangeList(CountryNames);
            Random random = new Random();
            while (MarketPapers.Count < 45)
            {
                int choice = random.Next(0, 2);
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
