using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entiteti_Data;
using Entiteti_Data.BLOB;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Diagnostics;
using Contracts;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebRole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        BlobHelper blobHelper = new BlobHelper();

        [HttpPost]
        public ActionResult MetodaBill()
        {
            string index = Request["indexBill"];
            string dateTime = Request["dateTime"];

            if(index == "" || dateTime == "")
            {
                ViewBag.Message1 = "Popunite polja index i DateTime!";
                return View("Index");
            }
            //upis u blob
            string text = blobHelper.DownloadFromBlob($"blob_{index}");
            text += $"{index}_{Convert.ToDateTime(dateTime)},";
            blobHelper.UploadToBlob($"blob_{index}", text);            

            //komunikacija sa worker rolom
            IInputConnection proxy = Connect();

            if (Request.Form["dodaj"] != null) { } //Task.Factory.StartNew(() => { proxy.Send(1, index, dateTime, 1); });
            else if (Request.Form["obrisi"] != null) { }//Task.Factory.StartNew(() => { proxy.Send(1, index, dateTime, 2); });
            else if (Request.Form["izmeni"] != null) { }//Task.Factory.StartNew(() => { proxy.Send(1, index, dateTime, 3); });
            else if (Request.Form["izlistaj"] != null)
            {
                //Task.Factory.StartNew(() => { proxy.Send(1, index, dateTime, 4); });
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult MetodaEntry()
        {
            string index = Request["indexEntry"];
            string quantity = Request["quantity"];

            if (index == null || quantity == null)
            {
                ViewBag.Message1 = "Popunite polja index i datetime!";
                return View("Index");
            }

            //upis u blob
            string text = blobHelper.DownloadFromBlob($"blob_{index}");
            text += $"{index}_{double.Parse(quantity)},";
            blobHelper.UploadToBlob($"blob_{index}", text);

            //komunikacija sa worker rolom
            IInputConnection proxy = Connect();

            if (Request.Form["dodaj"] != null) { } //Task.Factory.StartNew(() => { proxy.Send(2, index, quantity, 1); });
            else if (Request.Form["obrisi"] != null) { } //Task.Factory.StartNew(() => { proxy.Send(2, index, quantity, 2); });
            else if (Request.Form["izmeni"] != null) { } //Task.Factory.StartNew(() => { proxy.Send(2, index, quantity, 3); });
            else if (Request.Form["izlistaj"] != null)
            {
                //Task.Factory.StartNew(() => { proxy.Send(2, index, quantity, 4); });
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult MetodaProduct()
        {
            string name = Request["name"];
            string price = Request["price"];

            if (name == null || price == null)
            {
                ViewBag.Message1 = "Popunite polja index i datetime!";
                return View("Index");
            }

            //upis u blob
            string text = blobHelper.DownloadFromBlob($"blob_{name}");
            text += $"{name}_{double.Parse(price)},";
            blobHelper.UploadToBlob($"blob_{name}", text);

            //komunikacija sa worker rolom
            IInputConnection proxy = Connect();

            if (Request.Form["dodaj"] != null) { }// Task.Factory.StartNew(() => { proxy.Send(3, name, price, 1); });
            else if (Request.Form["obrisi"] != null) { }// Task.Factory.StartNew(() => { proxy.Send(3, name, price, 2); });
            else if (Request.Form["izmeni"] != null) { }// Task.Factory.StartNew(() => { proxy.Send(3, name, price, 3); });
            else if (Request.Form["izlistaj"] != null)
            {
               // Task.Factory.StartNew(() => { proxy.Send(3, name, price, 4); });
            }


            return View("Index");
        }

        private IInputConnection Connect()
        {
            NetTcpBinding binding = new NetTcpBinding()
            {
                SendTimeout = new TimeSpan(0, 10, 0),
                OpenTimeout = new TimeSpan(0, 10, 0),
                CloseTimeout = new TimeSpan(0, 10, 0),
                ReceiveTimeout = new TimeSpan(0, 10, 0),

            };
            String remoteAddress = $"net.tcp://localhost:10100/InputRequest";
            return new ChannelFactory<IInputConnection>(binding, remoteAddress).CreateChannel();
        }
    }
}