using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contracts;
using Entiteti_Data;
using System.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace EntityHandlerWorkerRole
{
    public class EntityServerProvider : IPorukaKlijenta, IWebRole, IInputConnection
    {
        EntitetiDataRepository repositoryEntitet = new EntitetiDataRepository();

        public bool PorukaKlijenta(int instanca)
        {
            DodajEntry("prva", 15.4);
            Trace.TraceInformation("Klijent trazi status za instancu " + instanca + ".");
            int id = int.Parse(RoleEnvironment.CurrentRoleInstance.Id.Split('_')[2]);
            if (instanca == id)
                return true;
            else
                return false;
        }
//============================================================================================
        public void Send(int klasa, string key, string podatak, int metoda)
        {
            int id = int.Parse(RoleEnvironment.CurrentRoleInstance.Id.Split('_')[2]);
            if (klasa == 1 && id == 1 || id == 3)
            {
                switch (metoda)
                {
                    case 1: DodajBill(key, Convert.ToDateTime(podatak)); break;
                    case 2: ObrisiBill(key, Convert.ToDateTime(podatak)); break;
                    case 3: IzmeniBill(key, Convert.ToDateTime(podatak)); break;
                    case 4: IzlistajBill(); break;
                }
            }

            if (klasa == 2 && id == 2)
            {
                switch (metoda)
                {
                    case 1: DodajEntry(key, double.Parse(podatak)); break;
                    case 2: ObrisiEntry(key, double.Parse(podatak)); break;
                    case 3: IzmeniEntry(key, double.Parse(podatak)); break;
                    case 4: IzlistajEntry(); break;
                }
            }

            if (klasa == 3 && id==2)
            {
                switch (metoda)
                {
                    case 1: DodajProduct(key, double.Parse(podatak)); break;
                    case 2: ObrisiProduct(key, double.Parse(podatak)); break;
                    case 3: IzmeniProduct(key, double.Parse(podatak)); break;
                    case 4: IzlistajProduct(); break;
                }
            }
        }
//============================================================================================
        public void DodajBill(string index, DateTime dateTime)
        {
            repositoryEntitet.AddBill(new Bill(index, dateTime));
        }

        public void DodajEntry(string index, double quantity)
        {            
            repositoryEntitet.AddEntry(new Entry(index, quantity));
        }

        public void DodajProduct(string name, double price)
        {
            repositoryEntitet.AddProduct(new Product(name,price));
        }
//============================================================================================
        public void IzlistajBill()
        {
            repositoryEntitet.RetrieveAllBill();
        }

        public void IzlistajEntry()
        {
            repositoryEntitet.RetrieveAllEntry();
        }

        public void IzlistajProduct()
        {
            repositoryEntitet.RetrieveAllProduct();
        }
//============================================================================================
        public void IzmeniBill(string index, DateTime dateTime)
        {
            repositoryEntitet.ReplaceBill(new Bill(index, dateTime));
        }

        public void IzmeniEntry(string index, double quantity)
        {
            repositoryEntitet.ReplaceEntry(new Entry(index, quantity));
        }

        public void IzmeniProduct(string name, double price)
        {
            repositoryEntitet.ReplaceProduct(new Product(name, price));
        }
//============================================================================================
        public void ObrisiBill(string index, DateTime dateTime)
        {
            repositoryEntitet.RemoveBill(new Bill(index, dateTime));
        }

        public void ObrisiEntry(string index, double quantity)
        {
            repositoryEntitet.RemoveEntry(new Entry(index, quantity));
        }

        public void ObrisiProduct(string name, double price)
        {
            repositoryEntitet.RemoveProduct(new Product(name, price));
        }
        //============================================================================================   

        
    }
}
