using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Glass
    {
        public bool IsClean { get; set; }
        public bool HasBeer { get; set; }

        public Glass()
        {
            IsClean = true;
            HasBeer = false;
        }
    }
}
