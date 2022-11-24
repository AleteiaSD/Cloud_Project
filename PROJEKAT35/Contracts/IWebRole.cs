using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IWebRole
    {
        [OperationContract]
        void DodajBill(string index, DateTime dateTime);
        [OperationContract]
        void DodajEntry(string index, double quantity);
        [OperationContract]
        void DodajProduct(string name, double price);

        [OperationContract]
        void ObrisiBill(string index, DateTime dateTime);
        [OperationContract]
        void ObrisiEntry(string index, double quantity);
        [OperationContract]
        void ObrisiProduct(string name, double price);

        [OperationContract]
        void IzmeniBill(string index, DateTime dateTime);
        [OperationContract]
        void IzmeniEntry(string index, double quantity);
        [OperationContract]
        void IzmeniProduct(string name, double price);


        [OperationContract]
        void IzlistajBill();
        [OperationContract]
        void IzlistajEntry();
        [OperationContract]
        void IzlistajProduct();
    }
}
