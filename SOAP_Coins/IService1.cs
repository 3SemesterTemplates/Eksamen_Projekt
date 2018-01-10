using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SOAP_Coins
{
    //Service contract specificerer hvilke operationer/metoder/funktioner som servicen understøtter.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Coin> GetCoins(); 

        [OperationContract]
        int AddCoin(string genstand, byte minipris, byte bid, string name);


        [OperationContract]
        Coin GetOneCoin(String id);

    }

}
