
using System.Collections.Generic;
using System.Linq;
using SporeCity.Buildings;

namespace SporeCity
{
    public class City
    {
        private int WorkUnits { get; set; }
        private int MoralUnits { get; set; }
        public int Price { get; private set; }

        private IEnumerable<CityPlace> CityPlacement { get; set; }

        public City(CityConfiguration configuration)
        {
            CityPlacement = configuration.Configuration;
        }

        public (int work, int moral) Calculate(IEnumerable<BuildingType> input)
        {
            WorkUnits = 0;
            MoralUnits = 0;
            Price = 0;

            var layout = input.ToArray();
            var config = CityPlacement.ToArray();

            for (var i = 0; i < config.Length; i++)
            {
                config[i].Building = layout[i] switch
                {
                    BuildingType.House => new House(),
                    BuildingType.Work => new Work(),
                    BuildingType.Fun => new Fun(),
                    _ => null
                };
            }

            foreach (var place in config)
            {
                var building = place.Building;
                if (building == null) continue;
                
                building.Neighbors = place.Neighbours.Select(place => place.Building).ToList();
                building.NearCenter = place.NearCenter;
            }

            foreach (var building in config.Where(place => place.Building != null).Select(place => place.Building))
            {
                var (work, moral, price) = building!.Calculate();
                WorkUnits += work;
                MoralUnits += moral;
                Price += price;
            }

            return (WorkUnits, MoralUnits);
        }

        public override string ToString()
        {
            return string.Join(", ", CityPlacement.Select((place, index) => $"{index}: {place.Building}"));
        }
    }
}