using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Wheely.Service.HttpRequest
{
    public sealed class RestApiManager : IRestApiService
    {
        #region Methods
        public T GetApiResponse<T>(string serviceUri, NameValueCollection headersCollection = null) where T : class, new()
        {
            var httpResponseMessage = GetRequest(serviceUri, headersCollection);
            if (!httpResponseMessage.IsSuccessStatusCode)
                return null;

            var result = JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            return result;
        }

        public T PostApiResponse<T>(string serviceUri, object parameters, NameValueCollection headersCollection = null, string authToken = null) where T : class, new()
        {
            var httpResponseMessage = PostRequest(serviceUri, parameters, headersCollection, authToken);
            if (!httpResponseMessage.IsSuccessStatusCode)
                return null;

            var result = JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            return result;
        }
        #endregion

        #region Utitilies
        private HttpResponseMessage PostRequest(string serviceUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null)
        {
            using HttpClient client = new()
            {
                Timeout = TimeSpan.FromMilliseconds(60000)
            };
            var strParam = JsonConvert.SerializeObject(parameters);
            var stringContent = new StringContent(strParam, Encoding.UTF8, MediaTypeNames.Application.Json);

            client.BaseAddress = new Uri(serviceUrl);
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

        private HttpResponseMessage GetRequest(string serviceUrl, NameValueCollection headersCollection = null)
        {
            using HttpClient client = new()
            {
                Timeout = TimeSpan.FromMilliseconds(60000),
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
        #endregion
    }
}
