using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCapNet.Models
{
    public class Cryptocurrency
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Slug { get; set; }

        public int IsActive { get; set; }

        public DateTime FirstHistoricalData { get; set; }

        public DateTime LastHistoricalData { get; set; }

        public Platform Platform { get; set; }
    }
}
