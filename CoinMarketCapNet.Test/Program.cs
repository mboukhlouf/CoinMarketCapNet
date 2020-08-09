using CoinMarketCapNet.JsonNamingPolicies;
using System;
using System.Threading.Tasks;

namespace CoinMarketCapNet.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new CoinMarketCapClient("<api-key>");

            // Get mapping of all cryptocurrencies
            var cryptocurrencies = await client.GetCoinMarketCapIdMapAsync();

            // Get mapping of the first 10 of cryptocurrencies
            var cryptocurrenciesPage1 = await client.GetCoinMarketCapIdMapAsync(new { start = 1, limit = 10 });

            // Get latest listings
            var listingsLatest = await client.GetListingsLatest();
        }
    }
}
