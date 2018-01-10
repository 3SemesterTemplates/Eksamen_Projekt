using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SOAP_Coins
{    
    public class Service1 : IService1
    {
        #region CONNECTION STRING
        private const string ConnectionString =
          "Server=tcp:natascha.database.windows.net,1433;Initial Catalog=School;Persist Security Info=False;User ID=nataschajakobsen;Password=Roskilde4000;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion

        #region ADD COIN (Virker)
        public int AddCoin(string genstand, byte minipris, byte bid, string name)
        {
            const string sql = "insert into Coins (Genstand, Minpris, Bud, Navn) values (@Genstand, @Minpris, @Bud, @Navn)";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand insertCommand = new SqlCommand(sql, conn))
                {
                    insertCommand.Parameters.AddWithValue("@Genstand", genstand);
                    insertCommand.Parameters.AddWithValue("@Minpris", minipris);
                    insertCommand.Parameters.AddWithValue("@Bud", bid);
                    insertCommand.Parameters.AddWithValue("@Navn", name);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
        #endregion

        #region GET COINS (Virker)
        public List<Coin> GetCoins()
        {
            const string sql = "select * from Coins order by id";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand selectCommand = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Coin> coinList = new List<Coin>();
                        while (reader.Read())
                        {
                            Coin mont = ReadCoins(reader);
                            coinList.Add(mont);
                        }
                        return coinList;
                    }
                }
            }

        }
        #endregion

        #region GET ONE COIN (Virker)
        public Coin GetOneCoin(string id)
        {
            Coin coins = new Coin();

            int idint = Convert.ToInt32(id);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                String sql = "SELECT * FROM Coins WHERE ID = @id";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@id", idint);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    coins.Id = reader.GetInt32(0);
                    coins.Genstand = reader.GetString(1);
                    coins.MinPris = reader.GetInt32(2);
                    coins.Bud = reader.GetInt32(3);
                    coins.Navn = reader.GetString(4);
                }
                return coins;
            }

           
            }
        #endregion

        #region ReadCoins
        private static Coin ReadCoins(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string genstand = reader.GetString(1);
            int minpris = reader.GetInt32(2);
            int bud = reader.GetInt32(3);
            string navn = reader.GetString(4);

            Coin monts = new Coin
            {
                Id = id,
                Genstand = genstand,
                MinPris = minpris,
                Bud = bud,
                Navn = navn,

            };
            return monts;
        }
        #endregion
    }
}
