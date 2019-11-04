using System;
using System.Collections.Generic;
using System.Threading;
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
            foreach (var patron in patronsQueue)
            {
                if (!patron.HasBeenServedBeer)
                {
                    if (NumberOfGlasses > 0)
                    {
                        Thread.Sleep(3000);
                        NumberOfGlasses--;
                        patron.HasBeenServedBeer = true;
                        return patron;
                    }
                }
            }
            return null;
        }

    }
}
