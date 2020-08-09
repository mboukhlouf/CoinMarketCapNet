using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCapNet.Models
{
    public class Platform
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Slug { get; set; }

        public string TokenAddress { get; set; }
    }
}
