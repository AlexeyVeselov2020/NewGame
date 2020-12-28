using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for SellBuyWindow.xaml
    /// </summary>
    public partial class BuyWindow : Window
    {
        public IValuablePieceOfPaper correntPaper { get; set; }
        public MainWindow previousWindow { get; set; }
        public BuyWindow(IValuablePieceOfPaper paper, MainWindow window)
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(SellBuyWindow_Closing);
            correntPaper = paper;
            previousWindow = window;
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
            textblockName.Text = paper.Name;
            if(Names.CompanyNames().Contains(paper.Name))
            {
                percentorpriceBlock.Text = "Price: ";
                percentorpriceBox.Text = paper.Price.ToString();
                quantityBlock.Text = "Maximum quantity of stocks: ";
                quantityBox.Text = Math.Floor(Math.Min(paper.Quantity,((previousWindow as MainWindow).player.Money/paper.Price))).ToString();
                quantitytobuyBlock.Text = "Quantity to buy: ";
            }
            else if(Names.BankNames().Contains(paper.Name))
            {
                Deposit deposit = paper as Deposit;
                percentorpriceBlock.Text = "Interest rate: ";
                percentorpriceBox.Text = (new StringBuilder(deposit.Percent.ToString() + "%").ToString());
                percentofbacruptPanel.Visibility = Visibility.Visible;
                percentofbancruptBox.Text= (new StringBuilder(deposit.BankruptcyProbability.ToString() + "%").ToString());
                quantityBlock.Text = "Maximum quantity of invested funds: ";
                quantityBox.Text = Math.Floor(Math.Min(deposit.MaxQuantity, previousWindow.player.Money)).ToString();
                quantitytobuyBlock.Text = "Funds for investment: ";
            }
            else if (Names.CountryNames().Contains(paper.Name))
            {
                Bond bond = paper as Bond;
                percentorpriceBlock.Text = "Interest rate: ";
                percentorpriceBox.Text = (new StringBuilder(bond.Percent.ToString() + "%").ToString());
                quantityBlock.Text = "Maximum quantity of invested funds: ";
                quantityBox.Text = Math.Floor(Math.Min(bond.MaxQuantity, previousWindow.player.Money)).ToString();
                quantitytobuyBlock.Text = "Funds for investment: ";
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            previousWindow.IsEnabled = true;
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            string text = quantitytobuyBox.Text;
            Regex r = new Regex(@"[\d]+");
            if (r.Match(text).Success)
            {
                var quantitytobuy = double.Parse(quantitytobuyBox.Text);
                if (quantitytobuy <= double.Parse(quantityBox.Text))
                {
                    previousWindow.player.Buy(correntPaper, quantitytobuy);
                    this.Close();
                    previousWindow.IsEnabled = true;
                }
                else
                    MessageBox.Show("You don't have enough funds.");
            }
            else
                MessageBox.Show("You entered an incorrect quantity value");
        }

        private void SellBuyWindow_Closing(object sender, EventArgs e)
        {
            previousWindow.IsEnabled = true;
        }
    }
}
