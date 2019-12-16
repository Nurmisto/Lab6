using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Lab6
{
    public class BarController
    {
        private MainWindow view;
        private Bar model;

        public BarController(MainWindow view, Bar model)
        {
            this.view = view;
            this.model = model;
            view.Show();
            view.OpenOrCloseThePub.Click += OpenOrCloseThePub_Click;

            //view.NumberOfGlasOnShelfLabel.Content = $"Det finns {Bar.shelfForGlasses.Count} glas i hyllan";
            //view.NumberOfVacantSeatsLabel.Content = $"Det finns {Bar.availableChairs.Count} stolar lediga";
            //
            //view.NumberOfPatronsInBarLabel.Content = $"Det finns {Bar.numberOfPatronsInBar.Count.ToString()} gäster i baren";
        }
        
        public void StartSimulation()
        {
            model = new Bar(this);

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

    }
}