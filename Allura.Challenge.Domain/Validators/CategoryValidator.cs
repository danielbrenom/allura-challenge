using Allura.Challenge.Domain.Models.Data;
using FluentValidation;

namespace Allura.Challenge.Domain.Validators
{
    public class CategoryValidator : Domain.Abstracts.AbstractValidator<Category>
    {
        protected override void SetRules()
        {
            RuleSet("Create", () =>
            {
                RuleFor(c => c.Title).NotEmpty()
                                     .WithMessage("Title must be filled");
                RuleFor(c => c.Color).NotEmpty()
                                     .WithMessage("Color must be filled");
            });
            RuleSet("Update", () =>
            {
                RuleFor(c => c.Title).Length(1,100).When(c => !string.IsNullOrEmpty(c.Title))
                                     .WithMessage("Title must be filled");
                RuleFor(c => c.Color).Length(1,200).When(c => !string.IsNullOrEmpty(c.Color))
                                           .WithMessage("Description must be filled");
            });
        }
    }
}