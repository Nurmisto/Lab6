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

    }
}