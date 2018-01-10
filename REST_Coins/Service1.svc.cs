using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace REST_Coins
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

        //færdig
    public class Service1 : IService1
    {
        #region Connection string
        //Data Source=natascha.database.windows.net;Initial Catalog=School;Integrated Security=False;User ID=nataschajakobsen;Password=********;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        private static string connectingString =
               "Server=tcp:natascha.database.windows.net,1433;Initial Catalog=School;Persist Security Info=False;User ID=nataschajakobsen;Password=Roskilde4000;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion

        #region POST 
        public void AddCoin(Coin newCoin)
        {
            SqlConnection conn = new SqlConnection(connectingString); //laver en ny instans af SqlConnection og kalder den conn.
            SqlCommand command = new SqlCommand(); //ny instans af SqlCommand og kalder den command
            command.Connection = conn;
            conn.Open(); //åbnes forbindelsen 

            command.CommandText = @"INSERT INTO Coins(Genstand, Minpris, Bud, Navn) 
                                VALUES (@Genstand, @Minpris, @Bud, @Navn)";

            command.Parameters.AddWithValue("@Genstand", newCoin.Genstand);
            command.Parameters.AddWithValue("@Minpris", newCoin.MinPris);
            command.Parameters.AddWithValue("@Bud", newCoin.Bud);
            command.Parameters.AddWithValue("@Navn", newCoin.Navn);

            command.ExecuteNonQuery(); //udfører SQL statement "command"
            conn.Close(); //lukker for forbindelsen
        }
        #endregion

        #region GET (Virker)
        public List<Coin> GetCoins()
        {
            List<Coin> liste = new List<Coin>(); //ny instans af coin   
            using (SqlConnection conn = new SqlConnection(connectingString))
            {
                conn.Open();
                String sql = "SELECT * FROM Coins";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Coin mont = new Coin
                    {
                        Id = reader.GetInt32(0),
                        Genstand = reader.GetString(1),
                        MinPris = reader.GetInt32(2),
                        Bud = reader.GetInt32(3),
                        Navn = reader.GetString(4),
                    };
                    liste.Add(mont);
                }

            }
            return liste;
        }
        #endregion

        #region GET ONE COIN (Virker)
        public Coin GetOneCoin(string id)
        {
            Coin coin = new Coin(); //ny instans af Coin
            int dint = Int32.Parse(id);
            using (SqlConnection conn = new SqlConnection(connectingString)) //ny instans ad sqlconnection 
            {
                conn.Open(); //åbner for forbindelsen
                String sql = "SELECT * FROM Coins WHERE Id = @id"; //sql statement
                SqlCommand command = new SqlCommand(sql, conn); //ny instans af sqlcommand
                command.Parameters.AddWithValue("@id", dint); //til
                SqlDataReader reader = command.ExecuteReader(); //Sender min command til min connection  og kører Datareader (rows osv.)

                while (reader.Read())
                {
                    coin.Id = reader.GetInt32(0);
                    coin.Genstand = reader.GetString(1);
                    coin.MinPris = reader.GetInt32(2);
                    coin.Bud = reader.GetInt32(3);
                    coin.Navn = reader.GetString(4);
                }
            }
            return coin;
        }
        #endregion
    }
}
