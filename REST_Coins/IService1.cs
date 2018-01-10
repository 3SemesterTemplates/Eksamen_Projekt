using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace REST_Coins
{
    //Service contract specificerer hvilke operationer/metoder/funktioner som servicen understøtter.
    [ServiceContract]
    public interface IService1
    {
        //HTTP
        //Get all
        [OperationContract] //Operation Contract. Web service metode
        [WebInvoke(
                Method = "GET",
                ResponseFormat = WebMessageFormat.Json,
                UriTemplate = "coins")
        ]
        List<Coin> GetCoins();

        //Get by id
        [WebInvoke(
                Method = "GET",
                ResponseFormat = WebMessageFormat.Json,
                UriTemplate = "coins/{id}")
        ]
        Coin GetOneCoin(String id);


        //Post/create
        [WebInvoke(
                Method = "POST",
                RequestFormat = WebMessageFormat.Json,
                //ResponseFormat = WebMessageFormat.Json,
                UriTemplate = "coins")
        ]
        void AddCoin(Coin newCoin);

    }


    
}
