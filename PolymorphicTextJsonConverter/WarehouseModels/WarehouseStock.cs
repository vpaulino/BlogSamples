using System;
using System.Collections;
using System.Collections.Generic;

namespace Warehouse
{
    public class WarehouseStock
    {
        public WarehouseStock()
        {

        }
        public WarehouseStock(string storeName, int stockLevel, IEnumerable<DepartmentStock> departments)
        {
            this.StoreName = storeName;
            this.stockLevel = stockLevel;
            this.Departments = departments;
        }

        public string StoreName { get; set; }

        public int stockLevel { get; set; }

        public IEnumerable<DepartmentStock> Departments { get; set; }
        

    }
}
