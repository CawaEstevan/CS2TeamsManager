using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CS2Teams.Domain.Validations;

namespace CS2Teams.Application.ViewModels
{
    public class TimeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do time é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A tag do time é obrigatória")]
        [TagTime]
        public string Tag { get; set; }

        [Required(ErrorMessage = "O país é obrigatório")]
        [StringLength(50)]
        [Display(Name = "País")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "A data de fundação é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Fundação")]
        public DateTime DataFundacao { get; set; }

        [StringLength(500)]
        [Url(ErrorMessage = "URL inválida")]
        [Display(Name = "URL do Logo")]
        public string? LogoUrl { get; set; }

        [Required(ErrorMessage = "O ranking é obrigatório")]
        [Range(1, 100, ErrorMessage = "O ranking deve estar entre 1 e 100")]
        public int Ranking { get; set; }

        public List<JogadorViewModel> Jogadores { get; set; } = new List<JogadorViewModel>();
    }
}