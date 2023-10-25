using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisas_Desafio.Models
{
    /*
    "success": true,
 "terms": "https://currencylayer.com/terms",
 "privacy": "https://currencylayer.com/privacy",
 "timestamp": 1698117483,
 "source": "USD",
 "quotes": {
     "USDMXN": 18.12841
 }
  */
    public class ModelCurrencyLayer
    {
        public bool success { get; set; }
        public string terms { get; set; }
        public string privacy { get; set; }
        public int timestamp { get; set; }
        public string source { get; set; }
        public conversion quotes { get; set; }
    }
   
    public class conversion
    {
        public float USDMXN { get; set; }
    }
}
