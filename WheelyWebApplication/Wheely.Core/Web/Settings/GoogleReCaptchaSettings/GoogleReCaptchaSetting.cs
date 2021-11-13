namespace Wheely.Core.Web.Settings.GoogleReCaptchaSettings
{
    public class GoogleReCaptchaSetting : IGoogleReCaptchaSetting
    {
        public string SiteKeyAddress { get; set; }
        public string ValidateAddress { get; set; }
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }
        public decimal MinumumScore { get; set; }
    }
}
