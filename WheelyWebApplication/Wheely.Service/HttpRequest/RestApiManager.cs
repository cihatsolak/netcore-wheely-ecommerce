using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
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
        #region Fields
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region Constructor
        public RestApiManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        #endregion


        #region Methods
        public IDataResult<TModel> GetApiResponse<TModel>(string clientName, string routeUrl, NameValueCollection headersCollection = null) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException(nameof(clientName));

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            if (httpClient is null)
                throw new ArgumentNullException(nameof(httpClient));

            if (headersCollection is not null)
            {
                foreach (var key in headersCollection.AllKeys)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, headersCollection[key]);
                }
            }

            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(routeUrl).Result;
            string response = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(response.AsModel<TModel>());

            return new SuccessDataResult<TModel>(response.AsModel<TModel>());
        }

        public async Task<IDataResult<TModel>> GetApiResponseAsync<TModel>(string clientName, string routeUrl, NameValueCollection headersCollection = null) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException(nameof(clientName));

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            if (httpClient is null)
                throw new ArgumentNullException(nameof(httpClient));

            if (headersCollection is not null)
            {
                foreach (var key in headersCollection.AllKeys)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, headersCollection[key]);
                }
            }

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(routeUrl);
            string response = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(response.AsModel<TModel>());

            return new SuccessDataResult<TModel>(response.AsModel<TModel>());
        }

        public IDataResult<TModel> PostApiResponse<TModel>(string clientName, string routeUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException(nameof(clientName));

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            if (httpClient is null)
                throw new ArgumentNullException(nameof(httpClient));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, authToken);
            }

            if (headersCollection is not null)
            {
                foreach (var key in headersCollection.AllKeys)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, headersCollection[key]);
                }
            }

            string content = parameters.ToJsonString();
            StringContent stringContent = new(content, Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(routeUrl, stringContent).Result;
            string response = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(response.AsModel<TModel>());

            return new SuccessDataResult<TModel>(response.AsModel<TModel>());
        }

        public async Task<IDataResult<TModel>> PostApiResponseAsync<TModel>(string clientName, string routeUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException(nameof(clientName));

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            if (httpClient is null)
                throw new ArgumentNullException(nameof(httpClient));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, authToken);
            }

            if (headersCollection is not null)
            {
                foreach (var key in headersCollection.AllKeys)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, headersCollection[key]);
                }
            }

            string content = parameters.ToJsonString();
            StringContent stringContent = new(content, Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(routeUrl, stringContent);
            string response = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(response.AsModel<TModel>());

            return new SuccessDataResult<TModel>(response.AsModel<TModel>());
        }

        public IDataResult<TModel> PostEncodedApiResponse<TModel>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException(nameof(clientName));

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            if (httpClient is null)
                throw new ArgumentNullException(nameof(httpClient));

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, authToken);
            }

            using FormUrlEncodedContent content = new(keyValuePairs);
            HttpRequestMessage request = new(HttpMethod.Post, routeUrl) { Content = content };

            HttpResponseMessage httpResponseMessage = httpClient.SendAsync(request).Result;
            string response = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(response.AsModel<TModel>());

            return new SuccessDataResult<TModel>(response.AsModel<TModel>());
        }

        public async Task<IDataResult<TModel>> PostEncodedApiResponseAsync<TModel>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException(nameof(clientName));

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            if (httpClient is null)
                throw new ArgumentNullException(nameof(httpClient));

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, authToken);
            }

            using FormUrlEncodedContent content = new(keyValuePairs);
            HttpRequestMessage request = new(HttpMethod.Post, routeUrl) { Content = content };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request);
            string response = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
                return new ErrorDataResult<TModel>(response.AsModel<TModel>());

            return new SuccessDataResult<TModel>(response.AsModel<TModel>());
        }

        #endregion
    }
}
