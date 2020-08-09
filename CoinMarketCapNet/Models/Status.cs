using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCapNet.Models
{
    public class Status
    {
        public DateTime Timestamp { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public int Elapsed { get; set; }

        public int CreditCount { get; set; }
    }
}
