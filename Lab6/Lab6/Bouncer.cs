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
                while (true)
                {
                    if (barOpen && x < 1)
                    {
                        //logSsytem.Log("bóuncer let patron in");

                        x++;
                        Thread.Sleep(200);
                    }
                    if (barOpen = true && x >= 1)
                    {
                        Random newPatronTimer = new Random();
                        Thread.Sleep(newPatronTimer.Next(200, 1000));
                        
                        x++;
                    }
                }
            });
        }
    }
}
