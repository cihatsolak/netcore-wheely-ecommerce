using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using Wheely.Core.DependencyResolvers;
using Wheely.Core.Utilities;
using Wheely.Core.Web.Settings;
using Wheely.Web.Models.Google;

namespace Wheely.Web.Filters
{
    public class ValidateGoogleReCaptchaFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string errorMessageId = "google-validation-message";
            string errorMessage = "Google captcha doğrulama işlemi başarısız.";

            var requestModel = context.ActionArguments.Values.FirstOrDefault();
            string googleToken = requestModel?.GetType().GetProperty("GoogleToken").GetValue(requestModel).ToString();
            if (string.IsNullOrWhiteSpace(googleToken))
            {
                context.ModelState.AddModelError(errorMessageId, errorMessage);
                return;
            }

            IGoogleReCaptchaSetting googleReCaptchaSetting = ServiceTool.ServiceProvider.GetRequiredService<IGoogleReCaptchaSetting>();
            if (googleReCaptchaSetting is null)
            {
                context.ModelState.AddModelError(errorMessageId, errorMessage);
                return;
            }

            GoogleCaptchaResponseModel googleCaptchaResponseModel = null;

            try
            {
                var requestUri = string.Format(googleReCaptchaSetting.ValidateAddress, googleReCaptchaSetting.SecretKey, googleToken);
                using HttpClient httpClient = new()
                {
                    Timeout = TimeSpan.FromSeconds(30)
                };

                string response = httpClient.GetStringAsync(requestUri).Result;
                googleCaptchaResponseModel = response.AsModel<GoogleCaptchaResponseModel>();
            }
            finally
            {
                if (googleCaptchaResponseModel is null || (!googleCaptchaResponseModel.Success || googleCaptchaResponseModel.Score < googleReCaptchaSetting.MinumumScore))
                {
                    context.ModelState.AddModelError(errorMessageId, errorMessage);
                }
            }
        }
    }
}
