using System;

namespace Lab6
{

    public class Waitress : Bar
    {
        public int ClearTheTables()
        {
            int glassesOnTablesFound = glassesOnTables.Count;
            int glass = 1;
            for (int i = 0; i < glassesOnTablesFound; i++)
            {
                glassesOnTables.TryTake(out glass);
            }
            return glassesOnTablesFound;
        }
        public void CleanGlasses(int numberOfGlasses)
        {
            for (int i = 0; i < numberOfGlasses; i++)
            {
                shelfForGlasses.Add(1);
            }
        }
    }

}
