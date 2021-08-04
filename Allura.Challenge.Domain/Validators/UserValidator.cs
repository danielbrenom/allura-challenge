using Alura.Challenge.Domain.Models.Data;
using FluentValidation;

namespace Alura.Challenge.Domain.Validators
{
    public class UserValidator : Alura.Challenge.Domain.Abstracts.AbstractValidator<User>
    {
        protected override void SetRules()
        {
            RuleFor(u => u.Name).NotEmpty()
                                .WithMessage("Name must be filled");
            RuleFor(u => u.Email).NotEmpty()
                                 .WithMessage("Email must be filled")
                                 .EmailAddress()
                                 .WithMessage("Email must be valid");
            RuleFor(u => u.Password).NotEmpty()
                                    .WithMessage("Password must be filled")
                                    .Length(5, 18)
                                    .WithMessage("Password must have between {MinLenght} and {MaxLenght} characters");
        }
    }
}