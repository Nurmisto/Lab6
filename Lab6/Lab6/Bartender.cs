﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Lab6
{
    public class Bartender : Agent
    {
        private Glass glassInBar;
        private bool hasBeenProductive = true;
        private const int TimeSpentGettingGlass = 3000;
        private const int TimeSpentFillingGlassWithBeer = 3000;
        public Bartender(Bar bar)
        {
            Bar = bar;
            BarController = bar.BarController;
        }

        public override void AgentCycle(Bar bar)
        {
            while (!hasGoneHome)
            {
                switch (CheckState(bar))
                {
                    case RunState.Idling:
                        {
                            if (hasBeenProductive)
                            {
                                if (bar.PatronsWaitingForBeer.IsEmpty) BarController.EventListBoxHandler(this, "Waiting for a patron");
                                else if (bar.shelfForGlasses.Count is 0) BarController.EventListBoxHandler(this, "Waiting for clean glasses");
                            }
                            hasBeenProductive = false;
                            Thread.Sleep(100);
                            break;
                        }
                    case RunState.Working:
                        {
                            Patron patronWaitingToBeServed = null;
                            while(patronWaitingToBeServed is null)
                            {
                                bar.PatronsWaitingForBeer.TryPeek(out patronWaitingToBeServed);
                            }
                            if(patronWaitingToBeServed.glass is null)
                            {
                                BarController.EventListBoxHandler(this, $"Taking order from {patronWaitingToBeServed.Name}");

                                while (glassInBar is null)
                                {
                                    bar.shelfForGlasses.TryTake(out glassInBar);
                                }
                                BarController.EventListBoxHandler(this, "Getting a glass from the shelves");
                                Thread.Sleep(TimeSpentGettingGlass);

                                glassInBar.HasBeer = true;
                                glassInBar.IsClean = false;
                                BarController.EventListBoxHandler(this, "Filling glass with beer");
                                Thread.Sleep(TimeSpentFillingGlassWithBeer);

                                patronWaitingToBeServed.glass = glassInBar;
                                BarController.EventListBoxHandler(this, $"Giving beer to {patronWaitingToBeServed.Name}");
                                glassInBar = null;
                                hasBeenProductive = true;
                            }
                            break;
                        }
                    case RunState.LeavingThePub:
                        {
                            BarController.EventListBoxHandler(this, "Bartender is going home");
                            hasGoneHome = true;
                            break;
                        }
                }
            }
        }

        public RunState CheckState(Bar bar)
        {
            if (bar.patronsQueue.IsEmpty && bar.currentBarState is BarState.Closed)
            {
                return RunState.LeavingThePub;
            }
            else if (!bar.PatronsWaitingForBeer.IsEmpty && bar.shelfForGlasses.Count > 0)
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
