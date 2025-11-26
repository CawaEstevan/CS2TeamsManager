namespace CS2Players.Domain.Entities
{
    public class Jogador
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string NomeCompleto { get; set; }
        public int Idade { get; set; }
        public string Funcao { get; set; } 
        public decimal Salario { get; set; }
        public string Nacionalidade { get; set; }
        public DateTime DataContratacao { get; set; }


        public int TimeId { get; set; }
        

        public Time Time { get; set; }

        public Jogador()
        {
            DataContratacao = DateTime.Now;
        }


        public bool ValidarNickname()
        {
            return !string.IsNullOrWhiteSpace(Nickname) && 
                   Nickname.Length >= 2 && 
                   Nickname.Length <= 20;
        }


        public bool ValidarIdade()
        {
            return Idade >= 16 && Idade <= 40;
        }


        public bool ValidarSalario()
        {
            return Salario > 0;
        }
    }
}