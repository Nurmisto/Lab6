using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Lab6
{
    public class Bouncer : Bar
    {
        public Bouncer()
        {
             
        }
        public void GeneratePatrons()
        {
            Task.Run(() => {
                int x = 0;
                while (barOpen)
                {
                    if (barOpen && x < 1)
                    {
                        //logSsytem.Log("bóuncer let patron in");
                        patronsQue.Enqueue(new Patron());
                        x++;
                        Thread.Sleep(200);
                    }
                    else
                    {
                        Random newPatronTimer = new Random();
                        Thread.Sleep(newPatronTimer.Next(200, 1000));
                        patronsQue.Enqueue(new Patron());
                        x++;
                    }
                }
            });
        }
    }
}
