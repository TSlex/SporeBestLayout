namespace SporeCity.Buildings
{
    public class Work : Building
    {
        public override (int work, int moral, int price) Calculate()
        {
            const int price = 3;
            
            var work = NearCenter ? 1 : 0;

            return (work, -1, price);
        }
        
        public override string ToString()
        {
            return "Work";
        }
    }
}