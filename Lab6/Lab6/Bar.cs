﻿using System;
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
        public  bool barOpen = false;
        public int NumberOfPatronsInBar { get; set; } = 0;
        public int NumberOfGlasses { get; set; } = 8;
        public int NumberOfSeats { get; set; } = 9;
        public double TimeUntillBarCloses;

        public DateTime TimeStamp { get; set; }

        public Bar()
        {
            
        }

        public void StartAgents()
        {
            Bouncer bouncer = new Bouncer();
        }
    }
}