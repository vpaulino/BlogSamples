namespace Warehouse
{
    public class Phone : Product 
    {

        public Phone(string name, string owner, decimal pricePerUnit, string unit, string brand, string model)
            : base(name, owner, pricePerUnit, unit)
        {
            this.Brand = brand;
            this.Model = model;
        }
        public Phone()
        {

        }
        public string Brand { get; set; }

        public string Model { get; set; }


    }




}