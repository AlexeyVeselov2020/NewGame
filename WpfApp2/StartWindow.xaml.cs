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
            if(nameBox.Text!="")
            {
                if(complexityBox.SelectedItem!=null)
                {
                    Player.SetValues(nameBox.Text, complexityBox.SelectedItem.ToString());
                    var taskWindow = new MainWindow();
                    this.Close();
                    taskWindow.Show();
                }
                else
                    MessageBox.Show("Вы не выбрали сложность.");
            }
            else
                MessageBox.Show("Вы не ввели имя.");
        }
    }
}
