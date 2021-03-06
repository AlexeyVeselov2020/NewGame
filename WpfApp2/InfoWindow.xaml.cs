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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public IValuablePieceOfPaper correntPaper { get; set; }
        public MainWindow previousWindow { get; set; }
        public InfoWindow(IValuablePieceOfPaper paper, MainWindow window)
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(InfoWindow_Closing);
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
                typeBlock.Text = "stock";
                quantityBlock.Text = "Quantity of stocks: ";
                quantityBox.Text = paper.Quantity.ToString();
            }
            else if (Names.BankNames().Contains(paper.Name))
            {
                Deposit deposit = paper as Deposit;
                percentorpriceBlock.Text = "Interest rate: ";
                typeBlock.Text = "deposit";
                percentorpriceBox.Text = (new StringBuilder(deposit.Percent.ToString() + "%").ToString());
                percentofbacruptPanel.Visibility = Visibility.Visible;
                percentofbancruptBox.Text = (new StringBuilder(deposit.BankruptcyProbability.ToString() + "%").ToString());
                quantityBlock.Text = "Quantity of invested funds: ";
                quantityBox.Text = deposit.Quantity.ToString();
            }
            else if (Names.CountryNames().Contains(paper.Name))
            {
                Bond bond = paper as Bond;
                percentorpriceBlock.Text = "Interest rate: ";
                typeBlock.Text = "bond";
                percentorpriceBox.Text = (new StringBuilder(bond.Percent.ToString() + "%").ToString());
                quantityBlock.Text = "Quantity of invested funds: ";
                quantityBox.Text = bond.Quantity.ToString();
            }
            double total = paper.Price * paper.Quantity;
            totalBox.Text = total.ToString();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            previousWindow.IsEnabled = true;
        }

        private void InfoWindow_Closing(object sender, EventArgs e)
        {
            previousWindow.IsEnabled = true;
        }
    }
}

