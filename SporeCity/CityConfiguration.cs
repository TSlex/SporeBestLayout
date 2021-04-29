using System;
using System.Collections.Generic;
using System.Linq;
using SporeCity.Buildings;

namespace SporeCity
{
    public class CityConfiguration
    {
        public IEnumerable<CityPlace> Configuration { get; }

        public CityConfiguration(ICollection<(bool nearCenter, int[] neighboursIndexes)> configuration)
        {
            CityPlace[] placement = new CityPlace[configuration.Count];

            //create placement
            foreach (var (place, i) in configuration.Select((tuple, i) => (tuple, i)))
            {
                placement[i] = new CityPlace {NearCenter = place.nearCenter};
            }

            //configure neighbours
            foreach (var (place, i) in configuration.Select((tuple, i) => (tuple, i)))
            {
                placement[i].Neighbours = place.neighboursIndexes.Select(index => placement[index]);
            }

            Configuration = placement;
        }
    }
}