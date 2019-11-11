using System;
using System.Diagnostics;
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

            model.GenerateChairsInBar();
            model.GenerateGlassesOnShelf();

            view.NumberOfGlasOnShelfLabel.Content = $"Det finns {Bar.shelfForGlasses.Count} glas i hyllan";
            view.NumberOfVacantSeatsLabel.Content = $"Det finns {Bar.availableChairs.Count} stolar lediga";

            view.NumberOfPatronsInBarLabel.Content = $"Det finns {Bar.numberOfPatronsInBar.Count.ToString()} gäster i baren";
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
            Stopwatch timer = new Stopwatch();
            var seconds = 10;
            var startCountdown = DateTime.UtcNow;
            model.endTime = startCountdown.AddSeconds(seconds);

            TimeSpan remaningTime = model.endTime - DateTime.UtcNow;

            if (startSimulation)
            {
                timer.Start();
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
                                view.patronsEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: {bouncer.GetAPatronWhoJustEntered().name} kom in och går till baren");
                                view.NumberOfPatronsInBarLabel.Content = $"Det finns {Bar.numberOfPatronsInBar.Count.ToString()} gäster i baren";
                                if(remaningTime < TimeSpan.Zero)
                                {
                                    Thread.CurrentThread.Abort();
                                }
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
                            if (Bar.patronsQueue.Count > 0 && Bar.shelfForGlasses.Count > 0)
                            {
                                string currentPatron = null;
                                currentPatron = bartender.PatronAboutToBeServed().name;

                                view.Dispatcher.Invoke(() =>
                                {
                                    view.bartenderEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: Hämtar glas från hyllan");
                                    view.NumberOfGlasOnShelfLabel.Content = $"Det finns {Bar.shelfForGlasses.Count} glas i hyllan";

                                });
                                Thread.Sleep(Bar.GetGlassTime);
                                view.Dispatcher.Invoke(() =>
                                {
                                    view.bartenderEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: Häller upp öl till {currentPatron}");
                                });
                                Thread.Sleep(Bar.PourGlassTime);
                                view.Dispatcher.Invoke(() =>
                                {
                                    view.patronsEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: {currentPatron} tar sin öl och letar efter bord");
                                });
                                //Thread.Sleep(Bar.LookForTableTime);
                                //bartender.PatronLookingForTable();
                                //view.Dispatcher.Invoke(() =>
                                //{
                                //    view.patronsEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: {currentPatron} hittade en ledig stol och dricker sin öl.");
                                //    view.NumberOfVacantSeatsLabel.Content = $"Det finns {Bar.availableChairs.Count} stolar lediga";
                                //});
                                //Thread.Sleep(Bar.DrinkBeerTime);

                            }
                            else if(Bar.shelfForGlasses.Count == 0)
                            {
                                view.Dispatcher.Invoke(() =>
                                {
                                    view.bartenderEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: Slut på glas, inväntar fler.");
                                });
                            }
                            else
                            {
                                view.Dispatcher.Invoke(() =>
                                {
                                    view.bartenderEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: Inväntar besökare");
                                });
                            }

                        }
                        catch (Exception)
                        {
                            throw new Exception();
                        }
                    }
                });
                Task.Run(() =>
                {
                    while (model.barOpen)
                    {
                        int glassesOnTablesFound;
                        glassesOnTablesFound = waitress.ClearTheTables();
                        try
                        {
                            if(Bar.numberOfPatronsInBar.Count > 0)
                            {
                                view.Dispatcher.Invoke(() =>
                                {
                                    view.waitressEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: Plockar glas från borden.");
                                });
                                Thread.Sleep(Bar.ClearTablesTime);
                                view.Dispatcher.Invoke(() =>
                                {
                                    view.waitressEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: Hittade {glassesOnTablesFound} glas.");
                                });
                                Thread.Sleep(Bar.CleanGlassesTime);

                                if(glassesOnTablesFound > 0)
                                {
                                    waitress.CleanGlasses(glassesOnTablesFound);
                                    view.Dispatcher.Invoke(() =>
                                    {
                                        view.waitressEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: {glassesOnTablesFound} glas har diskats och ställs på hyllan.");
                                        view.NumberOfGlasOnShelfLabel.Content = $"Det finns {Bar.shelfForGlasses.Count} glas i hyllan";
                                    });
                                }
                                Thread.Sleep(2000);
                            }
                        }
                        catch (Exception)
                        {
                            throw new Exception();
                        }
                    }
                });

            }
            else
            {
                timer.Stop();
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