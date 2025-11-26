using System.Collections.Generic;
using System.Threading.Tasks;
using CS2Teams.Application.ViewModels;

namespace CS2Teams.Application.Interfaces
{
    public interface IJogadorService
    {
        Task<IEnumerable<JogadorViewModel>> GetAllAsync();
        Task<JogadorViewModel?> GetByIdAsync(int id);
        Task<IEnumerable<JogadorViewModel>> GetByTimeIdAsync(int timeId);
        Task<IEnumerable<JogadorViewModel>> SearchAsync(string searchTerm);
        Task<bool> AddAsync(JogadorViewModel jogadorViewModel);
        Task<bool> UpdateAsync(JogadorViewModel jogadorViewModel);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
