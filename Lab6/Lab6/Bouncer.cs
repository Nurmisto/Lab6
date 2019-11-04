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
        public Queue<Patron> patronsQueue = new Queue<Patron>();
        public int NumberOfPatronsInBar { get; set; } = 0;
        public Bouncer()
        {
             
        }
        public void GeneratePatrons()
        {

            //Thread.Sleep(3000);
            patronsQueue.Enqueue(new Patron(false, false));
            NumberOfPatronsInBar++;
            //MessageBox.Show(patronsQueue.Count.ToString());

        }

        public Patron GetAPatronWhoJustEntered()
        {
            foreach (var patron in patronsQueue)
            {
                if(patron.name != null && !patron.PatronWalkedToBar)
                {
                    patron.PatronWalkedToBar = true;
                    return patron;
                }
            }
            return null;
        }
    }
}
