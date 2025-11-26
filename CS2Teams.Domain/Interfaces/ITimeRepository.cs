using System.Collections.Generic;
using System.Threading.Tasks;
using CS2Teams.Domain.Entities;

namespace CS2Teams.Domain.Interfaces
{
    public interface ITimeRepository
    {
        Task<IEnumerable<Time>> GetAllAsync();
        Task<Time?> GetByIdAsync(int id);
        Task<Time?> GetByIdWithJogadoresAsync(int id);
        Task<IEnumerable<Time>> SearchAsync(string searchTerm);
        Task AddAsync(Time time);
        Task UpdateAsync(Time time);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> TagExistsAsync(string tag, int? excludeId = null);
    }
}