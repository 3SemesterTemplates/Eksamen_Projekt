using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SOAP_Coins
{
    [DataContract]
    public class Coin
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public String Genstand { get; set; }
        [DataMember]
        public int MinPris { get; set; }
        [DataMember]
        public int Bud { get; set; }
        [DataMember]
        public String Navn { get; set; }

        public Coin()
        {

        }

        public Coin(int id, string genstand, int minpris, int bud, string navn)
        {
            Id = id;
            Genstand = genstand;
            MinPris = minpris;
            Bud = bud;
            Navn = navn;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Genstand)}: {Genstand}, {nameof(MinPris)}: {MinPris}, {nameof(Bud)}: {Bud}, {nameof(Navn)}: {Navn}";
        }
    }
}