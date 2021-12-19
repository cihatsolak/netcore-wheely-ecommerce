namespace Wheely.Service.Cookies
{
    public sealed class CookieManager : ICookieService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProtectorService _protectorService;
        private readonly ILogger<CookieManager> _logger;
        #endregion

        #region Constructor
        public CookieManager(
            IHttpContextAccessor httpContextAccessor,
            IProtectorService protectorService,
            ILogger<CookieManager> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _protectorService = protectorService;
            _logger = logger;
        }
        #endregion

        #region Methods
        public IDataResult<TModel> Get<TModel>(string key) where TModel : class, new()
        {
            string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[key];
            if (string.IsNullOrWhiteSpace(cookieValue))
                return new ErrorDataResult<TModel>();

            var decryptedCookieData = _protectorService.Decrypt(cookieValue);
            if (!decryptedCookieData.Succeeded)
            {
                _logger.LogError("", decryptedCookieData);
                return new ErrorDataResult<TModel>();
            }

            return new SuccessDataResult<TModel>(decryptedCookieData.Data.AsModel<TModel>());
        }

        public IResult Set<TModel>(string key, TModel value) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            ArgumentNullException.ThrowIfNull(value);

            var encryptDataResponse = _protectorService.Encrypt(value.ToJsonString());
            if (!encryptDataResponse.Succeeded)
            {
                _logger.LogError("@Value", value);
                return new ErrorResult();
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, encryptDataResponse.Data);
            return new SuccessResult();
        }

        public IResult Set<TModel>(string key, TModel value, DateTime expireDate) where TModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNull(expireDate);

            var encryptDataResponse = _protectorService.Encrypt(value.ToJsonString());
            if (!encryptDataResponse.Succeeded)
            {
                _logger.LogError("@Value", value);
                return new ErrorResult();
            }

            CookieOptions cookieOptions = new()
            {
                HttpOnly = false,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                Secure = true,
                Expires = expireDate
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, encryptDataResponse.Data, cookieOptions);
            return new SuccessResult();
        }

        public IResult Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            return new SuccessResult();
        }
        #endregion
    }
}
