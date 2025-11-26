using System.ComponentModel.DataAnnotations;
using CS2Players.Application.Validators;

namespace CS2Players.Application.ViewModels
{
    public class JogadorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nickname é obrigatório")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "O nickname deve ter entre 2 e 20 caracteres")]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [StringLength(150, ErrorMessage = "O nome completo não pode exceder 150 caracteres")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória")]
        [IdadeMinima(16, 40)]
        [Display(Name = "Idade")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "A função é obrigatória")]
        [Display(Name = "Função")]
        public string Funcao { get; set; }

        [Required(ErrorMessage = "O salário é obrigatório")]
        [Range(1000, 1000000, ErrorMessage = "O salário deve estar entre R$ 1.000 e R$ 1.000.000")]
        [Display(Name = "Salário (R$)")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "A nacionalidade é obrigatória")]
        [Display(Name = "Nacionalidade")]
        public string Nacionalidade { get; set; }

        [Display(Name = "Data de Contratação")]
        public DateTime DataContratacao { get; set; }

        [Required(ErrorMessage = "Selecione um time")]
        [Display(Name = "Time")]
        public int TimeId { get; set; }

        public string TimeNome { get; set; }
    }
}