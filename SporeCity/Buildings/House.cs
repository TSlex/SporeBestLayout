using System.Linq;

namespace SporeCity.Buildings
{
    public class House : Building
    {
        public override (int work, int moral) Calculate()
        {
            var work = Neighbors.Count(building => building is Work);
            var fun = Neighbors.Count(building => building is Fun);

            return (work, fun);
        }
        
        public override string ToString()
        {
            return "House";
        }
    }
}