using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IInputConnection
    {
        [OperationContract]
        void Send(int klasa, string key, string podatak,int metoda);
    }
}
