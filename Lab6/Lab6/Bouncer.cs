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
                    while (true)
                    {
                        int x = 0;
                        if (barOpen && x < 1)
                        {
                            //logSsytem.Log("bóuncer let patron in");
                            patron.patronsQue.Enqueue(new Patron());
                            x++;
                            Thread.Sleep(200);
                        }
                        if (barOpen = true && x > 0)
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
