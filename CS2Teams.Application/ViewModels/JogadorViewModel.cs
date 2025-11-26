using System;
using System.ComponentModel.DataAnnotations;
using CS2Teams.Domain.Enums;
using CS2Teams.Domain.Validations;

namespace CS2Teams.Application.ViewModels
{
    public class JogadorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nickname é obrigatório")]
        [StringLength(50, MinimumLength = 2)]
        public string Nickname { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome real é obrigatório")]
        [StringLength(100)]
        [Display(Name = "Nome Real")]
        public string NomeReal { get; set; } = string.Empty;

        [Required(ErrorMessage = "A nacionalidade é obrigatória")]
        [StringLength(50)]
        public string Nacionalidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [IdadeMinima(16)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "A role é obrigatória")]
        public PlayerRole Role { get; set; }

        [Required(ErrorMessage = "O rating é obrigatório")]
        [Range(0.5, 2.0, ErrorMessage = "O rating deve estar entre 0.5 e 2.0")]
        public decimal Rating { get; set; }

        [Required(ErrorMessage = "O time é obrigatório")]
        [Display(Name = "Time")]
        public int TimeId { get; set; }

        [Display(Name = "Time")]
        public string TimeNome { get; set; } = string.Empty;

        public string TimeTag { get; set; } = string.Empty;

        [Display(Name = "Idade")]
        public int Idade
        {
            get
            {
                var idade = DateTime.Today.Year - DataNascimento.Year;
                if (DataNascimento.Date > DateTime.Today.AddYears(-idade))
                    idade--;
                return idade;
            }
        }
    }
}
