namespace Warehouse
{
    public class Dimension
    {
        public Dimension(decimal hight, decimal deep, decimal weight)
        {
            this.Hight = hight;
            this.Deep = deep;
            this.Weight = weight;
        }

        public decimal? Hight { get; set; }

        public decimal? Deep { get; set; }

        public decimal? Weight { get; set; }

    }
}