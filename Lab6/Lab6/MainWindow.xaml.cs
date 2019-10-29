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
        public int numberOfPatronsInBar = 0;
        public int numberOfGlass = 8;
        public int numberOfSeats = 9;
        public double TimeUntillBarCloses;
        public bool couplesNight;
        public DateTime DateTimerStart;
        
        public double BarOpenTime { get; set; }
        public List<string> PatronNameList = new List<string>() { "Alexander", "Anders", "Andreas", "Andreé", "Andreea", "Charlotte", "Daniel", "Elvis", "Emil", "FredrikÄrAldrigHär", "Johan",
                                                                "John", "Jonas", "Karo", "Khosro", "Luna", "Marcus", "Nicklas", "Nils", "Petter", "Pontus", "Robin", "Simon", "Sofia", "Tijana", 
                                                                "Tommy", "Toni", "Wilhelm"};
        public MainWindow()
        {
            
            InitializeComponent();
            TimeUntillBarCloses = sliderValue.Value;
            Console.WriteLine(TimeUntillBarCloses);

            NumberOfPatronsInBarLabel.Content = $"Total guests in bar: {numberOfPatronsInBar}";
            NumberOfGlasOnShelfLabel.Content = $"Total glasses on shelf: {numberOfGlass}";
            NumberOfVacantSeatsLabel.Content = $"Total chairs available: {numberOfSeats}";

            PausOrContinueButtonsEnabled(false);
        }
        public void PausOrContinueButtonsEnabled(bool enabled)
        {
            if (enabled)
            {
                bartenderPausOrContinueButton.IsEnabled = true;
                waitressPausOrContinueButton.IsEnabled = true;
                patronsPausOrContinueButton.IsEnabled = true;
            }
            else if (!enabled)
            {
                bartenderPausOrContinueButton.IsEnabled = false;
                waitressPausOrContinueButton.IsEnabled = false;
                patronsPausOrContinueButton.IsEnabled = false;
            }
        }


        public void StartSimulation()
        {
            simulationStarted = true;
            if (couplesNight)
            {

            }
            else
            {
                
            }
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

        private void OpenOrCloseThePub_Click(object sender, RoutedEventArgs e)
        {
            if (!simulationStarted)
            {
                UIOnBarOpen();
                StartSimulation();
                PausOrContinueButtonsEnabled(true);

            }
            else
            {
                OpenOrCloseThePub.IsEnabled = false;
                OpenOrCloseThePub.Content = "Closing the bar and going home..";
                PausOrContinueButtonsEnabled(false);
                TimeUntillBarCloses = 0;
                UIOnBarClosed();
            }

        }

        private void shutdownAllThreads_Click(object sender, RoutedEventArgs e)
        {

        }

        void UIOnBarOpen()
        {
            OpenOrCloseThePub.Content = "Close the bar";
            SliderValueTextBox.IsEnabled = false;
            sliderValue.IsEnabled = false;
        }

        void UIOnBarClosed()
        {
            OpenOrCloseThePub.Content = "Open the bar";
            SliderValueTextBox.IsEnabled = true;
            sliderValue.IsEnabled = true;
        }
    }
}
