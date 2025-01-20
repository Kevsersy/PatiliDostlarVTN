using FluentValidation;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Validators
{
    public class AboutUsValidator : AbstractValidator<AboutUs>
    {
        public AboutUsValidator()
        {
            
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters.");
        }
    }
}
