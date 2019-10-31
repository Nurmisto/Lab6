using System;
using System.Threading;
using System.Windows;

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
            view.Show();
            view.OpenOrCloseThePub.Click += OpenOrCloseThePub_Click;
        }
        

        public void SetTimeSlider()
        {
            if (!model.barOpen)
            {
                model.TimeUntillBarCloses = view.sliderValue.Value;
            }
            else
            {
                view.sliderValue.IsEnabled = false;
            }
        }


        
        public void PausOrContinueButtonsEnabled(bool enabled)
        {
            if (enabled)
            {
                view.bartenderPausOrContinueButton.IsEnabled = true;
                view.waitressPausOrContinueButton.IsEnabled = true;
                view.patronsPausOrContinueButton.IsEnabled = true;   
            }
            else if (!enabled)
            {
                view.bartenderPausOrContinueButton.IsEnabled = false;
                view.waitressPausOrContinueButton.IsEnabled = false;
                view.patronsPausOrContinueButton.IsEnabled = false;
            }
        }
        public void StartSimulation(bool startSimulation)
        {
            if (startSimulation)
            {
                //starta bouncer h´är istället
                
                
                model.bouncer.GeneratePatrons();
                Thread.Sleep(1000);
                foreach (var patron in Patron.patronsQue)
                {
                    if (patron.name != null)
                    {
                        view.patronsEventListBox.Items.Add($"{patron.name} kom in och går till baren");
                    }
                }
                view.RefreshListboxes();
            }
            else
            {
                MessageBox.Show("Simulation ended");
            }
        }
        

        

        private void OpenOrCloseThePub_Click(object sender, RoutedEventArgs e)
        {
            if (!model.barOpen)
            {
                StartSimulation(true);
                MessageBox.Show("Bar Open");
            }
            else
            {
                StartSimulation(false);
            }
        }

        //private void waitressPausOrContiniueButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void patronsPausOrContiniueButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void shutdownAllThreads_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}