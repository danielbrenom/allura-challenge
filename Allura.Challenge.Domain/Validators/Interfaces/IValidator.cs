using System.Threading.Tasks;
using FluentValidation.Results;

namespace Alura.Challenge.Domain.Validators.Interfaces
{
    public interface IValidator<TEntity> : FluentValidation.IValidator<TEntity>
    {
        ValidationResult Check(TEntity entity);
        ValidationResult Check(TEntity entity, string ruleSet);
        Task<ValidationResult> CheckAsync(TEntity entity);
        Task<ValidationResult> CheckAsync(TEntity entity, string ruleSet);
    }
}