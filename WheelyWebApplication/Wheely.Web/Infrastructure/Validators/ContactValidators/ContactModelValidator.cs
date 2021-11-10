using FluentValidation;
using Wheely.Web.Models.Contacts;

namespace Wheely.Web.Infrastructure.Validators.ContactValidators
{
    public class ContactModelValidator : AbstractValidator<ContactModel>
    {
        public ContactModelValidator()
        {
            RuleFor(p => p.FullName)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .WithMessage("Lütfen geçerli bir isim giriniz.");

            RuleFor(p => p.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Lütfen geçerli email adresi giriniz.");

            RuleFor(p => p.Message)
                .NotEmpty()
                .NotNull()
                .Length(10, 100)
                .WithMessage("Mesaj alanı minimum 10 maksimum 100 karakter olmalıdır.");
        }
    }
}
