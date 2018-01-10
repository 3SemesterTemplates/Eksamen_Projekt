using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using REST_Coins;

namespace REST_Client_Coins
{
    class Program
    {
        static void Main(string[] args)
        {
            RestClient client = new RestClient();
            client.Start();

            Console.ReadLine();
        }
    }
}
