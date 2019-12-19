using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lab6
{
    public class Patron : Agent
    {
        private const int TimeSpentWalkingToChair = 4000;
        private const int TimeSpentWaiting = 100;
        public int TimeSpentDrinkingBeer { get; set; }
        public string Name { get; set; }
        public Enum CurrentState { get; set; }
        public Glass glass;
        public Chair chair;
        private ConcurrentQueue<Patron> CurrentQueue { get; set; }

        private static List<string> patronNameList = new List<string>() { "Alexander", "Anders", "Andreas", "Andreé", "Andreea", "Charlotte", "Daniel", "Elvis", "Emil", "FredrikÄrAldrigHär", "Johan",
                                                                "John", "Jonas", "Karo", "Khosro", "Luna", "Marcus", "Nicklas", "Nils", "Petter", "Pontus", "Robin", "Simon", "Sofia", "Tijana",
                                                                "Tommy", "Toni", "Wilhelm","Alexander", "Anders", "Andreas", "Andreé", "Andreea", "Charlotte", "Daniel", "Elvis", "Emil", "FredrikÄrAldrigHär", "Johan",
                                                                "John", "Jonas", "Karo", "Khosro", "Luna", "Marcus", "Nicklas", "Nils", "Petter", "Pontus", "Robin", "Simon", "Sofia", "Tijana",
                                                                "Tommy", "Toni", "Wilhelm"};

        public Patron(Bar bar)
        {
            Bar = bar;
            BarController = bar.BarController;
            Name = GetRandomPatronName();
            CurrentState = RunState.WalkingToBar;
            
            Run(bar);
        }
        public static string GetRandomPatronName()
        {
            Random r = new Random();
            int index = r.Next(patronNameList.Count);
            string patronName = patronNameList[index];
            patronNameList.RemoveAt(index);
            return patronName;
        }

        public override void AgentCycle(Bar bar)
        {
            while (hasGoneHome is false)
            {
                switch (CurrentState)
                {
                    case RunState.WalkingToBar:
                        {
                            BarController.EventListBoxHandler(this, $"{Name} entered and is walking to the bar");
                            bar.PatronsWaitingForBeer.Enqueue(this);
                            CurrentQueue = bar.PatronsWaitingForBeer;
                            Thread.Sleep(1000);
                            CurrentState = RunState.WaitingForBeer;
                            break;
                        }
                    case RunState.WaitingForBeer:
                        {
                            BarController.EventListBoxHandler(this, $"{Name} is waiting for a pint of beer");
                            while (glass is null)
                            {
                                Thread.Sleep(TimeSpentWaiting);
                            }
                            BarController.EventListBoxHandler(this, $"{Name} got a pint of beer");
                            DequeuePatron(CurrentQueue, this);
                            CurrentState = RunState.WaitingForChair;
                            break;
                        }
                    case RunState.WaitingForChair:
                        {
                            bar.PatronsWaitingForChair.Enqueue(this);
                            CurrentQueue = bar.PatronsWaitingForChair;
                            BarController.EventListBoxHandler(this, $"{Name} is waiting for an available chair");
                            //Check to see if patron is first in line
                            var isFirstInQueue = false;
                            while (isFirstInQueue is false)
                            {
                                //Spend time checking
                                Thread.Sleep(TimeSpentWaiting);
                                bar.PatronsWaitingForChair.TryPeek(out var firstPatronInQueue);
                                if (this.Equals(firstPatronInQueue))
                                {
                                    isFirstInQueue = true;
                                    while (chair is null)
                                    {
                                        foreach (var chair in bar.availableChairs)
                                        {
                                            if (!(chair.IsAvailable)) continue;
                                            this.chair = chair; //Dibs on available chair
                                            chair.IsAvailable = false;
                                            DequeuePatron(CurrentQueue, this);
                                            BarController.RefreshLabels();
                                            break;
                                        }
                                    }
                                }
                            }
                            CurrentState = RunState.WalkingToChair;
                            break;
                        }
                    case RunState.WalkingToChair:
                        {
                            BarController.EventListBoxHandler(this, $"{Name} is walking to a chair");
                            Thread.Sleep(TimeSpentWalkingToChair);
                            CurrentState = RunState.DrinkingBeer;
                            break;
                        }
                    case RunState.DrinkingBeer:
                        {
                            BarController.EventListBoxHandler(this, $"{Name} is drinking a pint of beer");
                            Thread.Sleep(TimeSpentDrinkingBeer);
                            glass.HasBeer = false;

                            bar.glassesOnTables.Add(glass);
                            chair.IsAvailable = true;
                            CurrentState = RunState.LeavingThePub;
                            break;
                        }
                    case RunState.LeavingThePub:
                        {
                            BarController.EventListBoxHandler(this, $"{Name} finished the beer and is going home");
                            while (hasGoneHome is false)
                            {
                                hasGoneHome = bar.patronsQueue.TryRemove(Name, out _);
                            }
                            hasGoneHome = true;
                            break;
                        }
                }
            }
        }

        private Patron DequeuePatron(ConcurrentQueue<Patron> currentQueue, Patron patronToDequeue)
        {
            Patron patron = null;
            while(patron is null)
            {
                currentQueue.TryDequeue(out patron);
            }
            return patron;
        }
    }
}
