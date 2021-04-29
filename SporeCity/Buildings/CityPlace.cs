using System.Collections.Generic;

namespace SporeCity.Buildings
{
    public class CityPlace
    {
        public Building? Building { get; set; }
        public IEnumerable<CityPlace> Neighbours { get; set; }
        public bool NearCenter { get; set; }

        public CityPlace()
        {
            Neighbours = new List<CityPlace>();
            Building = null;
        }
    }
}