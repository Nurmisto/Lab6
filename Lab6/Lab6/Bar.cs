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
         public double TimeUntillBarCloses;
         public bool couplesNight;
         public static bool barOpen = false;
         public static int NumberOfPatronsInBar { get; set; } = 0;
         public static int NumberOfGlasses { get; set; } = 8;
         public static int NumberOfSeats { get; set; } = 9;
     
         public DateTime TimeStamp { get; set; }
     }
}
