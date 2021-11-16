using Microsoft.AspNetCore.Http;
using System;
using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Service.Cookies
{
    public partial interface ICookieService
    {
        /// <summary>  
        /// Get the cookie  
        /// </summary>  
        /// <param name="key">Key</param>  
        /// <returns>type of TModel</returns>  
        IDataResult<TModel> Get<TModel>(string key) where TModel : class, new();

        /// <summary>  
        /// Set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">Value to store in cookie object</param>  
        /// <param name="options">Additional cookie options</param>  
        IResult Set<TModel>(string key, TModel value) where TModel : class, new();

        /// <summary>  
        /// Set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">Value to store in cookie object</param>  
        /// <param name="expireDate">Additional expire date</param>  
        IResult Set<TModel>(string key, TModel value, DateTime expireDate) where TModel : class, new();

        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param> 
        IResult Remove(string key);
    }
}
