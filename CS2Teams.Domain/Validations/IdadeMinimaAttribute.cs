using System;
using System.ComponentModel.DataAnnotations;

namespace CS2Teams.Domain.Validations
{
    public class IdadeMinimaAttribute : ValidationAttribute
    {
        private readonly int _idadeMinima;

        public IdadeMinimaAttribute(int idadeMinima)
        {
            _idadeMinima = idadeMinima;
            ErrorMessage = $"O jogador deve ter no mínimo {_idadeMinima} anos";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dataNascimento)
            {
                var idade = DateTime.Today.Year - dataNascimento.Year;
                
                if (dataNascimento.Date > DateTime.Today.AddYears(-idade))
                {
                    idade--;
                }

                if (idade < _idadeMinima)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}