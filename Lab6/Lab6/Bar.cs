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
        public static BlockingCollection<int> shelfForGlasses = new BlockingCollection<int>;//(boundedCapacity: 8);
        public static BlockingCollection<int> glassesOnTables = new BlockingCollection<int>;//();
        public static BlockingCollection<int> availableChairs = new BlockingCollection<int>;//(boundedCapacity: 9);

        public double TimeUntillBarCloses;
        public DateTime endTime;

        
        public Bar()
        {

        }
        
    }
}
