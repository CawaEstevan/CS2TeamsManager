using System.Collections.Generic;
using System.Threading.Tasks;
using CS2Teams.Domain.Entities;

namespace CS2Teams.Domain.Interfaces
{
    public interface IJogadorRepository
    {
        Task<IEnumerable<Jogador>> GetAllAsync();
        Task<Jogador?> GetByIdAsync(int id);
        Task<Jogador?> GetByIdWithTimeAsync(int id);
        Task<IEnumerable<Jogador>> GetByTimeIdAsync(int timeId);
        Task<IEnumerable<Jogador>> SearchAsync(string searchTerm);
        Task AddAsync(Jogador jogador);
        Task UpdateAsync(Jogador jogador);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> NicknameExistsAsync(string nickname, int? excludeId = null);
    }
}