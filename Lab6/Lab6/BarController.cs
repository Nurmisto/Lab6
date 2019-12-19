using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Lab6
{
    public class BarController
    {
        private MainWindow MainWindow { get; set; }
        private Bar model;
        public static TimeSpan time;
        private DispatcherTimer timer;
        private int logCount = 0;
        
        

        public BarController(MainWindow view)
        {
           
            MainWindow = view;
            
        }
        
        public void StartSimulation()
        {
            
            model = new Bar(this);
            time = TimeSpan.FromSeconds(Bar.TimeUntillBarCloses);
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                MainWindow.TimeUntillClosed.Content = time.ToString("c");
                if (time == TimeSpan.Zero && model.currentBarState is BarState.Open)
                {
                    model.currentBarState = BarState.Closed;
                }
                if (
                    model.currentBarState is BarState.Closed &&
                    model.Bartender.hasGoneHome is true &&
                    model.Bouncer.hasGoneHome is true &&
                    model.Waitress.hasGoneHome is true
                    ) timer.Stop();
                time = time.Add(TimeSpan.FromSeconds(-1));

                RefreshLabels();
            }, Application.Current.Dispatcher);
            model.StartAgents();
            if (MainWindow.BussLoad.IsChecked == true)
            {
                model.IsBussLoad = true;

            }
        }

        public void EventListBoxHandler(Agent messageLogger, string message)
        {
            logCount++;
            string messageInput = logCount + ": " + message;
            if (messageLogger is Bartender)
            {
                MainWindow.Dispatcher.Invoke(() => MainWindow.bartenderEventListBox.Items.Insert(0, messageInput));
            }
            else if (messageLogger is Bouncer || messageLogger is Patron)
            {
                MainWindow.Dispatcher.Invoke(() => MainWindow.patronsEventListBox.Items.Insert(0, messageInput));
            }
            else if (messageLogger is Waitress)
            {
                MainWindow.Dispatcher.Invoke(() => MainWindow.waitressEventListBox.Items.Insert(0, messageInput));
            }
        }
        public void RefreshLabels()
        {
            MainWindow.Dispatcher.Invoke(() => MainWindow.NumberOfGlasOnShelfLabel.Content = "Glasses on shelf: " + model.shelfForGlasses.Count);
            MainWindow.Dispatcher.Invoke(() => MainWindow.NumberOfPatronsInBarLabel.Content = "Patrons: " + model.patronsQueue.Count);
            MainWindow.Dispatcher.Invoke(() => MainWindow.NumberOfVacantSeatsLabel.Content = "Available chairs: " + (from chair in model.availableChairs
                                                                                                         where chair.IsAvailable
                                                                                                         select chair).Count());
        }
    }
}