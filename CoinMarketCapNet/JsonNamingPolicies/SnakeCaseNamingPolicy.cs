using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace CoinMarketCapNet.JsonNamingPolicies
{
    internal class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return ToSnakeCase(name);
        }

        private static string ToSnakeCase(string input)
        {
            var stringBuilder = new StringBuilder();
            bool inNumber = false;
            for(int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (i > 0 && (char.IsUpper(c) || (char.IsDigit(c) && !inNumber)))
                {
                    stringBuilder.Append("_");
                }
                stringBuilder.Append(char.ToLower(c));

                inNumber = char.IsDigit(c);
            }
            return stringBuilder.ToString();
        }
    }
}
