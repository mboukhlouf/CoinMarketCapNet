using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCapNet.Models
{
    public class Quote
    {
        public decimal Price { get; set; }

        public decimal Volume24h { get; set; }

        public decimal PercentChange1h { get; set; }

        public decimal PercentChange24h { get; set; }

        public decimal PercentChange7d { get; set; }

        public decimal MarketCap { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
