namespace Wheely.Core.Web.Settings
{
    public interface IGoogleReCaptchaSetting
    {
        string SiteKeyAddress { get; set; }
        string ValidateAddress { get; set; }
        string SiteKey { get; set; }
        string SecretKey { get; set; }
        decimal MinumumScore { get; set; }
    }

    public sealed class GoogleReCaptchaSetting : IGoogleReCaptchaSetting
    {
        public string SiteKeyAddress { get; set; }
        public string ValidateAddress { get; set; }
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }
        public decimal MinumumScore { get; set; }
    }
}
