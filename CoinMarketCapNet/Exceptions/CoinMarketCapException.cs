using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCapNet.Exceptions
{
    public class CoinMarketCapException : Exception
    {
        public int ErrorCode { get; private set; }

        public CoinMarketCapException() : base()
        {
        }

        public CoinMarketCapException(int errorCode, string errorMessage) : base(errorMessage)
        {
            ErrorCode = errorCode;
        }
    }
}
