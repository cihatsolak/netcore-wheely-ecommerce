using System.Collections.Specialized;

namespace Wheely.Service.HttpRequest
{
    public partial interface IRestApiService
    {
        T PostApiResponse<T>(string serviceUri, object parameters, NameValueCollection headersCollection = null, string authToken = null) where T : class, new();
        T GetApiResponse<T>(string serviceUri, NameValueCollection headersCollection = null) where T : class, new();
    }
}
