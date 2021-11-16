using System.Collections.Generic;
using System.Collections.Specialized;

namespace Wheely.Service.HttpRequest
{
    public partial interface IRestApiService
    {
        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="T">class and new()</typeparam>
        /// <param name="serviceUrl">service url</param>
        /// <param name="parameters">parameters</param>
        /// <param name="headersCollection">header collection</param>
        /// <param name="authToken">authorization token</param>
        /// <returns></returns>
        T PostApiResponse<T>(string serviceUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where T : class, new();

        /// <summary>
        /// Http post request - Form Url Encoded Content
        /// </summary>
        /// <typeparam name="T">class and new()</typeparam>
        /// <param name="serviceUrl">service url</param>
        /// <param name="keyValuePairs">key value pairs</param>
        /// <param name="authToken">authorization token</param>
        /// <returns></returns>
        T PostApiResponse<T>(string serviceUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where T : class, new();

        /// <summary>
        /// Http get request
        /// </summary>
        /// <typeparam name="T">class and new()</typeparam>
        /// <param name="serviceUrl">service url</param>
        /// <param name="headersCollection">header collection</param>
        /// <returns></returns>
        T GetApiResponse<T>(string serviceUrl, NameValueCollection headersCollection = null) where T : class, new();
    }
}
