using FluentValidation;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Validators
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            
            RuleFor(x => x.AvatarUrl)
                .MaximumLength(200).WithMessage("Avatar URL cannot exceed 200 characters.");

            RuleFor(x => x.TimeAgo)
                .NotEmpty().WithMessage("TimeAgo is required.")
                .MaximumLength(50).WithMessage("TimeAgo cannot exceed 50 characters.");

           
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MaximumLength(500).WithMessage("Message cannot exceed 500 characters.");
        }
    }
}
