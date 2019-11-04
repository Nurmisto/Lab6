using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Bar
    {
        public bool simulationStarted = false;
        public bool couplesNight;
        public bool barOpen { get; set; } = false;
        public int NumberOfGlasses { get; set; } = 8;
        public int NumberOfCleanGlasses { get; set; }
        public int NumberOfSeats { get; set; } = 9;
        public double TimeUntillBarCloses;
        public Bouncer bouncer { get; set; }
        public Bartender bartender { get; set; }
        public Waitress waitress { get; set; }
        public Patron patron { get; set; }
        public Queue<Patron> patronsQueue = new Queue<Patron>();

        public DateTime TimeStamp { get; set; }
        public bool PatronWalkedToBar;
        public bool HasBeenServedBeer;
        public Bar()
        {
        
        }
    }
}
