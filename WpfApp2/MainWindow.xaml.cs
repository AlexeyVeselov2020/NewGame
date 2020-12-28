using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IEnumerable<IValuablePieceOfPaper> StockMarket { get; }
        public IEnumerable<IValuablePieceOfPaper> BondMarket { get; }
        public IEnumerable<IValuablePieceOfPaper> DepositMarket { get; }
        public Player player { get; set; }
        public MainWindow(Player p)
        {
            player = p;
            //Money = Player.Money;
            //Goal = Player.Goal;
            StockMarket = Player.OurMarket.MarketPapers.Where(c=>Names.CompanyNames().Contains(c.Name)==true);
            BondMarket = Player.OurMarket.MarketPapers.Where(c => Names.CountryNames().Contains(c.Name) == true);
            DepositMarket = Player.OurMarket.MarketPapers.Where(c => Names.BankNames().Contains(c.Name) == true);
            InitializeComponent();
            DataContext = player;
            switch (Player.Difficulty)
            {
                case 1:
                    IB.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Themes/NormalTheme.jpg"));
                    break;
                case 0:
                    IB.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Themes/EasyTheme.jpg"));
                    break;
                case 2:
                    IB.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Themes/HardTheme.jpg"));
                    break;
                default:
                    break;
            }
        }

        private void SellClick(object sender, RoutedEventArgs e)
        {

        }

        private void InfoClick(object sender, RoutedEventArgs e)
        {

        }

        private void BuyClick(object sender, RoutedEventArgs e)
        {
            var item = (IValuablePieceOfPaper)(sender as Button).DataContext;
            var BuyWindow = new SellBuyWindow(item,this,0);
            this.IsEnabled=false;
            BuyWindow.Show();
        }

        private void EndturnButton_Click(object sender, RoutedEventArgs e)
        {
            player.RenewAll();
        }
    }
}
