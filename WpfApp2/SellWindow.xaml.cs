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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for SellWindow.xaml
    /// </summary>
    public partial class SellWindow : Window
    {
        public IValuablePieceOfPaper correntPaper { get; set; }
        public MainWindow previousWindow { get; set; }
        public SellWindow(IValuablePieceOfPaper paper, MainWindow window)
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
            if (Names.CompanyNames().Contains(paper.Name))
            {
                percentorpriceBlock.Text = "Price: ";
                percentorpriceBox.Text = paper.Price.ToString();
                quantityBlock.Text = "Number of your stocks: ";
                quantityBox.Text = paper.Quantity.ToString();
                quantitytobuyBlock.Text = "Quantity to sell: ";
            }
            else if (Names.BankNames().Contains(paper.Name))
            {
                Deposit deposit = paper as Deposit;
                percentorpriceBlock.Text = "Interest rate: ";
                percentorpriceBox.Text = (new StringBuilder(deposit.Percent.ToString() + "%").ToString());
                percentofbacruptPanel.Visibility = Visibility.Visible;
                percentofbancruptBox.Text = (new StringBuilder(deposit.BankruptcyProbability.ToString() + "%").ToString());
                quantityBlock.Text = "Your intested funds: ";
                quantityBox.Text = deposit.Quantity.ToString();
                quantitytobuyBlock.Text = "Deposit for sell: ";
            }
            else if (Names.CountryNames().Contains(paper.Name))
            {
                Bond bond = paper as Bond;
                percentorpriceBlock.Text = "Interest rate: ";
                percentorpriceBox.Text = (new StringBuilder(bond.Percent.ToString() + "%").ToString());
                quantityBlock.Text = "Your intested funds: ";
                quantityBox.Text = bond.Quantity.ToString();
                quantitytobuyBlock.Text = "Bond for sell: ";
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

        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            var quantitytosell = double.Parse(quantitytobuyBox.Text);
            if (quantitytosell <= double.Parse(quantityBox.Text))
            {
                previousWindow.player.Sell(correntPaper, quantitytosell);
                this.Close();
                previousWindow.IsEnabled = true;
            }
            else
                MessageBox.Show("You don't have enough securities.");
        }

        private void SellBuyWindow_Closing(object sender, EventArgs e)
        {
            previousWindow.IsEnabled = true;
        }
    }
}
