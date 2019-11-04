using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

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
            model.NumberOfCleanGlasses = model.NumberOfGlasses;
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
        public void StartSimulation(bool startSimulation)
        {
            if (startSimulation)
            {
                Bartender bartender = new Bartender();
                Waitress waitress = new Waitress();
                Bouncer bouncer = new Bouncer();

                Task.Run(() =>
                {
                    while (model.barOpen)
                    {
                        bouncer.GeneratePatrons();
                        bartender.StartWorking();
                        try
                        {
                            view.Dispatcher.Invoke(() =>
                            {
                                view.patronsEventListBox.Items.Insert(0, $"{bouncer.GetAPatronWhoJustEntered().name} kom in och går till baren");

                            });
                        }
                        catch
                        {
                        }
                    }
                });

            }
            else
            {
                //MessageBox.Show("Simulation ended");
            }
        }
        private void OpenOrCloseThePub_Click(object sender, RoutedEventArgs e)
        {
            if (!model.barOpen)
            {
                model.barOpen = true;
                //MessageBox.Show("Bar open");
                view.UIOnBarOpen();
                StartSimulation(true);
            }
            else if(model.barOpen)
            {
                model.barOpen = false;
                //MessageBox.Show("Bar closed");
                view.UIOnBarClosed();
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