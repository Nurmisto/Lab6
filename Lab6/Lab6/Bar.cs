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
        public static BlockingCollection<int> shelfForGlasses = new BlockingCollection<int>(boundedCapacity: 8);
        public static BlockingCollection<int> glassesOnTables = new BlockingCollection<int>();
        public static BlockingCollection<int> availableChairs = new BlockingCollection<int>(boundedCapacity: 9);

        public bool simulationStarted = false;
        public bool couplesNight;
        public bool barOpen { get; set; } = false;

        public double TimeUntillBarCloses;
        public const int GetGlassTime = 3000;
        public const int PourGlassTime = 3000;
        public const int ClearTablesTime = 10000;
        public const int CleanGlassesTime = 15000;
        public DateTime endTime;
        
        public Bar()
        {

        }
        public void GenerateGlassesOnShelf()
        {
            for (int i = 0; i < shelfForGlasses.BoundedCapacity; i++)
            {
                shelfForGlasses.Add(1);
            }
        }
        public void GenerateChairsInBar()
        {
            for (int i = 0; i < availableChairs.BoundedCapacity; i++)
            {
                availableChairs.Add(1);
            }
        }
    }
}
