using System;
using System.Collections.Generic;
using System.Linq;
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

        public Patron GetPatronAboutToBeServed()
        {
            Thread.Sleep(2000);
            foreach (var patron in Bar.patronsQueue)
            {
                if (!patron.HasBeenServedBeer && patron.HasWalkedToBar)
                {
                    return patron;
                }
            }
            return null;
        }
        public string PatronAboutToBeServed()
        {
            var query = from patron in Bar.patronsQueue
                        where patron.HasBeenServedBeer == false && patron.HasWalkedToBar
                        select patron;
            foreach (var patron in query)
            {
                if (!patron.HasBeenServedBeer)
                {
                    NumberOfGlasses--;
                    patron.HasBeenServedBeer = true;
                    return patron.name;
                }
            }
            return null;
            
            //foreach (var patron in Bar.patronsQueue)
            //{
            //    if (!patron.HasBeenServedBeer)
            //    {
            //        if (NumberOfGlasses > 0)
            //        {
            //            NumberOfGlasses--;
            //            patron.HasBeenServedBeer = true;
            //            return patron.name;
            //        }
            //    }
            //}
            //return null;
        }
    }
}
