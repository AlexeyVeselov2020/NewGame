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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            List<string> styles = new List<string> { "Easy", "Normal", "Hard" };
            complexityBox.ItemsSource = styles;
            complexityBox.SelectionChanged += ThemeChange;
        }
        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            StringBuilder style = new StringBuilder(complexityBox.SelectedItem as string);
            switch(style.ToString())
            {
                case "Normal":
                    IB.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Themes/NormalTheme.jpg"));
                    break;
                case "Easy":
                    IB.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Themes/EasyTheme.jpg"));
                    break;
                case "Hard":
                    IB.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Themes/HardTheme.jpg"));
                    break;
                default:
                    break;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
                if(complexityBox.SelectedItem!=null)
                {
                    var p=new Player(complexityBox.SelectedItem.ToString());
                    var taskWindow = new MainWindow(p);
                    this.Close();
                    taskWindow.Show();
                }
                else
                    MessageBox.Show("Вы не выбрали сложность.");
        }

        private void HintButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome to Financier Simulator — the game where you can manage your own fund and become a true tycoon!\r\n Rules:\r\nYou start the game with 500000$. You can buy and sell 3 types of securities: stocks, bonds and deposits. Stocks are randomly changed in price each turn. Bonds and deposits increase the amount of money you invest there by a particular percent. Deposits have better percents but there's always a probability of the bank becoming bankrupt. If it happens you will lose all the money you have invested there.\r\nEvery turn you will have the same sum of expenses. If you don't have enough uninvested money to pay — you lose.\r\nThere're 3 complexity levels:\r\nEasy\r\n- Deposits and bonds have high interest rates, stocks can grow in price almost unlimitedly\r\n- The sum of expenses is 25000$\r\n- The victory goal is 1000000$\r\nMedium\r\n- Deposits and bonds have good interest rates, stocks can grow in price substantially.\r\n- The sum of expenses is 50000$\r\n- The victory goal is 3000000$\r\nHard\r\n- Deposits and bonds have low interest rates, banks are likely to go bankrupt, stocks can grow in price.\r\n- The sum of expenses is 75000$\r\n- The victory goal is 5000000$");
        }
    }
}
