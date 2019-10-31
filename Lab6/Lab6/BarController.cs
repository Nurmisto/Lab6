using System;

namespace Lab6
{
    internal partial class BarController
    {
        private MainWindow view;
        private Bar model;

        public BarController(MainWindow view, Bar model)
        {
            this.view = view;
            this.model = model;
        }

        TimeUntillBarCloses = sliderValue.Value;
        
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
            if (startSimulation)
            {
                Bouncer bouncer = new Bouncer(); //starta bouncer h´är istället
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
        private void OpenOrCloseThePub_Click(object sender, RoutedEventArgs e)
        {

        }
        private void waitressPausOrContiniueButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void patronsPausOrContiniueButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void shutdownAllThreads_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}