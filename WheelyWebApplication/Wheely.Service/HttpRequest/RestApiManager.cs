using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Core.Utilities;

namespace Wheely.Service.HttpRequest
{
    public sealed class RestApiManager : IRestApiService
    {
        #region Methods

        #region Gets
        public IDataResult<TModel> GetApiResponse<TModel>(string serviceUrl, NameValueCollection headersCollection = null) where TModel : class, new()
        {
            var httpResponseMessage = GetRequest(serviceUrl, headersCollection);
            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(result.AsModel<TModel>());

            return new SuccessDataResult<TModel>(result.AsModel<TModel>()); ;
        }

        public async Task<IDataResult<TModel>> GetApiResponseAsync<TModel>(string serviceUrl, NameValueCollection headersCollection = null) where TModel : class, new()
        {
            var httpResponseMessage = await GetRequestAsync(serviceUrl, headersCollection);
            var result = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(result.AsModel<TModel>());

            return new SuccessDataResult<TModel>(result.AsModel<TModel>());
        }
        #endregion

        #region Posts
        public IDataResult<TModel> PostApiResponse<TModel>(string serviceUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where TModel : class, new()
        {
            var httpResponseMessage = PostRequest(serviceUrl, parameters, headersCollection, authToken);
            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(result.AsModel<TModel>());

            return new SuccessDataResult<TModel>(result.AsModel<TModel>());
        }

        public async Task<IDataResult<TModel>> PostApiResponseAsync<TModel>(string serviceUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where TModel : class, new()
        {
            var httpResponseMessage = await PostRequestAsync(serviceUrl, parameters, headersCollection, authToken);
            var result = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(result.AsModel<TModel>());

            return new SuccessDataResult<TModel>(result.AsModel<TModel>());
        }
        #endregion

        #region EncodedPosts
        public IDataResult<TModel> PostEncodedApiResponse<TModel>(string serviceUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where TModel : class, new()
        {
            var httpResponseMessage = PostEncodedRequest(serviceUrl, keyValuePairs, authToken);
            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(result.AsModel<TModel>());

            return new SuccessDataResult<TModel>(result.AsModel<TModel>());
        }

        public async Task<IDataResult<TModel>> PostEncodedApiResponseAsync<TModel>(string serviceUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where TModel : class, new()
        {
            var httpResponseMessage = await PostEncodedRequestAsync(serviceUrl, keyValuePairs, authToken);
            var result = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(result.AsModel<TModel>());

            return new SuccessDataResult<TModel>(result.AsModel<TModel>());
        }
        #endregion

        #endregion

        #region Utitilies

        #region Gets
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

        private async Task<HttpResponseMessage> GetRequestAsync(string serviceUrl, NameValueCollection headersCollection = null)
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

            var httpResponseMessage = await client.GetAsync(serviceUrl);
            return httpResponseMessage;
        }
        #endregion

        #region Posts
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

        private async Task<HttpResponseMessage> PostRequestAsync(string serviceUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null)
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

            var httpResponseMessage = await client.PostAsync(serviceUrl, stringContent);
            return httpResponseMessage;
        }
        #endregion

        #region EncodedPosts
        private HttpResponseMessage PostEncodedRequest(string serviceUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null)
        {
            using HttpClient client = new()
            {
                Timeout = TimeSpan.FromMinutes(1),
                BaseAddress = new Uri(serviceUrl)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            if (!string.IsNullOrWhiteSpace(authToken))
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, authToken);

            using FormUrlEncodedContent content = new(keyValuePairs);
            HttpRequestMessage request = new(HttpMethod.Post, serviceUrl) { Content = content };

            var result = client.SendAsync(request).Result;
            return result;
        }

        private async Task<HttpResponseMessage> PostEncodedRequestAsync(string serviceUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null)
        {
            using HttpClient client = new()
            {
                Timeout = TimeSpan.FromMinutes(1),
                BaseAddress = new Uri(serviceUrl)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            if (!string.IsNullOrWhiteSpace(authToken))
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, authToken);

            using FormUrlEncodedContent content = new(keyValuePairs);
            HttpRequestMessage request = new(HttpMethod.Post, serviceUrl) { Content = content };

            var httpResponseMessage = await client.SendAsync(request);
            return httpResponseMessage;
        }
        #endregion

        #endregion
    }
}
