using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CoinMarketCapNet.Endpoints
{
    internal class Endpoint
    {
        public Uri Uri { get; set; }

        public HttpMethod Method { get; set; }

        public EndpointSecurityType SecurityType { get; set; }

        public Endpoint(Uri uri)
        {
            Uri = uri;
            Method = HttpMethod.Get;
            SecurityType = EndpointSecurityType.None;
        }

        public Endpoint(Uri uri, HttpMethod method, EndpointSecurityType securityType = EndpointSecurityType.None)
        {
            Uri = uri;
            Method = method;
            SecurityType = securityType;
        }

        public override string ToString()
        {
            return Uri.ToString();
        }
    }
}
