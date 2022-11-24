using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
namespace Entiteti_Data
{
    public class Entry: TableEntity
    {
        private string index;
        private double quantity;
        public Entry() {}

        public Entry(string index, double q)
        {
            PartitionKey = "Entry";
            RowKey = index;
            Index = index;
            Quantity = q;
        }

        public string Index { get => index; set => index = value; }
        public double Quantity { get => quantity; set => quantity = value; }
    }
}
