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
        private const int NumberOfGlasses = 8;
        private const int NumberOfChairs = 9;
        public const int TimeUntillBarCloses = 120;
        public bool BarOpen { get; set; } = false;
        public Enum currentBarState { get; set; }
        public BarController BarController { get; set; }
        public Bartender Bartender { get; set; }
        public Bouncer Bouncer { get; set; }
        public Waitress Waitress { get; set; }

        public ConcurrentQueue<Patron> patronsQueue = new ConcurrentQueue<Patron>();
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
