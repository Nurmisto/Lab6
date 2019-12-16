using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Lab6
{
    public class Bouncer : Agent
    {
        
        
        public Bouncer(Bar bar)
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
