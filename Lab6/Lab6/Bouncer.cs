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
            barOpen = true;
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                while (barOpen)
                {
                    Thread.Sleep(2000);
                    patronsQueue.Enqueue(new Patron());
                    NumberOfPatronsInBar++;
                    MessageBox.Show("PATRON CREATED");
                }
            });
        }
    }
}
