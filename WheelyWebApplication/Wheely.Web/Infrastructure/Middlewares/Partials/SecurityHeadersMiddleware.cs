using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Wheely.Web.Infrastructure.Middlewares.Partials
{
    internal sealed class SecurityHeadersMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Methods
        public async Task InvokeAsync(HttpContext context)
        {
            /// <summary>
            /// Buradaki 1 değeri XSS’e karşı korumanın aktif olduğu manasına gelir. 
            /// “mode=block” ise Reflected XSS atağı durumunda sanitize işlemi yapmadan direkt tarayıcınızın sayfayı render etmesini engellemek içindir.
            /// </summary>
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");

            /// <summary>
            /// Sayfaya yüklenecek dinamik kaynaklar özelinde kontrol sağlar. 
            /// Örneğin hem kendi domaininizden hem de belirli bir başka domainden script ve style kaynaklarını yüklemeye izin veriyorsanız şu örnekteki gibi kullanılabilir:
            /// Burada “self” kendi domaininizi, sonrasında gelen url ise kendi domaininize ek olarak yükleme yapılabilecek kaynak url’ini ifade etmektedir. 
            /// Sadece kendi domaininizdeki script, img veya style vb. dosyaları yüklensin istenirse o zaman sadece self eklemek yeterli olacaktır.
            /// </summary>
            //context.Response.Headers.Add("Content-Security-Policy",
            //                             "default-src 'self'; " +
            //                             "object-src 'none';" +
            //                             "font-src 'self'; " +
            //                             "style-src 'self'; " +
            //                             "child-src 'self';" +
            //                             "frame-src 'self';" +
            //                             "connect-src 'self';" +
            //                             "frame-ancestors 'none';" +
            //                             "upgrade-insecure-requests;" +
            //                             "block-all-mixed-content" +
            //                             "img-src 'self' {your_awesome_url}; " +
            //                             "script-src 'self' https://cdnjs.cloudflare.com; " +
            //                             "style-src 'self' https://fonts.googleapis.com; font-src 'self' https://fonts.gstatic.com" +
            //                             "style-src 'self' 'unsafe-hashes' 'sha256-6lbPeCWBEjE++6uHl72kLeOZAnxMuymARwIqKPYVbI8=';" +
            //                             "style-src 'self' https://www.bootstrapcdn.com");

            /// <summary>
            /// Tarayıcılar "Content-Type" başlığı ile belirtişmemiş içerikleri kendileri değerlendirip türünü bulurlar. Buna MIME Type Sniffing denir.
            /// Bu XSS zaafiyetine yol açabilir.
            /// Örneğin saldırgan zararsız görülecek bir uzantı ile mesela .png ile bir dosya yükleme işlemi yapıyor ancak dosya içeriğinde zararlı bir script olabilir.
            /// Tarayıcının içeriği kendisinin belirlemesini engellemek içinse bu başlığa “nosniff” değerinin geçilmesi gerekmektedir:
            /// </summary>
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

            /// <summary>
            /// “Clickjacking” ataklarını engellemek amacıyla kullanılır. 
            /// Clickjacking kullanıcının işlem yaptığı sayfanın aslında işlem yapmak istediği sayfa değil de bir iframe içerisine yüklenmiş bir başka sayfa olması ve kullanıcının bunu farketmeyip işlemler yapması durumu.
            /// DENY:  Sayfanın herhangi bir frame içerisinde çağırılmasını tamamiyle engellemek için kullanılır.
            /// SAMEORIGIN: KKendi domainizden başka herhangi bir domainin frame içerisinde çağırmasını engellemek için kullanılır.
            /// ALLOW - FROM Uri: Bu özellik sadece özel belirlenmiş URL’lerden (örneğin; https://example.com) sadece frame yapılmasına izin vermek için kullanılır.
            /// </summary>
            context.Response.Headers.Add("X-Frame-Options", "DENY");

            /// <summary>
            /// Ziyaret edilen web sayfasına nereden ulaşıldığı bilgisi Referrer-Policy başlık bilgisi ile requeste eklenir. 
            /// Ziyaret edilen site kendisine nereden/hangi sayfadan gelindiğini bilir. Bu bilginin ziyaret edilen web sitelerinde görünmesinin sakıncalı olduğu durumlarda Referrer-Policy eklenir.
            /// no-referrer: referer başlığı tamamen kaldırılıp gönderilmesi engellenmiş olur.
            /// no-referrer-when-downgrade: https bir kaynaktan http olan bir kaynağı ziyaret ederken referer başlığı gönderilmez. Bir policy eklenmez ise varsayılan davranış bu olur.
            /// origin: Sadece domain bilgisi gönderilir. Örneğin https://mymedium.com/documents sadece https://mymedium.com olarak gönderilecektir.
            /// origin-when-cross-origin: Aynı domainde olmayan bir web sayfası ziyaret edildiğinde sadece domain bilgisi gönderilir. Tersi durumda tüm url gönderilir.
            /// same-origin: Aynı domainde olan web sayfaları için referer bilgisi gönderilir, tersi durumda gönderilmez.
            /// strict-origin: https protokollü bir web sayfasında https protoküllü bir başka web sayfasına giderken ya da http protokollü bir web sayfasında http protokollü bir web sayfasına giderken sadece domain bilgisi gönderilir. Farklı protokoller için referer bilgisini gönderilmez.
            /// strict-origin-when-cross-origin: Farklı domainli web sayfaları için strict-origin kurallarını işletir.
            /// unsafe-url: Tüm url bilgisi gönderilir.
            /// </summary>
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");

            /// <summary>
            /// Uygulamanızın hangi özelliklere ihtiyacı olduğunu tarayıcıya bildiren başlık bilgisidir. 
            /// Örneğin, mikrofon, kamera, usb gibi özelliklere ihtiyaç duyulmadığında bunlar bu Feature-Policy başlığı ile tarayıcıya söylenebilir.
            /// </summary>
            context.Response.Headers.Add("Feature-Policy",
                                         "camera 'none'; " +
                                         "accelerometer 'none'; " +
                                         "geolocation 'none'; " +
                                         "magnetometer 'none'; " +
                                         "microphone 'none'; " +
                                         "usb 'none'");

            /// <summary>
            /// Bu başlık, sitelerin içeriklerinin Adobe Reader gibi diğer uygulamalara gömülebilmesini engellemek amacıyla eklenebilir.
            /// </summary>
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

            context.Response.Headers.Add("Clear-Site-Data",  "\"cache\", \"cookies\", \"storage\"");
            context.Response.Headers.Add("Cross-Origin-Embedder-Policy", "require-corp");
            context.Response.Headers.Add("Cross-Origin-Opener-Policy", "same-origin");
            context.Response.Headers.Add("Cross-Origin-Resource-Policy", "same-origin");

            await _next.Invoke(context);
        }
        #endregion
    }
}
