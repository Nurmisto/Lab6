using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab6
{
    public class Bartender : Bar
    {
        public Bartender()
        {

        }

        public string GetNextPatron(string patron)
        {
            Thread.Sleep(200);
            return patron;
        }

    }
}
