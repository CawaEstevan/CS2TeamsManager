namespace CS2Players.Domain.Entities
{
    public class Time
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Regiao { get; set; }
        public int AnoFundacao { get; set; }
        public string LogoUrl { get; set; }
        public DateTime DataCriacao { get; set; }


        public ICollection<Jogador> Jogadores { get; set; }

        public Time()
        {
            Jogadores = new List<Jogador>();
            DataCriacao = DateTime.Now;
        }


        public bool ValidarNome()
        {
            return !string.IsNullOrWhiteSpace(Nome) && Nome.Length >= 2;
        }


        public bool ValidarAnoFundacao()
        {
            return AnoFundacao >= 2000 && AnoFundacao <= DateTime.Now.Year;
        }
    }
}