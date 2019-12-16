using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Lab6
{
    public class Bartender : Agent
    {
        public Bartender(Bar bar)
        {
            Bar = bar;
            BarController = bar.BarController;
        }

        public override void AgentCycle(Bar bar)
        {

        }

        public RunState CheckState(Bar bar)
        {

        }
    }
}
