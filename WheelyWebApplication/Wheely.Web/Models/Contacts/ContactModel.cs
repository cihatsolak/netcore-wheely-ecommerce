using System.ComponentModel.DataAnnotations;

namespace Wheely.Web.Models.Contacts
{
    public class ContactModel
    {
        [DataType(DataType.Text)]
        [Display(Name = nameof(FullName), Prompt = "Ad ve soyad")]
        public string FullName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = nameof(Email), Prompt = "E-posta adresi")]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = nameof(Message), Prompt = "Mesaj")]
        public string Message { get; set; }
    }
}
