using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitcoin.Wallet.Api.Client
{
    public class QueryString
    {
        private readonly IDictionary<string, string> _queryString;

        public QueryString()
        {
            _queryString = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            if (_queryString.ContainsKey(key))
            {
                throw new Exception($"Query string already has a value for {key}");
            }
            _queryString[key] = value;
        }

        public int Count
        {
            get { return _queryString.Count; }
        }

        public override string ToString()
        {
            return "?" + string.Join("&", _queryString.Select(kv => $"{kv.Key}={kv.Value}"));
        }
    }
}
