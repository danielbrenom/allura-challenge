using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Allura.Challenge.Domain.Exceptions
{
    public class InvalidDataException : System.Exception
    {
        public List<string> ValidationErrors { get; }
        public InvalidDataException(string message, IEnumerable<ValidationFailure> errors) : base(message)
        {
            ValidationErrors = errors.Select(e => e.ErrorMessage).ToList();
        }
        
        public InvalidDataException(string message, System.Exception inner, List<string> errors) : base(message, inner)
        {
        }
    }
}