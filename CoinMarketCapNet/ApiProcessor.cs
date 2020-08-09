using CoinMarketCapNet.Endpoints;
using CoinMarketCapNet.Exceptions;
using CoinMarketCapNet.JsonNamingPolicies;
using CoinMarketCapNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CoinMarketCapNet
{
    internal class ApiProcessor : IDisposable
    {
        private static readonly string AuthorizationHeader = "X-CMC_PRO_API_KEY";

        private static JsonSerializerOptions JsonOptions;

        private readonly HttpClientHandler httpClientHandler;
        private readonly HttpClient httpClient;

        private bool disposed = false;

        static ApiProcessor()
        {
            ConfigureJsonOptions();
        }

        public ApiProcessor()
        {
            httpClientHandler = new HttpClientHandler()
            {
                UseCookies = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
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
                    httpClientHandler.Dispose();
                    httpClient.Dispose();
                }

                disposed = true;
            }
        }

        ~ApiProcessor()
        {
            Dispose(false);
        }

        private static void ConfigureJsonOptions()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                IgnoreNullValues = true
            };
        }

        public string Key { get; set; }

        /// <summary>
        /// The status object from the response of the last request
        /// </summary>
        public Status Status { get; private set; }

        private Task<HttpResponseMessage> CreateRequestAsync(Endpoint endpoint)
            => CreateRequestAsync(endpoint, null, null, CancellationToken.None);

        private Task<HttpResponseMessage> CreateRequestAsync(Endpoint endpoint, object queryParameters, object requestObject)
            => CreateRequestAsync(endpoint, queryParameters, requestObject, CancellationToken.None);

        private Task<HttpResponseMessage> CreateRequestAsync(Endpoint endpoint, object queryParameters, object requestObject, CancellationToken cancellationToken)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();

            // Auth
            if (endpoint.SecurityType == EndpointSecurityType.ApiKey)
            {
                requestMessage.Headers.Add(AuthorizationHeader, $"{Key}");
            }

            if (queryParameters != null)
            {
                UriBuilder uriBuilder = new UriBuilder(endpoint.Uri)
                {
                    Query = GenerateQueryString(queryParameters)
                };
                requestMessage.RequestUri = uriBuilder.Uri;
            }
            else
            {
                requestMessage.RequestUri = endpoint.Uri;
            }

            if (requestObject != null)
            {
                string content = JsonSerializer.Serialize(requestObject, JsonOptions);
                requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            requestMessage.Method = endpoint.Method;

            var task = httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            task.ContinueWith(_ => requestMessage.Dispose());
            return task;
        }

        public Task ProcessRequestAsync(Endpoint endpoint)
        {
            return ProcessRequestAsync(endpoint, null, null, CancellationToken.None);
        }

        public Task ProcessRequestAsync(Endpoint endpoint, object queryParameters, object requestObject)
        {
            return ProcessRequestAsync(endpoint, queryParameters, requestObject, CancellationToken.None);
        }

        public async Task ProcessRequestAsync(Endpoint endpoint, object queryParameters, object requestObject, CancellationToken cancellationToken)
        {
            var message = await CreateRequestAsync(endpoint, queryParameters, requestObject, cancellationToken).ConfigureAwait(false);
            if (message.IsSuccessStatusCode)
            {
                return;
            }
            throw await HandleExceptionAsync(message).ConfigureAwait(false);
        }

        public Task<T> ProcessRequestAsync<T>(Endpoint endpoint) where T : class
        {
            return ProcessRequestAsync<T>(endpoint, null, null, CancellationToken.None);
        }

        public Task<T> ProcessRequestAsync<T>(Endpoint endpoint, object queryParameters, object requestObject) where T : class
        {
            return ProcessRequestAsync<T>(endpoint, queryParameters, requestObject, CancellationToken.None);
        }

        public async Task<T> ProcessRequestAsync<T>(Endpoint endpoint, object queryParameters, object requestObject, CancellationToken cancellationToken) where T : class
        {
            var message = await CreateRequestAsync(endpoint, queryParameters, requestObject, cancellationToken).ConfigureAwait(false);
            if (message.IsSuccessStatusCode)
            {
                if (message.StatusCode == HttpStatusCode.NoContent)
                {
                    await HandleJsonResponseAsync(message);
                }
                return await HandleJsonResponseAsync<T>(message).ConfigureAwait(false);
            }
            throw await HandleExceptionAsync(message).ConfigureAwait(false);
        }

        private async Task HandleJsonResponseAsync(HttpResponseMessage responseMessage)
        {
            var jsonStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            try
            {
                var response = await JsonSerializer.DeserializeAsync<CoinMarketCapResponse>(jsonStream, JsonOptions).ConfigureAwait(false);
                Status = response.Status;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                responseMessage.Dispose();
            }
        }

        private async Task<T> HandleJsonResponseAsync<T>(HttpResponseMessage responseMessage)
        {
            var jsonStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            T obj;
            try
            {
                var response = await JsonSerializer.DeserializeAsync<CoinMarketCapResponse<T>>(jsonStream, JsonOptions).ConfigureAwait(false);
                Status = response.Status;
                obj = response.Data;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                responseMessage.Dispose();
            }
            return obj;
        }

        private async Task<Exception> HandleExceptionAsync(HttpResponseMessage responseMessage)
        {
            try
            {
                var jsonStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var response = await JsonSerializer.DeserializeAsync<CoinMarketCapResponse>(jsonStream, JsonOptions).ConfigureAwait(false);
                Status = response.Status;
                return new CoinMarketCapException(response.Status.ErrorCode, response.Status.ErrorMessage);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                responseMessage.Dispose();
            }
        }

        private static Dictionary<string, object> ObjectToDictionary(object obj)
        {
            var properties = new Dictionary<string, object>();
            var type = obj.GetType();
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                properties.Add(prop.Name, prop.GetValue(obj));
            }

            return properties;
        }

        private static string GenerateQueryString(object queryParameters)
        {
            if (queryParameters == null)
            {
                throw new ArgumentNullException(nameof(queryParameters));
            }
            var properties = ObjectToDictionary(queryParameters);
            var queryString = string.Join("&", properties
                .Where(property => property.Value != null)
                .Select(property => property.Key + "=" + WebUtility.UrlEncode(property.Value.ToString())));
            return queryString;
        }
    }
}