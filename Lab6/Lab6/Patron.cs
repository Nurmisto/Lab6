using System;
using System.Collections.Generic;

namespace Lab6
{
    public class Patron : Bar
    {
        public List<string> patronNameList = new List<string>() { "Alexander", "Anders", "Andreas", "Andreé", "Andreea", "Charlotte", "Daniel", "Elvis", "Emil", "FredrikÄrAldrigHär", "Johan",
                                                                "John", "Jonas", "Karo", "Khosro", "Luna", "Marcus", "Nicklas", "Nils", "Petter", "Pontus", "Robin", "Simon", "Sofia", "Tijana",
                                                                "Tommy", "Toni", "Wilhelm"};
        public string name;
        

        public Patron(bool patronWalkedToBar, bool hasBeenServedBeer)
        {
            HasWalkedToBar = patronWalkedToBar;
            HasBeenServedBeer = hasBeenServedBeer;
            Random r = new Random();
            int index = r.Next(patronNameList.Count);
            name = patronNameList[index];
        }

    }
}
