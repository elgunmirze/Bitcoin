﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bitcoin.Wallet.Api.Client
{
    public class BlockchainHttpClient : IHttpClient
    {
        private const string BaseUri = "https://blockchain.info";
        private const int TimeoutMs = 100000;
        private readonly HttpClient _httpClient;
        public string ApiCode { get; set; }

        public BlockchainHttpClient(string apiCode = null, string uri = BaseUri)
        {
            ApiCode = apiCode;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(uri),
                Timeout = TimeSpan.FromMilliseconds(BlockchainHttpClient.TimeoutMs)
            };
        }

        public async Task<T> GetAsync<T>(string route, QueryString queryString = null,
                                         Func<string, T> customDeserialization = null)
        {

            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }

            if (ApiCode != null)
            {
                queryString?.Add("api_code", ApiCode);
            }

            if (queryString != null && queryString.Count > 0)
            {
                int queryStringIndex = route.IndexOf('?');
                if (queryStringIndex >= 0)
                {
                    //Append to querystring
                    string queryStringValue = queryStringIndex.ToString();
                    //replace questionmark with &
                    queryStringValue = "&" + queryStringValue.Substring(1);
                    route += queryStringValue;
                }
                else
                {
                    route += queryString.ToString();
                }
            }

            HttpResponseMessage response = await _httpClient.GetAsync(route);
            string responseString = await ValidateResponse(response);
            T responseObject = customDeserialization == null
                ? JsonConvert.DeserializeObject<T>(responseString)
                : customDeserialization(responseString);

            return responseObject;

        }

        public async Task<TResponse> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse> customDeserialization = null, bool multiPartContent = false, string contentType = "application/x-www-form-urlencoded")
        {
            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }
            if (ApiCode != null)
            {
                route += "?api_code=" + ApiCode;
            }
            string json = JsonConvert.SerializeObject(postObject);
            HttpContent httpContent;
            if (multiPartContent)
            {
                httpContent = new MultipartFormDataContent
                {
                    new StringContent(json, Encoding.UTF8, contentType)
                };
            }
            else
            {
                httpContent = new StringContent(json, Encoding.UTF8, contentType);
            }
            HttpResponseMessage response = await _httpClient.PostAsync(route, httpContent);
            string responseString = await this.ValidateResponse(response);
            var responseObject = JsonConvert.DeserializeObject<TResponse>(responseString);
            return responseObject;
        }

        private async Task<string> ValidateResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                if (responseString != null && responseString.StartsWith("{\"error\":"))
                {
                    JObject jObject = JObject.Parse(responseString);
                    var message = jObject["error"].ToObject<string>();
                    throw new Exception(message);
                }
                return responseString;
            }
            string responseContent = await response.Content.ReadAsStringAsync();
            if (string.Equals(responseContent, "Block Not Found"))
            {
                throw new Exception("Block Not Found");
            }
            throw new Exception(response.ReasonPhrase + ": " + responseContent);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
