using System.Collections.Generic;

namespace SporeCity.Buildings
{
    public abstract class Building
    {
        public List<Building?> Neighbors { get; set; }
        public bool NearCenter { get; set; }

        public Building()
        {
            Neighbors = new List<Building?>();
        }

        public abstract (int work, int moral, int price) Calculate();
    }
}