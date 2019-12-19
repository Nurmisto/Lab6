using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public enum RunState
    {
        Idling,
        Working,
        WalkingToBar,
        WaitingForBeer,
        WaitingForChair,
        WalkingToChair,
        DrinkingBeer,
        LeavingThePub
    }
    public abstract class Agent
    {

        public bool hasGoneHome = false;
        public bool IsBuss = false;
        public BarController BarController { get; set; }
        public Bar Bar { get; set; }
        public abstract void AgentCycle(Bar bar);
        public void Run(Bar bar)
        {
            
            Task.Run(() => AgentCycle(bar));
        }
    }
}
