﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lab6
{
    public class Patron : Agent
    {
        //Affected rows that need to be changed for slow patrons
        //Row: 13 (default 4000), 47(default 20 000 - 30 000), 64 (default 1000), 
        private const int TimeSpentWalkingToChair = 4000;
        private const int TimeSpentWaiting = 100;
        public static int TimeSpentDrinkingBeer;
        public string Name { get; set; }
        public Enum CurrentState { get; set; }
        public Glass glass;
        public Chair chair;
        private ConcurrentQueue<Patron> CurrentQueue { get; set; }

        private static List<string> patronNameList = new List<string>() { "Alexander", "Anders", "Andreas", "Andreé", "Andreea", "Charlotte", "Daniel", "Elvis", "Emil", "FredrikÄrAldrigHär", "Johan",
                                                                "John", "Jonas", "Karo", "Khosro", "Luna", "Marcus", "Nicklas", "Nils", "Petter", "Pontus", "Robin", "Simon", "Sofia", "Tijana",
                                                                "Tommy", "Toni", "Wilhelm","Thiemo", "Stasya", "Madelyn", "Primitiva", "Alisha", "Stanko", "Jacobine", "Priti", "Mariona", "Mathias", "Alf",
                                                                "Jo", "Terje", "Bente", "Kaj", "Halle", "Torleif", "Aron", "Halle", "Brynhild", "Atle", "Asgeir", "Emilia", "Kevin", "Erlend",
                                                                "Ursula", "Thomas", "Rikard"};

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
        public static int TimeDrinkingBeer(int DrinkTime)
        {
            Random r = new Random();
            int drinkTime = r.Next(20000, 30000);
            TimeSpentDrinkingBeer = drinkTime;
            return TimeSpentDrinkingBeer;
        }

        public override void AgentCycle(Bar bar)
        {
            while (hasGoneHome is false)
            {
                TimeDrinkingBeer(TimeSpentDrinkingBeer);
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
                            BarController.EventListBoxHandler(this, $"{Name} is waiting for a glass of beer");
                            while (glass is null)
                            {
                                Thread.Sleep(TimeSpentWaiting);
                            }
                            BarController.EventListBoxHandler(this, $"{Name} got a glass of beer");
                            DequeuePatron(CurrentQueue, this);
                            CurrentState = RunState.WaitingForChair;
                            break;
                        }
                    case RunState.WaitingForChair:
                        {
                            bar.PatronsWaitingForChair.Enqueue(this);
                            CurrentQueue = bar.PatronsWaitingForChair;
                            BarController.EventListBoxHandler(this, $"{Name} is waiting for an available chair");
                            
                            var isFirstInQueue = false;
                            while (isFirstInQueue is false)
                            {
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
                                            this.chair = chair;
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
                            BarController.EventListBoxHandler(this, $"{Name} is drinking the beer");
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
