using System.ComponentModel.DataAnnotations;
using CS2Players.Application.Validators;

namespace CS2Players.Application.ViewModels
{
    public class TimeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do time é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
        [Display(Name = "Nome do Time")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A região é obrigatória")]
        [Display(Name = "Região")]
        public string Regiao { get; set; }

        [Required(ErrorMessage = "O ano de fundação é obrigatório")]
        [AnoFundacaoValido]
        [Display(Name = "Ano de Fundação")]
        public int AnoFundacao { get; set; }

        [Url(ErrorMessage = "URL inválida")]
        [Display(Name = "URL do Logo")]
        public string LogoUrl { get; set; }

        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }

        public ICollection<JogadorViewModel> Jogadores { get; set; }

        public TimeViewModel()
        {
            Jogadores = new List<JogadorViewModel>();
        }
    }
}