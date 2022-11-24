using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace Entiteti_Data
{
    public class Bill : TableEntity
    {
        private string index;
        private DateTime creationTime;

        public string Index { get => index; set => index = value; }
        public DateTime CreationTime { get => creationTime; set => creationTime = value; }

        public Bill()
        {
        }
        public Bill(string index, DateTime dateTime)
        {
            PartitionKey = "Bill";
            RowKey = index;
            Index = index;
            CreationTime = dateTime;
        }
        
    }
}
