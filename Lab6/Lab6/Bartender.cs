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
        public Patron PatronAboutToBeServed()
        {
            var query = from patron in Bar.patronsQueue
                        where patron.HasBeenServedBeer == false && patron.HasWalkedToBar
                        select patron;
            foreach (var patron in query)
            {
                if (!patron.HasBeenServedBeer && patron.HasWalkedToBar)
                {
                    int glass = 1;
                    if(shelfForGlasses.TryTake(out glass))
                    {
                        glassesOnTables.Add(glass);
                        patron.HasBeenServedBeer = true;
                        return patron;
                    }                    
                }
            }
            return null;
        }

        
    }
}
