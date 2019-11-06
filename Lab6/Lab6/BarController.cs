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
            view.NumberOfGlasOnShelfLabel.Content = $"Det finns {Bar.NumberOfGlasses} glas i hyllan";
            view.NumberOfPatronsInBarLabel.Content = $"Det finns {Bar.numberOfPatronsInBar.Count.ToString()} gäster i baren";
            view.OpenOrCloseThePub.Click += OpenOrCloseThePub_Click;
            model.NumberOfCleanGlasses = Bar.NumberOfGlasses;
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
                Bouncer bouncer = new Bouncer();
                Bartender bartender = new Bartender();
                Waitress waitress = new Waitress();
                Task.Run(() =>
                {
                    while (model.barOpen)
                    {
                        bouncer.GeneratePatrons();
                        try
                        {
                            view.Dispatcher.Invoke(() =>
                            {
                                view.patronsEventListBox.Items.Insert(0, $"{bouncer.GetAPatronWhoJustEntered().name} kom in och går till baren");
                                view.NumberOfPatronsInBarLabel.Content = $"Det finns {Bar.numberOfPatronsInBar.Count.ToString()} gäster i baren";
                            });
                        }
                        catch
                        {
                        }
                    }
                });

                Task.Run(() =>
                {
                    while (model.barOpen)
                    {
                        Thread.Sleep(2000);
                        try
                        {
                            if(Bar.patronsQueue.Count > 0)
                            {
                                string currentPatron = null;
                                currentPatron = bartender.PatronAboutToBeServed().name;
                                Thread.Sleep(3000);
                                view.Dispatcher.Invoke(() =>
                                {
                                    if(Bar.NumberOfGlasses > 0)
                                    {
                                        view.bartenderEventListBox.Items.Insert(0, $"Häller upp öl till {currentPatron}");
                                        view.patronsEventListBox.Items.RemoveAt(0);
                                        view.NumberOfGlasOnShelfLabel.Content = $"Det finns {Bar.NumberOfGlasses} glas i hyllan";
                                    }
                                });

                            }
                            else
                            {
                                view.Dispatcher.Invoke(() =>
                                {
                                    view.bartenderEventListBox.Items.Remove("Inväntar besökare");
                                    view.bartenderEventListBox.Items.Insert(0, "Inväntar besökare");
                                });
                            }
                            
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                });
                Task.Run(() =>
                {
                    while (model.barOpen)
                    {
                        waitress.ClearTheTables();
                        try
                        {
                            view.Dispatcher.Invoke(() =>
                            {

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
                view.UIOnBarOpen();
                StartSimulation(true);
            }
            else if(model.barOpen)
            {
                model.barOpen = false;
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