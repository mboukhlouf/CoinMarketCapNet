using CoinMarketCapNet.Endpoints;
using CoinMarketCapNet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoinMarketCapNet
{
    public class CoinMarketCapClient : IDisposable
    {
        private static readonly string DefaultBaseUrl = "https://pro-api.coinmarketcap.com";

        private readonly EndpointsManager endpointsManager;
        private readonly ApiProcessor apiProcessor;

        private bool disposed = false;

        public CoinMarketCapClient() : this(null, DefaultBaseUrl)
        {
        }

        public CoinMarketCapClient(string key) : this(key, DefaultBaseUrl)
        {
        }

        public CoinMarketCapClient(string key, string baseUrl)
        {
            endpointsManager = new EndpointsManager(baseUrl);
            apiProcessor = new ApiProcessor();
            Key = key;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    apiProcessor.Dispose();
                }

                disposed = true;
            }
        }

        ~CoinMarketCapClient()
        {
            Dispose(false);
        }

        public string Key
        {
            get => apiProcessor.Key;
            set => apiProcessor.Key = value;
        }

        /// <summary>
        /// The status object from the response of the last request
        /// </summary>
        public Status Status
        {
            get => apiProcessor.Status;
        }

        #region Cryptocurrency
        public Task<IEnumerable<Cryptocurrency>> GetCoinMarketCapIdMapAsync(object queryParameters = null)
        {
            var endpoint = endpointsManager.Cryptocurrency.Map();
            return apiProcessor.ProcessRequestAsync<IEnumerable<Cryptocurrency>>(endpoint, queryParameters, null);
        }

        public Task<IEnumerable<Listing>> GetListingsHistorical(object queryParameters = null)
        {
            var endpoint = endpointsManager.Cryptocurrency.ListingsHistorical();
            return apiProcessor.ProcessRequestAsync<IEnumerable<Listing>>(endpoint, queryParameters, null);
        }

        public Task<IEnumerable<Listing>> GetListingsLatest(object queryParameters = null)
        {
            var endpoint = endpointsManager.Cryptocurrency.ListingsLatest();
            return apiProcessor.ProcessRequestAsync<IEnumerable<Listing>>(endpoint, queryParameters, null);
        }
        #endregion
    }
}
