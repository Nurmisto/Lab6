using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Bar
    {
        public static ConcurrentQueue<Patron> patronsQueue = new ConcurrentQueue<Patron>();
        public static BlockingCollection<int> numberOfPatronsInBar = new BlockingCollection<int>(boundedCapacity: 10);
        //public BlockingCollection<int> shelfForGlasses = new BlockingCollection<int>(boundedCapacity: 8);

        public bool simulationStarted = false;
        public bool couplesNight;
        public bool barOpen { get; set; } = false;
        public static int NumberOfGlasses { get; set; } = 8;
        public int NumberOfCleanGlasses { get; set; }
        public static int NumberOfSeats { get; set; } = 9;

        public double TimeUntillBarCloses;
        public const int GetGlassTime = 3000;
        public const int PourGlassTime = 3000;
        public DateTime endTime;
        
        public Bar()
        {

        }
    }
}
