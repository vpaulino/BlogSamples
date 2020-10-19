namespace Warehouse
{
    public class Furniture : Product 
    {
        public Furniture(string name, string owner, decimal pricePerUnit, string unit, Dimension dimension)
           : base(name, owner, pricePerUnit, unit)
        {
            this.Dimentions = dimension;
        }

        public Furniture()
        {

        }

        public Dimension Dimentions { get; set; }
    }




}