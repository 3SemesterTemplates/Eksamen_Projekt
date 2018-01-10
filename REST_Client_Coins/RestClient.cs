using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using REST_Coins;
using Newtonsoft.Json;

namespace REST_Client_Coins
{
    class RestClient
    {
        public RestClient()
        {
        }

        public void Start()
        {
            #region don't need this
            //var CoinList = GetCoinAsync().Result; //resultatet af task
            //Console.WriteLine("All Coins\n" +
            //    String.Join("\n", CoinList));
            #endregion

            #region cw
            Console.WriteLine("Klik enter for at Hente");
            Console.ReadKey();
            #endregion

            #region Get Coin
            var coinList = GetCoinsAsync().Result;  //resultatet af task
            Console.WriteLine("All coins\n" + string.Join("\n", coinList)); //parametrer "seperator" + values (coinList)
            #endregion

            #region cw
            Console.WriteLine();
            Console.WriteLine("Klik enter for at tilføjet en Coin");
            #endregion

            #region Post Coin
            AddCoinAsync(new Coin(123, "Gold TEST", 36, 25, "testingNavn"));
            Console.ReadKey();
            var coinliste2 = GetCoinsAsync().Result;
            Console.WriteLine("All coins\n" + string.Join("\n", coinliste2));
            #endregion

            #region cw
            Console.WriteLine();
            Console.WriteLine("Klik enter for at hente én Coin");
            Console.ReadKey();
            #endregion

            #region Get One Coin
            var oneCoin = GetOneCoinAsync(1).Result;
            Console.WriteLine("Coin nr=" + 1 + "\n" +
                              oneCoin);
            #endregion


        }

        #region URI
        private const String uri = "http://eksamenrest.azurewebsites.net/Service1.svc"; //min uri til web service
        #endregion

        #region don't need this! :)
        //private Task<List<Coin>> GetCoin() //asynkron operation som returnere en liste af typen Coin
        //{
        //    using (HttpClient client = new HttpClient()) //Http request of response
        //    {
        //        string content = client.(uri + "/movies"); //fortæller hvor jeg finder min data/webmetode
        //        List<Coin> coinListe = JsonConvert.DeserializeObject<List<Coin>>(content); //ved serialisation convert object til string. 
        //        return coinListe; //returnere listen af Coins
        //    }
        //}
        #endregion

        #region Get 
        private async Task<IList<Coin>> GetCoinsAsync() //asynkron operation som returnere en liste af typen Coin
        {
            using (HttpClient client = new HttpClient()) //Http request of response
            {
                //Sender jeg en GET request til uri'en og returnerer det som en string.
                string content = await client.GetStringAsync(uri + "/coins");  //fortæller hvor jeg finder min data/webmetode

                //ved serialisation convert object til string. 
                IList<Coin> coinlist = JsonConvert.DeserializeObject<IList<Coin>>(content);
                return coinlist; //returnere listen af Coins
            }
        }
        #endregion

        #region Post
        private async void AddCoinAsync(Coin newcoin)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(newcoin));

                //Header: content type eks. Application.json. Vi fortæller hvad der er I body. 
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); 

                var result = await client.PostAsync(uri + "/coins", content);

            }
        }
        #endregion

        #region Get One Coin
        private async Task<Coin> GetOneCoinAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(uri + "/coins/" + id);
                Coin c = JsonConvert.DeserializeObject<Coin>(content);
                return c;
            }
        }
        #endregion

    }
}
