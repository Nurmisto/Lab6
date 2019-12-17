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

        public BarController(MainWindow view)
        {
            MainWindow = view;
        }
        
        public void StartSimulation()
        {
            model = new Bar(this);

            model.StartAgents();
        }

        private void OpenOrCloseThePub_Click(object sender, RoutedEventArgs e)
        {
            if (!model.BarOpen)
            {
                model.BarOpen = true;
                MainWindow.UIOnBarOpen();
                StartSimulation();
            }
            else if(model.BarOpen)
            {
                model.BarOpen = false;
                MainWindow.UIOnBarClosed();

            }
        }
        public void EventListBoxHandler(Agent messageLogger, string message)
        {
            string messageInput = ": " + message;
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