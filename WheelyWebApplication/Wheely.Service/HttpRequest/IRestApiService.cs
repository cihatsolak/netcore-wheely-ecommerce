using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Service.HttpRequest
{
    public partial interface IRestApiService
    {
        /// <summary>
        /// Http get request
        /// </summary>
        /// <typeparam name="T">class and new()</typeparam>
        /// <param name="serviceUrl">service url</param>
        /// <param name="headersCollection">header collection</param>
        /// <returns></returns>
        IDataResult<TModel> GetApiResponse<TModel>(string serviceUrl, NameValueCollection headersCollection = null) where TModel : class, new();

        /// <summary>
        /// Http get request
        /// </summary>
        /// <typeparam name="T">class and new()</typeparam>
        /// <param name="serviceUrl">service url</param>
        /// <param name="headersCollection">header collection</param>
        /// <returns></returns>
        Task<IDataResult<TModel>> GetApiResponseAsync<TModel>(string serviceUrl, NameValueCollection headersCollection = null) where TModel : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TModel">class and new()</typeparam>
        /// <param name="serviceUrl">service url</param>
        /// <param name="parameters">parameters</param>
        /// <param name="headersCollection">header collection</param>
        /// <param name="authToken">authorization token</param>
        /// <returns></returns>
        IDataResult<TModel> PostApiResponse<TModel>(string serviceUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where TModel : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TModel">class and new()</typeparam>
        /// <param name="serviceUrl">service url</param>
        /// <param name="parameters">parameters</param>
        /// <param name="headersCollection">header collection</param>
        /// <param name="authToken">authorization token</param>
        /// <returns></returns>
        Task<IDataResult<TModel>> PostApiResponseAsync<TModel>(string serviceUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where TModel : class, new();

        /// <summary>
        /// Http post request - Form Url Encoded Content
        /// </summary>
        /// <typeparam name="T">class and new()</typeparam>
        /// <param name="serviceUrl">service url</param>
        /// <param name="keyValuePairs">key value pairs</param>
        /// <param name="authToken">authorization token</param>
        /// <returns></returns>
        IDataResult<TModel> PostEncodedApiResponse<TModel>(string serviceUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where TModel : class, new();

        /// <summary>
        /// Http post request - Form Url Encoded Content
        /// </summary>
        /// <typeparam name="T">class and new()</typeparam>
        /// <param name="serviceUrl">service url</param>
        /// <param name="keyValuePairs">key value pairs</param>
        /// <param name="authToken">authorization token</param>
        /// <returns></returns>
        Task<IDataResult<TModel>> PostEncodedApiResponseAsync<TModel>(string serviceUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where TModel : class, new();
    }
}
