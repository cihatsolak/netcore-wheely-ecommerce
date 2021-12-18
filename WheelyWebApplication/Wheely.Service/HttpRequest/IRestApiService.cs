namespace Wheely.Service.HttpRequest
{
    public partial interface IRestApiService
    {
        /// <summary>
        /// Http get request
        /// </summary>
        /// <typeparam name="TModel">class and new()</typeparam>
        /// <param name="clientName">client name</param>
        /// <param name="routeUrl">route url</param>
        /// <param name="headersCollection">header collection</param>
        /// <returns>type of data result interface</returns>
        IDataResult<TModel> GetApiResponse<TModel>(string clientName, string routeUrl, NameValueCollection headersCollection = null) where TModel : class, new();

        /// <summary>
        /// Http get request
        /// </summary>
        /// <typeparam name="TModel">class and new()</typeparam>
        /// <param name="clientName">client name</param>
        /// <param name="routeUrl">route url</param>
        /// <param name="headersCollection">header collection</param>
        /// <returns>type of data result interface</returns>
        Task<IDataResult<TModel>> GetApiResponseAsync<TModel>(string clientName, string routeUrl, NameValueCollection headersCollection = null) where TModel : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TModel">class and new()</typeparam>
        /// <param name="clientName">client name</param>
        /// <param name="routeUrl">route url</param>
        /// <param name="parameters">parameters</param>
        /// <param name="headersCollection">header collection</param>
        /// <param name="authToken">authorization token</param>
        /// <returns>type of data result interface</returns>
        IDataResult<TModel> PostApiResponse<TModel>(string clientName, string routeUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where TModel : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TModel">class and new()</typeparam>
        /// <param name="clientName">client name</param>
        /// <param name="routeUrl">route url</param>
        /// <param name="parameters">parameters</param>
        /// <param name="headersCollection">header collection</param>
        /// <param name="authToken">authorization token</param>
        /// <returns>type of data result interface</returns>
        Task<IDataResult<TModel>> PostApiResponseAsync<TModel>(string clientName, string routeUrl, object parameters, NameValueCollection headersCollection = null, string authToken = null) where TModel : class, new();

        /// <summary>
        /// Http post request - Form Url Encoded Content
        /// </summary>
        /// <typeparam name="TModel">class and new()</typeparam>
        /// <param name="clientName">client name</param>
        /// <param name="routeUrl">route url</param>
        /// <param name="keyValuePairs">key value pairs</param>
        /// <param name="authToken">authorization token</param>
        /// <returns>type of data result interface</returns>
        IDataResult<TModel> PostEncodedApiResponse<TModel>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where TModel : class, new();

        /// <summary>
        /// Http post request - Form Url Encoded Content
        /// </summary>
        /// <typeparam name="TModel">class and new()</typeparam>
        /// <param name="routeUrl">route url</param>
        /// <param name="keyValuePairs">key value pairs</param>
        /// <param name="authToken">authorization token</param>
        /// <returns>type of data result interface</returns>
        Task<IDataResult<TModel>> PostEncodedApiResponseAsync<TModel>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs, string authToken = null) where TModel : class, new();
    }
}
