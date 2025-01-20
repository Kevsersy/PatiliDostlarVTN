using FluentValidation;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
             RuleFor(x => x.Company)
                .NotEmpty().WithMessage("Pati türü alanı boş geçilemez.")
                .MaximumLength(100).WithMessage("Pati türü 100 karakterden fazla olamaz.");
        }
    }
}
