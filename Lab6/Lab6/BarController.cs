using System;
using System.Threading;
using System.Threading.Tasks;
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
        public void StartSimulation(bool startSimulation)
        {
            if (startSimulation)
            {
                Bartender bartender = new Bartender();
                Waitress waitress = new Waitress();
                Bouncer bouncer = new Bouncer();

                while (model.barOpen)
                {
                    Task.Factory.StartNew(() =>
                    {
                        bouncer.GeneratePatrons();
                    });
                    Thread.Sleep(5000);
                    MessageBox.Show("Nu har det thread sleepats 5 sec");
                    foreach (var patron in bouncer.patronsQueue)
                    {
                        if (!patron.PatronWalkedToBar)
                        {
                            view.patronsEventListBox.Items.Insert(0, $"{patron.name} kom in och går till baren");
                            patron.PatronWalkedToBar = true;
                            view.RefreshListboxes();
                        }
                        Thread.Sleep(2000);
                        MessageBox.Show("Nu har det thread sleepats 2 sec");
                        if (patron.PatronWalkedToBar && !patron.HasBeenServedBeer)
                        {
                            view.bartenderEventListBox.Items.Insert(0, $"Bartender serverar öl till {patron.name}");
                            patron.HasBeenServedBeer = true;
                            view.RefreshListboxes();
                        }
                    }
                    
                }




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
                model.barOpen = true;
                MessageBox.Show("Bar open");
                view.UIOnBarOpen();
                StartSimulation(true);
            }
            else if(model.barOpen)
            {
                model.barOpen = false;
                MessageBox.Show("Bar closed");
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