using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Lab6
{
    public class Bouncer : Agent
    {
        private int NumberOfPatronToLetInside = 1;

        public Bouncer(Bar bar)
        {
            Bar = bar;
            BarController = bar.BarController;
        }

        public override void AgentCycle(Bar bar)
        {
            while (hasGoneHome is false)
            {
                switch (CheckState(bar))
                {
                    case RunState.Working:
                        {
                            Thread.Sleep(TimeBetweenLettingPatronIn());
                            for (int patron = 0; patron < NumberOfPatronToLetInside; patron++)
                            {
                                var newPatron = new Patron(bar);
                                bar.patronsQueue.TryAdd(newPatron.Name, newPatron);
                            }
                            break;
                        }
                    case RunState.LeavingThePub:
                        {
                            BarController.EventListBoxHandler(this, "Bouncer is going home");
                            hasGoneHome = true;
                            break;
                        }
                }
            }
        }
        private static int TimeBetweenLettingPatronIn()
        {
            Random r = new Random();
            var milliseconds = 1000 * (r.Next(3, 11));
            return milliseconds;
        }
        public RunState CheckState(Bar bar)
        {
            if (bar.currentBarState is BarState.Closed)
            {
                return RunState.LeavingThePub;
            }
            return RunState.Working;
        }
    }
}
