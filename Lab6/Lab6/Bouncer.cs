using System;
using System.Windows;

namespace Lab6
{
    public partial class MainWindow
    {
        public class Bouncer
        {



            public void GeneratePatrons()
            {
                Patron.patrons.Enqueue(new Patron());

            }
        }
    }
}
