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

            //view.NumberOfGlasOnShelfLabel.Content = $"Det finns {Bar.shelfForGlasses.Count} glas i hyllan";
            //view.NumberOfVacantSeatsLabel.Content = $"Det finns {Bar.availableChairs.Count} stolar lediga";
            //
            //view.NumberOfPatronsInBarLabel.Content = $"Det finns {Bar.numberOfPatronsInBar.Count.ToString()} gäster i baren";
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
                view.UIOnBarOpen();
                StartSimulation();
            }
            else if(model.BarOpen)
            {
                model.BarOpen = false;
                view.UIOnBarClosed();

            }
        }
        public void EventListBoxHandler(Agent messageLogger, string message)
        {
            string messageInput = ": " + message;
            if (messageLogger is Bartender)
            {
                view.Dispatcher.Invoke(() => view.bartenderEventListBox.Items.Insert(0, messageInput));
            }
            else if (messageLogger is Bouncer || messageLogger is Patron)
            {
                view.Dispatcher.Invoke(() => view.patronsEventListBox.Items.Insert(0, messageInput));
            }
            else if (messageLogger is Waitress)
            {
                view.Dispatcher.Invoke(() => view.waitressEventListBox.Items.Insert(0, messageInput));
            }
        }
        public void RefreshLabels()
        {
            view.Dispatcher.Invoke(() => view.NumberOfGlasOnShelfLabel.Content = "Glasses on shelf: " + model.shelfForGlasses.Count);
            view.Dispatcher.Invoke(() => view.NumberOfPatronsInBarLabel.Content = "Patrons: " + model.patronsQueue.Count);
            view.Dispatcher.Invoke(() => view.NumberOfVacantSeatsLabel.Content = "Available chairs: " + (from chair in model.availableChairs
                                                                                                         where chair.IsAvailable
                                                                                                         select chair).Count());
        }
    }
}