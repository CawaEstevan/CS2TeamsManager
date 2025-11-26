using Mapster;
using CS2Teams.Domain.Entities;
using CS2Teams.Application.ViewModels;

namespace CS2Teams.Application.Mappings
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            // Time -> TimeViewModel
            TypeAdapterConfig<Time, TimeViewModel>
                .NewConfig()
                .Map(dest => dest.Jogadores, src => src.Jogadores);

            // TimeViewModel -> Time
            TypeAdapterConfig<TimeViewModel, Time>
                .NewConfig()
                .Map(dest => dest.Jogadores, src => src.Jogadores);

            // Jogador -> JogadorViewModel
            TypeAdapterConfig<Jogador, JogadorViewModel>
                .NewConfig()
                .Map(dest => dest.TimeNome, src => src.Time != null ? src.Time.Nome : null)
                .Map(dest => dest.TimeTag, src => src.Time != null ? src.Time.Tag : null);

            // JogadorViewModel -> Jogador
            TypeAdapterConfig<JogadorViewModel, Jogador>
                .NewConfig()
                .Ignore(dest => dest.Time);
        }
    }
}