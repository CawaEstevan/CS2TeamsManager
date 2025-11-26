using CS2Players.Application.Interfaces;
using CS2Players.Application.ViewModels;
using CS2Players.Domain.Entities;
using CS2Players.Domain.Interfaces;
using Mapster;

namespace CS2Players.Application.Services
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

        public async Task<TimeViewModel> GetByIdAsync(int id)
        {
            var time = await _timeRepository.GetByIdAsync(id);
            return time?.Adapt<TimeViewModel>();
        }

        public async Task<TimeViewModel> GetByIdWithJogadoresAsync(int id)
        {
            var time = await _timeRepository.GetByIdWithJogadoresAsync(id);
            return time?.Adapt<TimeViewModel>();
        }

        public async Task<IEnumerable<TimeViewModel>> SearchAsync(string termo)
        {
            var times = await _timeRepository.SearchAsync(termo);
            return times.Adapt<IEnumerable<TimeViewModel>>();
        }

        public async Task AddAsync(TimeViewModel timeViewModel)
        {
            var time = timeViewModel.Adapt<Time>();
            await _timeRepository.AddAsync(time);
        }

        public async Task UpdateAsync(TimeViewModel timeViewModel)
        {
            var time = timeViewModel.Adapt<Time>();
            await _timeRepository.UpdateAsync(time);
        }

        public async Task DeleteAsync(int id)
        {
            await _timeRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _timeRepository.ExistsAsync(id);
        }
    }
}