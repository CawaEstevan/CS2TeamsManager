using System.Collections.Generic;
using System.Threading.Tasks;
using CS2Teams.Application.ViewModels;

namespace CS2Teams.Application.Interfaces
{
    public interface ITimeService
    {
        Task<IEnumerable<TimeViewModel>> GetAllAsync();
        Task<TimeViewModel?> GetByIdAsync(int id);
        Task<TimeViewModel?> GetByIdWithJogadoresAsync(int id);
        Task<IEnumerable<TimeViewModel>> SearchAsync(string searchTerm);
        Task<bool> AddAsync(TimeViewModel timeViewModel);
        Task<bool> UpdateAsync(TimeViewModel timeViewModel);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
