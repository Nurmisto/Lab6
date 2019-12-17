using System;
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
        //Default values : 8 glasses, 9 chairs, time untill bar closes 120 sec.
        //Simulation nr2 : 20 glasses, 3 chairs, time untill bar closes 120 sec.
        //Simulation nr3 : 5 glasses, 20 chairs, time untill bar closes 120 sec.
        //Simulation nr4 : 8 glasses, 9 chairs , time untill bar closes 120 sec, Guests stays for double amount of time. (Point to location to change value)
        //Simulation nr5 : 8 glasses, 9 chairs, time untill bar closes 120 sec, waitress dose her job twise as fast. (Point to location to change value)
        //Simulation nr6 : 8 glasses, 9 chairs, time untill bar closes 300 sec. (Point to location to change value)
        //Simulation nr7 : 8 glasses, 9 chairs, time untill bar closes 120 sec, couples night: bouncer lets 2 patrons in at the time. To change bouncer propertys -> Bouncer.cs line 12
        //Simulation nr8 : 8 glasses, 9 chairs, time untill bar closes 120 sec, reduce bouncer efficiency 50%, after 20 sec, let in bussload(20 patrons)
        private const int NumberOfGlasses = 8;
        private const int NumberOfChairs = 9;
        public const int TimeUntillBarCloses = 120;
        public bool BarOpen { get; set; } = false;
        public Enum currentBarState { get; set; }
        public BarController BarController { get; set; }
        public Bartender Bartender { get; set; }
        public Bouncer Bouncer { get; set; }
        public Waitress Waitress { get; set; }

        public ConcurrentDictionary<String, Patron> patronsQueue;
        public ConcurrentQueue<Patron> PatronsWaitingForBeer;
        public ConcurrentQueue<Patron> PatronsWaitingForChair;

        public BlockingCollection<Glass> shelfForGlasses;
        public BlockingCollection<Glass> glassesOnTables;
        public BlockingCollection<Chair> availableChairs;

        public DateTime endTime;


        public Bar(BarController barController)
        {
            BarController = barController;
            currentBarState = BarState.Open;

            PatronsWaitingForBeer = new ConcurrentQueue<Patron>();
            PatronsWaitingForChair = new ConcurrentQueue<Patron>();
            patronsQueue = new ConcurrentDictionary<String, Patron>();

            glassesOnTables = new BlockingCollection<Glass>();
            shelfForGlasses = new BlockingCollection<Glass>();
            availableChairs = new BlockingCollection<Chair>();

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
