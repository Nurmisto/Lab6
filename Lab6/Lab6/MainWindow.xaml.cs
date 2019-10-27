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

namespace Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool simulationStarted = false;
        public double BarOpenTime { get; set; }
        public List<string> PatronNameList = new List<string>() { "Alexander", "Anders", "Andreas", "Andreé", "Andreea", "Charlotte", "Daniel", "Elvis", "Emil", "FredrikÄrAldrigHär", "Johan",
                                                                "John", "Jonas", "Karo", "Khosro", "Luna", "Marcus", "Nicklas", "Nils", "Petter", "Pontus", "Robin", "Simon", "Sofia", "Tijana", 
                                                                "Tommy", "Toni", "Wilhelm"};
        public MainWindow()
        {
            
            InitializeComponent();
            
        }

      
        private void bartenderPausOrContiniueButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void waitressPausOrContiniueButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void patronsPausOrContiniueButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void openOrCloseThePub_Click(object sender, RoutedEventArgs e)
        {

        }

        private void shutdownAllThreads_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
