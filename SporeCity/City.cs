using System;
using System.Linq;

namespace SporeCity
{
    public class City
    {
        private int WorkUnits { get; set; }
        private int MoralUnits { get; set; }
        
        private CityConfiguration Configuration { get; set; }

        public City(CityConfiguration configuration)
        {
            Configuration = configuration;
        }

        public (int work, int moral) Calculate()
        {
            WorkUnits = 0;
            MoralUnits = 0;
            
            foreach (var building in Configuration.Placement.Where(building => building != null))
            {
                var (work, moral) = building!.Calculate();
                WorkUnits += work;
                MoralUnits += moral;
            }

            return (WorkUnits, MoralUnits);
        }
    }
}