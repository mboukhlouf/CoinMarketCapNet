using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCapNet.Models
{
    internal class CoinMarketCapResponse
    {
        public Status Status { get; set; }
    }

    internal class CoinMarketCapResponse<TData>
    {
        public TData Data { get; set; }

        public Status Status { get; set; }
    }
}
