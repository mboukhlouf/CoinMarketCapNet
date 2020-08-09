using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCapNet.Models
{
    public class Listing
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Slug { get; set; }

        public int CmcRank { get; set; }

        public int NumMarketPairs { get; set; }

        public decimal CirculatingSupply { get; set; }

        public decimal TotalSupply { get; set; }

        public decimal? MaxSupply { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime DateAdded { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public Platform Platform { get; set; }

        public Dictionary<string, Quote> Quote { get; set; }
    }
}
