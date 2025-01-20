using FluentValidation;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Validators
{
    public class ContactableEntityValidator : AbstractValidator<ContactableEntity>
    {
        public ContactableEntityValidator()
        {
          
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi gereklidir.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi sağlayın.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası gereklidir.")
                .MaximumLength(15).WithMessage("Telefon numarası en fazla 15 karakter olmalıdır.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Lütfen geçerli bir telefon numarası girin.");

            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad alanı gereklidir.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olmalıdır.");
        

        }
    }
}
