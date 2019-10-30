using System;
using System.Collections.Generic;

namespace Lab6
{
    public partial class MainWindow
    {
        public class Patron : Bar
        {
            public static Queue<Patron> patrons = new Queue<Patron>();

            public List<string> patronNameList = new List<string>() { "Alexander", "Anders", "Andreas", "Andreé", "Andreea", "Charlotte", "Daniel", "Elvis", "Emil", "FredrikÄrAldrigHär", "Johan",
                                                                "John", "Jonas", "Karo", "Khosro", "Luna", "Marcus", "Nicklas", "Nils", "Petter", "Pontus", "Robin", "Simon", "Sofia", "Tijana",
                                                                "Tommy", "Toni", "Wilhelm"};
            public string name;
            public Patron()
            {
                Random r = new Random();
                int index = r.Next(patronNameList.Count);
                name = patronNameList[index];
            }
        }
    }
}
