using System.Collections;
using System.Collections.Generic;

namespace Warehouse
{
    public class DepartmentStock
    {
        public DepartmentStock()
        {

        }

        public DepartmentStock(string ownerEmail, string departmentName, IEnumerable<Product> products)
        {
            this.OwnerEmail = ownerEmail;
            this.DepartmentName = departmentName;
            this.Products = products;
        }
        public IEnumerable<Product> Products { get; set; }

        public string DepartmentName { get; set; }

        public string OwnerEmail { get; set; }
    }
}