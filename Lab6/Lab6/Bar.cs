﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public enum BarState { Open, Closed }
    public class Bar
    {
        //Set the parameters for the different simulations
        //Parameters for glasses, chairs and time untill close is all just below.
        //Default values : 8 glasses, 9 chairs, time untill bar closes 120 sec.
        //Simulation nr2 : 20 glasses, 3 chairs, time untill bar closes 120 sec.
        //Simulation nr3 : 5 glasses, 20 chairs, time untill bar closes 120 sec.
        //Simulation nr4 : 8 glasses, 9 chairs , time untill bar closes 120 sec, Guests stays for double amount of time. Please check top of patrons file for values to change
        //Simulation nr5 : 8 glasses, 9 chairs, time untill bar closes 120 sec, waitress dose her job twise as fast. Please check top of Waitress file for values to change.
        //Simulation nr6 : 8 glasses, 9 chairs, time untill bar closes 300 sec.
        //Simulation nr7 : 8 glasses, 9 chairs, time untill bar closes 120 sec, couples night: bouncer lets 2 patrons in at the time. (Checkbox in UI)
        //Simulation nr8 : 8 glasses, 9 chairs, time untill bar closes 120 sec, reduce bouncer efficiency 50%, after 20 sec, let in bussload(20 patrons) (Checkbox in UI)
        private const int NumberOfGlasses = 8;
        private const int NumberOfChairs = 9;
        public const int TimeUntillBarCloses = 120;
        public bool BarOpen { get; set; } = false;
        public bool IsBussLoad { get; set; } = false;
        public bool IsCouplesNight { get; set; } = false;
        public Enum currentBarState { get; set; }
        public BarController BarController { get; set; }
        public Bartender Bartender { get; set; }
        public Bouncer Bouncer { get; set; }
        public Waitress Waitress { get; set; }

        public ConcurrentDictionary<String, Patron> patronsQueue = new ConcurrentDictionary<String, Patron>();
        public ConcurrentQueue<Patron> PatronsWaitingForBeer = new ConcurrentQueue<Patron>();
        public ConcurrentQueue<Patron> PatronsWaitingForChair = new ConcurrentQueue<Patron>();

        public BlockingCollection<Glass> shelfForGlasses = new BlockingCollection<Glass>();
        public BlockingCollection<Glass> glassesOnTables = new BlockingCollection<Glass>();
        public BlockingCollection<Chair> availableChairs = new BlockingCollection<Chair>();

        public DateTime endTime;

        public Bar(BarController barController)
        {
            BarController = barController;
            currentBarState = BarState.Open;
            GenerateGlasses();
            GenerateChairs();
            Bartender = new Bartender(this);
            Bouncer = new Bouncer(this);
            Waitress = new Waitress(this);
        }
        public void GenerateGlasses()
        {
            for (int i = 0; i < NumberOfGlasses; i++)
            {
                shelfForGlasses.Add(new Glass());
            }
        }
        public void GenerateChairs()
        {
            for (int i = 0; i < NumberOfChairs; i++)
            {
                availableChairs.Add(new Chair());
            }
        }
        public void StartAgents()
        {
            Bartender.Run(this);
            Bouncer.Run(this);
            Waitress.Run(this);
        }

    }
}
