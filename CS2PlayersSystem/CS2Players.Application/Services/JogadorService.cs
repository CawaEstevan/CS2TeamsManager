using CS2Players.Application.Interfaces;
using CS2Players.Application.ViewModels;
using CS2Players.Domain.Entities;
using CS2Players.Domain.Interfaces;
using Mapster;

namespace CS2Players.Application.Services
{
    public class JogadorService : IJogadorService
    {
        private readonly IJogadorRepository _jogadorRepository;

        public JogadorService(IJogadorRepository jogadorRepository)
        {
            _jogadorRepository = jogadorRepository;
        }

        public async Task<IEnumerable<JogadorViewModel>> GetAllAsync()
        {
            var jogadores = await _jogadorRepository.GetAllAsync();
            return jogadores.Adapt<IEnumerable<JogadorViewModel>>();
        }

        public async Task<JogadorViewModel> GetByIdAsync(int id)
        {
            var jogador = await _jogadorRepository.GetByIdAsync(id);
            return jogador?.Adapt<JogadorViewModel>();
        }

        public async Task<IEnumerable<JogadorViewModel>> GetByTimeIdAsync(int timeId)
        {
            var jogadores = await _jogadorRepository.GetByTimeIdAsync(timeId);
            return jogadores.Adapt<IEnumerable<JogadorViewModel>>();
        }

        public async Task<IEnumerable<JogadorViewModel>> SearchAsync(string termo)
        {
            var jogadores = await _jogadorRepository.SearchAsync(termo);
            return jogadores.Adapt<IEnumerable<JogadorViewModel>>();
        }

        public async Task AddAsync(JogadorViewModel jogadorViewModel)
        {
            var jogador = jogadorViewModel.Adapt<Jogador>();
            await _jogadorRepository.AddAsync(jogador);
        }

        public async Task UpdateAsync(JogadorViewModel jogadorViewModel)
        {
            var jogador = jogadorViewModel.Adapt<Jogador>();
            await _jogadorRepository.UpdateAsync(jogador);
        }

        public async Task DeleteAsync(int id)
        {
            await _jogadorRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _jogadorRepository.ExistsAsync(id);
        }
    }
}