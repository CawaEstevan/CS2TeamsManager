using System;
using System.Collections.Generic;
using CS2Teams.Domain.Entities;

namespace CS2Teams.Domain.Factories
{
    public class TimeFactory
    {
        public static Time Create(string nome, string tag, string pais, DateTime dataFundacao, int ranking, string? logoUrl = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do time não pode ser vazio", nameof(nome));

            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentException("Tag do time não pode ser vazia", nameof(tag));

            if (ranking < 1 || ranking > 100)
                throw new ArgumentException("Ranking deve estar entre 1 e 100", nameof(ranking));

            return new Time
            {
                Nome = nome,
                Tag = tag.ToUpper(),
                Pais = pais,
                DataFundacao = dataFundacao,
                Ranking = ranking,
                LogoUrl = logoUrl,
                Jogadores = new List<Jogador>()
            };
        }
    }
}
