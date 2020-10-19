using System.Collections.Generic;
using Warehouse;

namespace Warehouse
{
    public class Laptop : Product
    {

        public Laptop()
        {

        }
        public Laptop(string name, string owner, decimal pricePerUnit, string unit, string brand, string model, decimal weight, Dictionary<string, string> features)
            : base(name, owner, pricePerUnit, unit)
        {
            this.Brand = brand;
            this.Model = model;
            this.Weight = weight;
            this.Features = features;
        }

        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Weight { get; set; }

        public Dictionary<string, string> Features { get; set; }
    }
}