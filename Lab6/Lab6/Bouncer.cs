using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Lab6
{
    public class Bouncer : Agent
    {
        private int NumberOfPatronToLetInside = 1;
        int bouncerSpeed = 1000;
        
        public Timer timer;
        

        public Bouncer(Bar bar)
        {
            Bar = bar;
            BarController = bar.BarController;
        }

        public override void AgentCycle(Bar bar)
        {
            var busCheck = 0;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (hasGoneHome is false)
            {
                
                switch (CheckState(bar))
                {
                    
                    case RunState.Working:
                        {
                            
                            if (Bar.IsBussLoad)
                            {
                                bouncerSpeed = 500;
                                
                                while(busCheck == 0)
                                {
                                    if(timer.ElapsedMilliseconds >= 20000)
                                    {
                                        
                                        busCheck++;
                                        for (int i = 0; i <= 10; i++)
                                        {
                                            var newPatron = new Patron(bar);
                                            bar.patronsQueue.TryAdd(newPatron.Name, newPatron);

                                        }
                                        timer.Stop();
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                    
                                }
                                

                                //Ändra timesbetween
                                // sätt en int utanför ifstatmentet som är 0, när bussload har körts sätts den till 1 och kör inte igen
                                //få timern att funka.. Fuck meee
                            }

                            if (Bar.IsCouplesNight)
                            {
                                NumberOfPatronToLetInside = 2;
                            }
                            Thread.Sleep(TimeBetweenLettingPatronIn(bouncerSpeed));
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

        

        private static int TimeBetweenLettingPatronIn(int milliseconds)
        {
            
            Random r = new Random();
            milliseconds = 1000 * (r.Next(3, 11));
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
