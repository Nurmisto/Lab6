using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab6
{
    public partial class MainWindow
    {
        public class Bartender
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
}
