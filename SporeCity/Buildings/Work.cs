namespace SporeCity.Buildings
{
    public class Work : Building
    {
        public override (int work, int moral) Calculate()
        {
            var work = NearCenter ? 1 : 0;

            return (work, -1);
        }
        
        public override string ToString()
        {
            return "Work";
        }
    }
}