using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace HealthMonitoringConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var binding = new NetTcpBinding();
            ChannelFactory<IPorukaKlijenta> factory = new ChannelFactory<IPorukaKlijenta>(binding, new EndpointAddress("net.tcp://localhost:10100/InputRequest"));
            while (true)
            {
                IPorukaKlijenta proxy = factory.CreateChannel();
                Console.Write("Poruka klijenta prema serveru: ");
                int poruka = int.Parse(Console.ReadLine());
                bool stanje = proxy.PorukaKlijenta(poruka);
                if(stanje == true)
                Console.WriteLine("Stanje instance "+poruka+" je: "+stanje.ToString());
                else
                Console.WriteLine("Stanje instance " + poruka + " je: UGASENO");
            }
        }
    }
}
