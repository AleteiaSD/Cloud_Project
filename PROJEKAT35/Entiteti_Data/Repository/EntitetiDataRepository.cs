using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteti_Data
{
    public class EntitetiDataRepository
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public EntitetiDataRepository()
        {
            _storageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("AzureTable");
            _table.CreateIfNotExists();
        }

        public void AddBill(Bill newBill)
        {
            TableOperation insertOperation1 = TableOperation.Insert(newBill);
            _table.Execute(insertOperation1);
        }
        public void AddEntry(Entry newEntry)
        {
            TableOperation insertOperation2 = TableOperation.Insert(newEntry);
            _table.Execute(insertOperation2);
        }
        public void AddProduct(Product newProduct)
        {
            TableOperation insertOperation3 = TableOperation.Insert(newProduct);
            _table.Execute(insertOperation3);
        }


        public void RemoveBill(Bill b)
        {
            TableOperation removeOperation1 = TableOperation.Delete(b);          
            _table.Execute(removeOperation1);
        }
        public void RemoveEntry(Entry e)
        {
            TableOperation removeOperation2 = TableOperation.Delete(e);
            _table.Execute(removeOperation2);
        }

        public void RemoveProduct(Product p)
        {
            TableOperation removeOperation3 = TableOperation.Delete(p);
            _table.Execute(removeOperation3);
        }


        public void ReplaceBill(Bill b)
        {
            TableOperation operation1 = TableOperation.Replace(b);
            _table.Execute(operation1);
        }
        public void ReplaceEntry( Entry e)
        {
            TableOperation operation2 = TableOperation.Replace(e);
            _table.Execute(operation2);
        }
        public void ReplaceProduct(Product p)
        {
            TableOperation operation3 = TableOperation.Replace(p);
            _table.Execute(operation3);
        }
        
        public IQueryable<Bill> RetrieveAllBill()
        {
            var results = from g in _table.CreateQuery<Bill>()
                          where g.PartitionKey == "Bill"
                          select g;
            return results;
        }
        public IQueryable<Entry> RetrieveAllEntry()
        {
            var results = from g in _table.CreateQuery<Entry>()
                          where g.PartitionKey == "Entry"
                          select g;
            return results;
        }
        public IQueryable<Product> RetrieveAllProduct()
        {
            var results = from g in _table.CreateQuery<Product>()
                          where g.PartitionKey == "Product"
                          select g;
            return results;
        }


    }
}
