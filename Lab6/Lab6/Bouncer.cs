using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Lab6
{
    public partial class MainWindow
    {
        public class Bouncer : Bar
        {
            public Bouncer(DateTime TimeStamp)
            {
                public string timeStamp = this.TimeStamp.ToString(); 
            }


            public void GeneratePatrons()
            {
                Task.Run(() => {
                    while (true)
                    {
                        int x = 0;
                        if (Bar.barOpen && x < 1)
                        {
                            logSsytem.Log("bóuncer let patron in");
                            Patron.patrons.Enqueue(new Patron());
                            x++;
                            Thread.Sleep(200);
                        }
                        if (Bar.barOpen = true && x > 0)
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
}
