using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using CS2Teams.Application.Interfaces;
using CS2Teams.Application.ViewModels;
using CS2Teams.Domain.Entities;
using CS2Teams.Domain.Interfaces;

namespace CS2Teams.Application.Services
{
    public class JogadorService : IJogadorService
    {
        private readonly IJogadorRepository _jogadorRepository;
        private readonly ITimeRepository _timeRepository;

        public JogadorService(IJogadorRepository jogadorRepository, ITimeRepository timeRepository)
        {
            _jogadorRepository = jogadorRepository;
            _timeRepository = timeRepository;
        }

        public async Task<IEnumerable<JogadorViewModel>> GetAllAsync()
        {
            var jogadores = await _jogadorRepository.GetAllAsync();
            return jogadores.Adapt<IEnumerable<JogadorViewModel>>();
        }

        public async Task<JogadorViewModel?> GetByIdAsync(int id)
        {
            var jogador = await _jogadorRepository.GetByIdWithTimeAsync(id);
            return jogador?.Adapt<JogadorViewModel>();
        }

        public async Task<IEnumerable<JogadorViewModel>> GetByTimeIdAsync(int timeId)
        {
            var jogadores = await _jogadorRepository.GetByTimeIdAsync(timeId);
            return jogadores.Adapt<IEnumerable<JogadorViewModel>>();
        }

        public async Task<IEnumerable<JogadorViewModel>> SearchAsync(string searchTerm)
        {
            var jogadores = await _jogadorRepository.SearchAsync(searchTerm);
            return jogadores.Adapt<IEnumerable<JogadorViewModel>>();
        }

        public async Task<bool> AddAsync(JogadorViewModel jogadorViewModel)
        {
            try
            {
                if (!await _timeRepository.ExistsAsync(jogadorViewModel.TimeId))
                    return false;

                if (await _jogadorRepository.NicknameExistsAsync(jogadorViewModel.Nickname))
                    return false;

                var jogador = jogadorViewModel.Adapt<Jogador>();
                await _jogadorRepository.AddAsync(jogador);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(JogadorViewModel jogadorViewModel)
        {
            try
            {
                if (!await _timeRepository.ExistsAsync(jogadorViewModel.TimeId))
                    return false;

                if (await _jogadorRepository.NicknameExistsAsync(jogadorViewModel.Nickname, jogadorViewModel.Id))
                    return false;

                var jogador = jogadorViewModel.Adapt<Jogador>();
                await _jogadorRepository.UpdateAsync(jogador);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _jogadorRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _jogadorRepository.ExistsAsync(id);
        }
    }
}
