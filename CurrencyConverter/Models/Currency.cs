using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace CurrencyConverter.Models
{
    [DataContract]
    public class Currency
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<CurrencyRate> Rates { get; set; }
    }
}