namespace Warehouse
{
    public class Book : Product 
    {

        public Book(string name, string owner, decimal pricePerUnit, string unit, string isbn, string title, string[] authors)
           : base(name, owner, pricePerUnit, unit)
        {
            this.ISBN = isbn;
            this.Title = title;
            this.Authors = authors;
        }

        public Book()
        {

        }
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string[]  Authors { get; set; }
    }




}