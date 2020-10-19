using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Text.Json.Serialization.Extensions;
using Warehouse;
using Xunit;

namespace StocksSerializationTests
{
    public class JsonConverterWithTypeDescriminatorTests
    {
        [Fact]
        public void WhenSerializingProductStocks_AnStockWithCorrectProductsShouldBeReturned()
        {
            List<DepartmentStock> departmentStocks = new List<DepartmentStock>();
            var electronicProducts = new Product[] { 
                new Phone("iphone12", "productowner", 800, "cardinal", "Apple", "IPhone"),
                new Laptop("dell15", "productOwner", 3000, "cardinal", "dell","dellbrand",(decimal)1.2,new Dictionary<string, string>()) 
            };
            var departmentStock = new DepartmentStock("ownerEMail@gmail.com","electronics", electronicProducts);
            departmentStocks.Add(departmentStock);

             
            WarehouseStock expected = new WarehouseStock("Store Name", 2, departmentStocks);

            JsonSerializerOptions options = new JsonSerializerOptions();

            var descriminatorTypeMap = new Dictionary<string, Func<string, Type>>();
            descriminatorTypeMap.Add(typeof(Book).FullName, (descriminator) => typeof(Book));
            descriminatorTypeMap.Add(typeof(Phone).FullName, (descriminator) => typeof(Phone));
            descriminatorTypeMap.Add(typeof(Laptop).FullName, (descriminator) => typeof(Laptop));
            options.Converters.Add(new PolymorphicJsonConverter<Product>(descriminatorTypeMap, "$type"));

            var serialized = System.Text.Json.JsonSerializer.Serialize(expected, options);

            Assert.NotNull(serialized);

            WarehouseStock actual = System.Text.Json.JsonSerializer.Deserialize<WarehouseStock>(serialized, options);

            Assert.IsType<Phone>(actual.Departments.ElementAt(0).Products.ElementAt(0));
            Assert.IsType<Laptop>(actual.Departments.ElementAt(0).Products.ElementAt(1));
            
        }
         
    }
}
