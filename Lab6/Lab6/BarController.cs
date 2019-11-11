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
            


            if (startSimulation)
            {
                timer.Start();
                Bouncer bouncer = new Bouncer();
                Bartender bartender = new Bartender();
                Waitress waitress = new Waitress();
                model.TimeUntillBarCloses = 10;

                var ts = new CancellationTokenSource();
                CancellationToken ct = ts.Token;

                Task.Run(() => // Time untill bar closes.
                {
                    while (model.barOpen)
                    {
                        if (model.TimeUntillBarCloses <= timer.Elapsed.TotalSeconds)
                        {
                            model.barOpen = false;
                            view.Dispatcher.Invoke(() =>
                            {
                                view.UIOnBarClosed();
                            });
                            
                            Thread.Sleep(20);
                            ts.Cancel();
                            timer.Stop();
                        }
                    }
                });


                Task.Run(() => // Generate new patrons
                {
                    while (model.barOpen)
                    {
                        bouncer.GeneratePatrons();
                        if (ct.IsCancellationRequested)
                        {
                            break;
                        }
                        try
                        {
                            view.Dispatcher.Invoke(() =>
                            {
                                view.patronsEventListBox.Items.Insert(0, $"{Math.Round(decimal.Parse(timer.Elapsed.TotalSeconds.ToString()), 3)} s: {bouncer.GetAPatronWhoJustEntered().name} kom in och går till baren");
                                view.NumberOfPatronsInBarLabel.Content = $"Det finns {Bar.numberOfPatronsInBar.Count.ToString()} gäster i baren";
                                
                            });
                        }
                        catch
                        {
                        }
                    }
                }, ct);
                

                Task.Run(() => // Bartender work
                {
                    while (model.barOpen)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            break;
                        }
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
                }, ct);

                Task.Run(() => // Waitress work
                {
                    while (model.barOpen)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            break;
                        }
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
                }, ct);
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