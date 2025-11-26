using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using CS2Teams.Application.Interfaces;
using CS2Teams.Application.ViewModels;
using CS2Teams.Domain.Entities;
using CS2Teams.Domain.Interfaces;

namespace CS2Teams.Application.Services
{
    public class TimeService : ITimeService
    {
        private readonly ITimeRepository _timeRepository;

        public TimeService(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public async Task<IEnumerable<TimeViewModel>> GetAllAsync()
        {
            var times = await _timeRepository.GetAllAsync();
            return times.Adapt<IEnumerable<TimeViewModel>>();
        }

        public async Task<TimeViewModel?> GetByIdAsync(int id)
        {
            var time = await _timeRepository.GetByIdAsync(id);
            return time?.Adapt<TimeViewModel>();
        }

        public async Task<TimeViewModel?> GetByIdWithJogadoresAsync(int id)
        {
            var time = await _timeRepository.GetByIdWithJogadoresAsync(id);
            return time?.Adapt<TimeViewModel>();
        }

        public async Task<IEnumerable<TimeViewModel>> SearchAsync(string searchTerm)
        {
            var times = await _timeRepository.SearchAsync(searchTerm);
            return times.Adapt<IEnumerable<TimeViewModel>>();
        }

        public async Task<bool> AddAsync(TimeViewModel timeViewModel)
        {
            try
            {
                // Verifica se a tag já existe
                if (await _timeRepository.TagExistsAsync(timeViewModel.Tag))
                {
                    return false;
                }

                var time = timeViewModel.Adapt<Time>();
                await _timeRepository.AddAsync(time);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TimeViewModel timeViewModel)
        {
            try
            {
                // Verifica se a tag já existe em outro time
                if (await _timeRepository.TagExistsAsync(timeViewModel.Tag, timeViewModel.Id))
                {
                    return false;
                }

                var time = timeViewModel.Adapt<Time>();
                await _timeRepository.UpdateAsync(time);
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
                await _timeRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _timeRepository.ExistsAsync(id);
        }
    }
}