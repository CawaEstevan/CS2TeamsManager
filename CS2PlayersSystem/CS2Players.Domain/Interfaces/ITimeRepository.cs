using CS2Players.Domain.Entities;

namespace CS2Players.Domain.Interfaces
{
    public interface ITimeRepository
    {
        Task<IEnumerable<Time>> GetAllAsync();
        Task<Time> GetByIdAsync(int id);
        Task<Time> GetByIdWithJogadoresAsync(int id);
        Task<IEnumerable<Time>> SearchAsync(string termo);
        Task AddAsync(Time time);
        Task UpdateAsync(Time time);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}