using System.ComponentModel.DataAnnotations;

namespace CS2Players.Application.Validators
{
    public class IdadeMinimaAttribute : ValidationAttribute
    {
        private readonly int _idadeMinima;
        private readonly int _idadeMaxima;

        public IdadeMinimaAttribute(int idadeMinima = 16, int idadeMaxima = 40)
        {
            _idadeMinima = idadeMinima;
            _idadeMaxima = idadeMaxima;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int idade)
            {
                if (idade < _idadeMinima)
                {
                    return new ValidationResult($"O jogador deve ter no mínimo {_idadeMinima} anos para competir profissionalmente.");
                }
                
                if (idade > _idadeMaxima)
                {
                    return new ValidationResult($"Idade máxima permitida é {_idadeMaxima} anos.");
                }
                
                return ValidationResult.Success;
            }
            
            return new ValidationResult("Idade inválida.");
        }
    }
}