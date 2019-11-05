using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Lab6
{
    public class Bartender : Bar
    {
        public Bartender()
        {

        }

        public Patron ServePatronBeer()
        {
            Thread.Sleep(2000);
            foreach (var patron in Bar.patronsQueue)
            {
                if (!patron.HasBeenServedBeer)
                {
                    if (NumberOfGlasses > 0)
                    {
                        NumberOfGlasses--;
                        return patron;
                    }
                }
            }
            return null;
        }
        public string PatronAboutToBeServed()
        {
            Thread.Sleep(3000);
            foreach (var patron in Bar.patronsQueue)
            {
                if (!patron.HasBeenServedBeer)
                {
                    patron.HasBeenServedBeer = true;
                    return patron.name;
                }
            }

            return null;
        }

        internal void StartWorking()
        {

        }
    }
}
