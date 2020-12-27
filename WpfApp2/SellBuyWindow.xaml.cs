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
    public partial class SellBuyWindow : Window
    {
        public IValuablePieceOfPaper correntPaper { get; set; }
        public SellBuyWindow(IValuablePieceOfPaper paper)
        {
            InitializeComponent();
            correntPaper = paper;
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+$");
            if (int.Parse(e.Text) > 0 && int.Parse(e.Text)<=correntPaper.Quantity)
                e.Handled = regex.IsMatch(e.Text);
        }
    }
}
