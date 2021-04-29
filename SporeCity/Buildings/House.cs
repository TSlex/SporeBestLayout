using System.Linq;

namespace SporeCity.Buildings
{
    public class House : Building
    {
        public override (int work, int moral, int price) Calculate()
        {
            const int price = 4;
            
            var work = Neighbors.Count(building => building is Work);
            var fun = Neighbors.Count(building => building is Fun);

            return (work, fun, price);
        }
        
        public override string ToString()
        {
            return "House";
        }
    }
}