using System.Linq;

namespace SporeCity.Buildings
{
    public class Fun : Building
    {
        public override (int work, int moral) Calculate()
        {
            var moral = 1 - Neighbors.Count(building => building is Work);

            if (NearCenter)
            {
                moral += 1;
            }
            else if (!Neighbors.Any())
            {
                return (0, 0);
            }

            return (0, moral);
        }

        public override string ToString()
        {
            return "Fun";
        }
    }
}