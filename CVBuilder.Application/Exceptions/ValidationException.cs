using FluentValidation.Results;

namespace CVBuilder.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValidationErrors { get; set; }

        public ValidationException(ValidationResult result)
        {
            ValidationErrors = new List<string>();

            foreach (var error in result.Errors)
            {
                ValidationErrors.Add(error.ErrorMessage);
            }

        }
    }

}
