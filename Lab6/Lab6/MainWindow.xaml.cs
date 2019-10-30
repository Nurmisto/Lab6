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
        public double TimeUntillBarCloses;
        public bool couplesNight;
        public DateTime DateTimerStart;
        
        public double BarOpenTime { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            TimeUntillBarCloses = sliderValue.Value;
            Console.WriteLine(TimeUntillBarCloses);

            NumberOfPatronsInBarLabel.Content = $"Total guests in bar: {Bar.NumberOfPatronsInBar}";
            NumberOfGlasOnShelfLabel.Content = $"Total glasses on shelf: {Bar.NumberOfGlasses}";
            NumberOfVacantSeatsLabel.Content = $"Total chairs available: {Bar.NumberOfSeats}";

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


        public void StartSimulation(bool startSimulation)
        {
            simulationStarted = true;
            if (startSimulation)
            {
                Bouncer bouncer = new Bouncer();
                bouncer.GeneratePatrons();
                foreach (var patron in Patron.patrons)
                {
                    if (patron.name != null)
                    {
                        patronsEventListBox.Items.Add($"{patron.name} kom in och går till baren");
                    }
                }
                RefreshListboxes();
            }
            else
            {
                MessageBox.Show("Simulation ended");
            }
        }
        private void RefreshListboxes()
        {
            bartenderEventListBox.Items.Refresh();
            waitressEventListBox.Items.Refresh();
            patronsEventListBox.Items.Refresh();
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
            if (!Bar.barOpen)
            {
                Bar.barOpen = true;
                MessageBox.Show("Open bar");
                UIOnBarOpen();
                StartSimulation(true);
                PausOrContinueButtonsEnabled(true);
            }
            else if(Bar.barOpen)
            {
                Bar.barOpen = false;
                MessageBox.Show("Close bar");
                OpenOrCloseThePub.Content = "Closing the bar and going home..";
                TimeUntillBarCloses = 0;
                UIOnBarClosed();
                StartSimulation(false);
                PausOrContinueButtonsEnabled(false);
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
