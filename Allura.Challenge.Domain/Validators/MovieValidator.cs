using Allura.Challenge.Domain.Models.Data;
using FluentValidation;

namespace Allura.Challenge.Domain.Validators
{
    public class MovieValidator : Domain.Abstracts.AbstractValidator<Movie>
    {
        protected override void SetRules()
        {
            RuleSet("Create", () =>
            {
                RuleFor(m => m.Title).NotEmpty()
                                     .WithMessage("Title must be filled");
                RuleFor(m => m.Description).NotEmpty()
                                           .WithMessage("Description must be filled");
                RuleFor(m => m.Url).NotEmpty()
                                   .WithMessage("Url must be filled");
                RuleFor(m => m.Category.Id).Length(1,100).When(m => !string.IsNullOrEmpty(m.Category.Id))
                                     .WithMessage("Category must be filled");
            });
            RuleSet("Update", () =>
            {
                RuleFor(m => m.Title).Length(1,100).When(m => !string.IsNullOrEmpty(m.Title))
                                     .WithMessage("Title must be filled");
                RuleFor(m => m.Description).Length(1,200).When(m => !string.IsNullOrEmpty(m.Description))
                                           .WithMessage("Description must be filled");
                RuleFor(m => m.Url).Length(1,200).When(m => !string.IsNullOrEmpty(m.Url))
                                   .WithMessage("Url must be filled");
                RuleFor(m => m.Category.Id).Length(1,100).When(m => !string.IsNullOrEmpty(m.Category.Id))
                                           .WithMessage("Category must be filled");
            });
        }
    }
}