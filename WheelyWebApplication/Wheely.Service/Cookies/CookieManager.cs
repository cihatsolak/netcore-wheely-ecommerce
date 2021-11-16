using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Core.Utilities;

namespace Wheely.Service.Cookies
{
    public sealed class CookieManager : ICookieService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtector _dataProtector;
        private readonly ILogger<CookieManager> _basketManager;
        #endregion

        #region Constructor
        public CookieManager(
            IHttpContextAccessor httpContextAccessor,
            IDataProtectionProvider dataProtectionProvider,
            ILogger<CookieManager> basketManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataProtector = dataProtectionProvider.CreateProtector(GetType().FullName);
            _basketManager = basketManager;
        }
        #endregion

        #region Methods
        public IDataResult<TModel> Get<TModel>(string key) where TModel : class, new()
        {
            try
            {
                string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[key];
                if (string.IsNullOrWhiteSpace(cookieValue))
                {
                    return new ErrorDataResult<TModel>();
                }

                var convertedCookieModel = Decrypt(cookieValue).AsModel<TModel>();
                if (convertedCookieModel is null)
                {
                    throw new ArgumentNullException(nameof(convertedCookieModel), "");
                }

                return new SuccessDataResult<TModel>(convertedCookieModel);
            }
            catch (Exception exception)
            {
                _basketManager.LogError("asdasd", exception);
                return new ErrorDataResult<TModel>();
            }
        }

        public IResult Set<TModel>(string key, TModel value) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(key) || value is null) return new ErrorResult();

            try
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append(key, Encrypt(value.ToJsonString()));
                return new SuccessResult();
            }
            catch (Exception exception)
            {
                _basketManager.LogError("asdasd", exception);
                return new ErrorResult();
            }
        }

        public IResult Set<TModel>(string key, TModel value, DateTime expireDate) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(key) || value is null || expireDate == DateTime.MinValue) return new ErrorResult();

            try
            {
                CookieOptions cookieOptions = new()
                {
                    HttpOnly = false,
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                    Expires = expireDate
                };

                _httpContextAccessor.HttpContext.Response.Cookies.Append(key, Encrypt(value.ToJsonString()), cookieOptions);
                return new SuccessResult();
            }
            catch (Exception exception)
            {
                _basketManager.LogError("asdasd", exception);
                return new ErrorDataResult<TModel>();
            }
        }

        public IResult Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return new ErrorResult();

            try
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
                return new SuccessResult();
            }
            catch (Exception exception)
            {
                _basketManager.LogError("asdasd", exception);
                return new ErrorResult();
            }
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Encrypting the cookie value (value should be a valid Base-64 string)
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>type of string</returns>
        private string Encrypt(string value)
        {
            return _dataProtector.Protect(value);
        }

        /// <summary>
        /// Decrypting the cookie value (value should be a valid Base-64 string)
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>type of string</returns>
        private string Decrypt(string value)
        {
            return _dataProtector.Unprotect(value);
        }
        #endregion
    }
}
