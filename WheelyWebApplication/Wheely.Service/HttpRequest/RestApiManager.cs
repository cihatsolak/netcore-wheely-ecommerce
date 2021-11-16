using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Wheely.Core.Utilities;

namespace Wheely.Service.HttpRequest
{
    public sealed class RestApiManager : IRestApiService
    {
        #region Methods
        public T GetApiResponse<T>(string serviceUrl, NameValueCollection headersCollection = null) where T : class, new()
        {
            var httpResponseMessage = GetRequest(serviceUrl, headersCollection);
            if (!httpResponseMessage.IsSuccessStatusCode)
                return null;

            var result = httpResponseMessage.Content.ReadAsStringAsync().Result.AsModel<T>();
            return result;
        }

        public T PostApiResponse<T>(string serviceUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where T : class, new()
        {
            var httpResponseMessage = PostRequest(serviceUrl, parameters, headersCollection, authToken);
            if (!httpResponseMessage.IsSuccessStatusCode)
                return null;

            var result = httpResponseMessage.Content.ReadAsStringAsync().Result.AsModel<T>();
            return result;
        }

        public T PostApiResponse<T>(string serviceUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where T : class, new()
        {
            var httpResponseMessage = PostRequest(serviceUrl, keyValuePairs, authToken);
            if (!httpResponseMessage.IsSuccessStatusCode)
                return null;

            var result = httpResponseMessage.Content.ReadAsStringAsync().Result.AsModel<T>();
            return result;
        }
        #endregion

        #region Utitilies
        private HttpResponseMessage GetRequest(string serviceUrl, NameValueCollection headersCollection = null)
        {
            using HttpClient client = new()
            {
                Timeout = TimeSpan.FromMinutes(1),
                BaseAddress = new Uri(serviceUrl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            if (headersCollection is not null)
            {
                foreach (var key in headersCollection.AllKeys)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(key, headersCollection[key]);
                }
            }

            var httpResponseMessage = client.GetAsync(serviceUrl).Result;
            return httpResponseMessage;
        }

        private HttpResponseMessage PostRequest(string serviceUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null)
        {
            using HttpClient client = new()
            {
                Timeout = TimeSpan.FromMinutes(1),
                BaseAddress = new Uri(serviceUrl)
            };

            var strParam = parameters.ToJsonString();
            StringContent stringContent = new(strParam, Encoding.UTF8, MediaTypeNames.Application.Json);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            if (!string.IsNullOrEmpty(authToken))
            {
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, authToken);
            }

            if (headersCollection is not null)
            {
                foreach (var key in headersCollection.AllKeys)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(key, headersCollection[key]);
                }
            }

            var httpResponseMessage = client.PostAsync(serviceUrl, stringContent).Result;
            return httpResponseMessage;
        }

        private HttpResponseMessage PostRequest(string serviceUrl, IList<KeyValuePair<string, string>> postData, string authToken = null)
        {
            using var client = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(1),
                BaseAddress = new Uri(serviceUrl)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            if (!string.IsNullOrWhiteSpace(authToken))
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, authToken);

            using var content = new FormUrlEncodedContent(postData);
            var request = new HttpRequestMessage(HttpMethod.Post, serviceUrl) { Content = content };

            var result = client.SendAsync(request).Result;
            return result;
        }
        #endregion
    }
}
