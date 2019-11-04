using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Lab6
{
    public class Bouncer : Bar
    {
        
        public int NumberOfPatronsInBar { get; set; } = 0;
        public Bouncer()
        {
             
        }
        public void GeneratePatrons()
        {
            Thread.Sleep(5000);
            patronsQueue.Add(new Patron(false, false));
            NumberOfPatronsInBar++;
        }

        public Patron GetAPatronWhoJustEntered()
        {
            foreach (var patron in patronsQueue)
            {
                if(patron.name != null && !patron.HasWalkedToBar)
                {
                    patron.HasWalkedToBar = true;
                    return patron;
                }
            }
            return null;
        }
    }
}
