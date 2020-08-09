using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCapNet.Endpoints
{
    internal class EndpointsManager
    {
        private string baseUrl;

        public EndpointsManager(string baseUrl)
        {
            this.baseUrl = baseUrl;
            Cryptocurrency = new CryptocurrencyEndpoints(baseUrl);
        }

        public string BaseUrl
        {
            get => baseUrl;
            set
            {
                baseUrl = value;
                Cryptocurrency.BaseUrl = baseUrl;
            }
        }

        public CryptocurrencyEndpoints Cryptocurrency { get; }
    }
}
