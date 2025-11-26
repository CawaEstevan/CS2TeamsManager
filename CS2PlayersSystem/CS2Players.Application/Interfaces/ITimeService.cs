using CS2Players.Application.ViewModels;

namespace CS2Players.Application.Interfaces
{
    public interface ITimeService
    {
        Task<IEnumerable<TimeViewModel>> GetAllAsync();
        Task<TimeViewModel> GetByIdAsync(int id);
        Task<TimeViewModel> GetByIdWithJogadoresAsync(int id);
        Task<IEnumerable<TimeViewModel>> SearchAsync(string termo);
        Task AddAsync(TimeViewModel timeViewModel);
        Task UpdateAsync(TimeViewModel timeViewModel);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}