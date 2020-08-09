using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CoinMarketCapNet.Endpoints
{
    internal class CryptocurrencyEndpoints
    {
        private readonly string Version = "v1";
        private readonly string Resource = "cryptocurrency";

        private string baseUrl;

        public CryptocurrencyEndpoints(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        private string Prefix => $"{baseUrl}";

        public string BaseUrl
        {
            get => baseUrl;
            set => baseUrl = value;
        }

        public Endpoint Map()
            => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/map"), HttpMethod.Get, EndpointSecurityType.ApiKey);

        public Endpoint Info()
            => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/info"), HttpMethod.Get, EndpointSecurityType.ApiKey);

        public Endpoint ListingsLatest()
            => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/listings/latest"), HttpMethod.Get, EndpointSecurityType.ApiKey);

        public Endpoint ListingsHistorical()
            => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/listings/historical"), HttpMethod.Get, EndpointSecurityType.ApiKey);

        public Endpoint QuotesLatest()
            => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/quotes/latest"), HttpMethod.Get, EndpointSecurityType.ApiKey);

        public Endpoint QuotesHistorical()
            => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/quotes/historical"), HttpMethod.Get, EndpointSecurityType.ApiKey);

        public Endpoint MarketPairsLatest()
            => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/market-pairs/latest"), HttpMethod.Get, EndpointSecurityType.ApiKey);

        public Endpoint OhlcvLatest()
        => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/ohlcv/latest"), HttpMethod.Get, EndpointSecurityType.ApiKey);

        public Endpoint OhlcvHistorical()
            => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/ohlcv/historical"), HttpMethod.Get, EndpointSecurityType.ApiKey);

        public Endpoint PricePerformanceStatsLatest()
            => new Endpoint(new Uri($"{Prefix}/{Version}/{Resource}/price-performance-stats/latest"), HttpMethod.Get, EndpointSecurityType.ApiKey);
    }
}
