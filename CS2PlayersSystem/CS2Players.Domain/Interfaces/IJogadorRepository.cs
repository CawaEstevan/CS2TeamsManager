using CS2Players.Domain.Entities;

namespace CS2Players.Domain.Interfaces
{
    public interface IJogadorRepository
    {
        Task<IEnumerable<Jogador>> GetAllAsync();
        Task<Jogador> GetByIdAsync(int id);
        Task<IEnumerable<Jogador>> GetByTimeIdAsync(int timeId);
        Task<IEnumerable<Jogador>> SearchAsync(string termo);
        Task AddAsync(Jogador jogador);
        Task UpdateAsync(Jogador jogador);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> NicknameExistsAsync(string nickname, int? excludeId = null);
    }
}