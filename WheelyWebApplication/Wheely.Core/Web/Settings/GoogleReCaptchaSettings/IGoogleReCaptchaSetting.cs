namespace Wheely.Core.Web.Settings.GoogleReCaptchaSettings
{
    public interface IGoogleReCaptchaSetting
    {
        string SiteKeyAddress { get; set; }
        string ValidateAddress { get; set; }
        string SiteKey { get; set; }
        string SecretKey { get; set; }
        decimal MinumumScore { get; set; }
    }
}
