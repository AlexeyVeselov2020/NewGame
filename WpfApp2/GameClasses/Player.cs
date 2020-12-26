using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Player
    {
        public const double StartingMoney = 1000000;
        public string Name { get; set; }
        public List<IValuablePieceOfPaper> Ownings = new List<IValuablePieceOfPaper>();
        public double Money { get; set; }
        public double InvestedMoney { get; set; }
        public Market OurMarket;
        public string Difficulty { get; set; }
        public Player(string name,string difficulty)
        {
            Name = name;
            Money = StartingMoney;
            InvestedMoney = 0;
            OurMarket = new Market();
            OurMarket.Fill(this);
            Difficulty = difficulty;
            foreach (var comp in Names.CompanyNames())
                Ownings.Add(new Stock(comp));//чтобы посмотреть как выглядит 
        }
        public void Buy(IValuablePieceOfPaper paper, double quantity)
        {
            if (Money <= quantity * paper.Price)
            {
                Money -= quantity * paper.Price;
                InvestedMoney += quantity * paper.Price;
                if (paper is Stock)
                {
                    Ownings.Add(new Stock(paper.Name, quantity, paper.Price));
                }
                if (paper is Deposit)
                {
                    var newDeposit = paper as Deposit;
                    Ownings.Add(new Deposit(newDeposit.Name, quantity, newDeposit.BankruptcyProbability));
                }
                if (paper is Bond)
                {
                    var newBond = paper as Bond;
                    Ownings.Add(new Bond(newBond.Name, quantity, newBond.Percent));
                }
            }
        }
        public void Sell(int i)
        {
            Money += Ownings[i].TotalValue;
            InvestedMoney -= Ownings[i].TotalValue;
            Ownings.RemoveAt(i);
        }
        public void RenewAll()
        {
            OurMarket.Fill(this);
            InvestedMoney = 0;
            for (int i = 0; i < Ownings.Count; i++)
            {
                Ownings[i].Renew(OurMarket);
                if (Ownings[i].Bankrupt)
                {
                    Ownings.RemoveAt(i);
                    OurMarket.BankNames.Remove(Ownings[i].Name); // обанкротившийся банк больше не появится в игре
                    i--;
                }
                else
                    InvestedMoney += Ownings[i].TotalValue;
            }
        }
    }
}
