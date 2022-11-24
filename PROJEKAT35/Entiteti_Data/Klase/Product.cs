using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace Entiteti_Data
{
    public class Product: TableEntity
    {
        private string name;
        private double price;
        public Product(){}
        public Product(string name, double price)
        {
            PartitionKey = "Product";
            RowKey = name;
            Name = name;
            Price = price;
        }

        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
    }
}
