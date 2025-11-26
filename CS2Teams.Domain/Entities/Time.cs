using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CS2Teams.Domain.Entities
{
    public class Time
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do time é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A tag do time é obrigatória")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "A tag deve ter entre 2 e 5 caracteres")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "A tag deve conter apenas letras maiúsculas e números")]
        public string Tag { get; set; }

        [Required(ErrorMessage = "O país é obrigatório")]
        [StringLength(50)]
        public string Pais { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Fundação")]
        public DateTime DataFundacao { get; set; }

        [StringLength(500)]
        [Url(ErrorMessage = "URL inválida")]
        public string? LogoUrl { get; set; }

        [Range(1, 100, ErrorMessage = "O ranking deve estar entre 1 e 100")]
        public int Ranking { get; set; }

        public ICollection<Jogador> Jogadores { get; set; } = new List<Jogador>();
    }
}
