using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Alura.Challenge.Domain.Abstracts
{
    public abstract class AbstractValidator<TEntity> : FluentValidation.AbstractValidator<TEntity>, Validators.Interfaces.IValidator<TEntity>
    {
        private bool _builded;

        private void ValidateValidator()
        {
            if (_builded) return;
            SetRules();
            _builded = true;
        }

        protected abstract void SetRules();

        public ValidationResult Check(TEntity entity)
        {
            ValidateValidator();
            return Validate(entity);
        }

        public ValidationResult Check(TEntity entity, string ruleSet)
        {
            ValidateValidator();
            return this.Validate(entity, options => options.IncludeRuleSets(ruleSet));
        }

        public async Task<ValidationResult> CheckAsync(TEntity entity)
        {
            ValidateValidator();
            return await ValidateAsync(entity);
        }

        public async Task<ValidationResult> CheckAsync(TEntity entity, string ruleSet)
        {
            ValidateValidator();
            return await this.ValidateAsync(entity, optioins => optioins.IncludeRuleSets(ruleSet));
        }
    }
}