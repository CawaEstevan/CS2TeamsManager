using System;
using System.ComponentModel.DataAnnotations;
using CS2Teams.Domain.Enums;

namespace CS2Teams.Domain.Entities
{
    public class Jogador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nickname é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nickname deve ter entre 2 e 50 caracteres")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "O nome real é obrigatório")]
        [StringLength(100)]
        [Display(Name = "Nome Real")]
        public string NomeReal { get; set; }

        [Required(ErrorMessage = "A nacionalidade é obrigatória")]
        [StringLength(50)]
        public string Nacionalidade { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "A role é obrigatória")]
        public PlayerRole Role { get; set; }

        [Range(0.5, 2.0, ErrorMessage = "O rating deve estar entre 0.5 e 2.0")]
        public double Rating { get; set; }

        [Required(ErrorMessage = "O time é obrigatório")]
        [Display(Name = "Time")]
        public int TimeId { get; set; }

        public Time? Time { get; set; }
    }
}
