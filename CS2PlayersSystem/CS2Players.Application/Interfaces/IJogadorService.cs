using CS2Players.Application.ViewModels;

namespace CS2Players.Application.Interfaces
{
    public interface IJogadorService
    {
        Task<IEnumerable<JogadorViewModel>> GetAllAsync();
        Task<JogadorViewModel> GetByIdAsync(int id);
        Task<IEnumerable<JogadorViewModel>> GetByTimeIdAsync(int timeId);
        Task<IEnumerable<JogadorViewModel>> SearchAsync(string termo);
        Task AddAsync(JogadorViewModel jogadorViewModel);
        Task UpdateAsync(JogadorViewModel jogadorViewModel);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}