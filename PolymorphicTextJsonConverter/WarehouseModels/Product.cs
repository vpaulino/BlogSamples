using System.Reflection;

namespace Warehouse
{
    public class Product
    {
        public Product()
        {

        }
        public Product(string name, string owner, decimal pricePerUnit, string unit)
        {
            this.Name = name;
            this.Owner = owner;
            this.PricePerUnit = pricePerUnit;
            this.Unit = unit;
        }
        public string Name { get; set; }

        public string Owner { get; set; }

        public decimal PricePerUnit { get; set; }

        public string Unit { get; set; }
    }




}