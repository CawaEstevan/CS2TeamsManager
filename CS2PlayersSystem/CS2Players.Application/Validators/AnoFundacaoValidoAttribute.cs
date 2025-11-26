using System.ComponentModel.DataAnnotations;

namespace CS2Players.Application.Validators
{
    public class AnoFundacaoValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int ano)
            {
                int anoAtual = DateTime.Now.Year;
                
                if (ano < 2000)
                {
                    return new ValidationResult("O ano de fundação não pode ser anterior a 2000 (início do CS competitivo).");
                }
                
                if (ano > anoAtual)
                {
                    return new ValidationResult($"O ano de fundação não pode ser maior que {anoAtual}.");
                }
                
                return ValidationResult.Success;
            }
            
            return new ValidationResult("Ano de fundação inválido.");
        }
    }
}