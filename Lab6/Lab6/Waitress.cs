using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Lab6
{
    public class Waitress : Agent
    {
        public Enum CurrentState { get; set; }

        private ConcurrentBag<Glass> tray;
        private const int TimeSpentCollectingPintGlass = 10000;
        private const int TimeSpentWashingPintGlass = 15000;
        private const int TimeSpentIdling = 100;
        private bool hasBeenProductive = true;
        public Waitress(Bar bar)
        {
            Bar = bar;
            BarController = bar.BarController;
            tray = new ConcurrentBag<Glass>();
        }

        public override void AgentCycle(Bar bar)
        {
            while (hasGoneHome is false)
            {
                switch (CheckState(bar))
                {
                    case RunState.Idling:
                        {
                            Thread.Sleep(TimeSpentIdling);
                            if (hasBeenProductive is true)
                            {
                                BarController.EventListBoxHandler(this, "Is currently idling");
                            }
                            hasBeenProductive = false;
                            break;
                        }
                    case RunState.Working:
                        {
                            //Gather empty pints from Tables
                            BarController.EventListBoxHandler(this, "Gathering dirty pints from tables");
                            foreach (var glass in bar.glassesOnTables.Where(g => g.HasBeer is false && g.IsClean is false))
                            {
                                Glass gatheredPintGlass = null;
                                while (gatheredPintGlass is null)
                                {
                                    bar.glassesOnTables.TryTake(out gatheredPintGlass);
                                }
                                tray.Add(gatheredPintGlass);
                            }
                            Thread.Sleep(TimeSpentCollectingPintGlass);

                            //Clean glass and place on Shelves
                            BarController.EventListBoxHandler(this, $"Cleaning {tray.Count} pint(s)");
                            Thread.Sleep(TimeSpentWashingPintGlass);
                            BarController.EventListBoxHandler(this, "Placing clean pints on the shelves");
                            foreach (var collectedGlass in tray)
                            {
                                collectedGlass.IsClean = true;
                                if (bar.shelfForGlasses.TryAdd(collectedGlass))
                                {
                                    tray.TryTake(out Glass glass);
                                }
                            }
                            hasBeenProductive = true;
                            break;
                        }
                    case RunState.LeavingThePub:
                        {
                            BarController.EventListBoxHandler(this, "Going home");
                            hasGoneHome = true;
                            break;
                        }
                }
            }
        }
        public RunState CheckState(Bar bar)
        {
            if (bar.patronsQueue.IsEmpty && bar.glassesOnTables.Count is 0 && bar.currentBarState is BarState.Closed)
            {
                return RunState.LeavingThePub;
            }
            else if (bar.glassesOnTables.Count > 0)
            {
                return RunState.Working;
            }
            else
            {
                return RunState.Idling;
            }
        }
    }
}
