using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CS2Teams.Domain.Validations
{
    public class TagTimeAttribute : ValidationAttribute
    {
        public TagTimeAttribute()
        {
            ErrorMessage = "A tag deve ter entre 2 e 5 caracteres maiúsculos";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string tag = value.ToString();

            if (tag.Length < 2 || tag.Length > 5)
                return new ValidationResult("A tag deve ter entre 2 e 5 caracteres");

            if (!Regex.IsMatch(tag, @"^[A-Z0-9]+$"))
                return new ValidationResult("A tag deve conter apenas letras maiúsculas e números");

            return ValidationResult.Success;
        }
    }
}
