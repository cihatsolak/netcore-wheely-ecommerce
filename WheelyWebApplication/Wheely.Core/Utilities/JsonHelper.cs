using Newtonsoft.Json;

namespace Wheely.Core.Utilities
{
    /// <summary>
    /// JSON Helper
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Converts object type to string type
        /// </summary>
        /// <param name="value">type of object</param>
        /// <returns>type of string value</returns>
        public static string ToJsonString(this object value)
        {
            if (value is null)
                return default;

            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Converts string type to specified type
        /// </summary>
        /// <typeparam name="TModel">deserialize type</typeparam>
        /// <param name="value">value to deserialize</param>
        /// <returns>type of object</returns>
        public static TModel AsModel<TModel>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return default;

            return JsonConvert.DeserializeObject<TModel>(value);
        }
    }
}
