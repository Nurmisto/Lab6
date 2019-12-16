using System;

namespace Lab6
{
    public class Waitress : Agent
    {
        public Waitress(Bar bar)
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
