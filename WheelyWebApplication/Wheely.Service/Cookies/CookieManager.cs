namespace Wheely.Service.Cookies
{
    public sealed class CookieManager : ICookieService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtector _dataProtector;
        private readonly ILogger<CookieManager> _logger;
        #endregion

        #region Constructor
        public CookieManager(
            IHttpContextAccessor httpContextAccessor,
            IDataProtectionProvider dataProtectionProvider,
            ILogger<CookieManager> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataProtector = dataProtectionProvider.CreateProtector(GetType().FullName);
            _logger = logger;
        }
        #endregion

        #region Methods
        public IDataResult<TModel> Get<TModel>(string key) where TModel : class, new()
        {
            string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[key];
            if (string.IsNullOrWhiteSpace(cookieValue))
                return new ErrorDataResult<TModel>();

            var convertedCookieModel = Decrypt(cookieValue).AsModel<TModel>();
            if (convertedCookieModel is null)
            {
                _logger.LogError("", cookieValue);
                throw new ArgumentNullException(nameof(convertedCookieModel));
            }

            return new SuccessDataResult<TModel>(convertedCookieModel);
        }

        public IResult Set<TModel>(string key, TModel value) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(key) || value is null)
            {
                _logger.LogError("@Value", value);
                return new ErrorResult();
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, Encrypt(value.ToJsonString()));
            return new SuccessResult();
        }

        public IResult Set<TModel>(string key, TModel value, DateTime expireDate) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(key) || value is null || expireDate == DateTime.MinValue)
            {
                _logger.LogError("@Value", value);
                return new ErrorResult();
            }

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

        public IResult Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                _logger.LogError("@key", key);
                return new ErrorResult();
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            return new SuccessResult();
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
