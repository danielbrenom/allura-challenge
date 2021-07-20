using Allura.Challenge.Domain.Models.Data;
using FluentValidation;

namespace Allura.Challenge.Domain.Validators
{
    public class MovieValidator : Domain.Abstracts.AbstractValidator<Movie>
    {
        protected override void SetRules()
        {
            RuleFor(m => m.Title).NotEmpty()
                                 .WithMessage("Title must be filled");
            RuleFor(m => m.Description).NotEmpty()
                                       .WithMessage("Description must be filled");
            RuleFor(m => m.Url).NotEmpty()
                               .WithMessage("Url must be filled");
        }
    }
    
}