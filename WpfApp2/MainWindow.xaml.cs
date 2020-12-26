﻿using System;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player correntPlayer { get; set; }
        public IEnumerable<IValuablePieceOfPaper> Ownings { get; }
        public MainWindow(Player player)
        {
            correntPlayer = player;
            Ownings = player.Ownings;
            InitializeComponent();

            correntPlayer = player;
            StringBuilder style = new StringBuilder(correntPlayer.Difficulty);
            switch (style.ToString())
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

        private void SellClick(object sender, RoutedEventArgs e)
        {

        }

        private void InfoClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
