using CS2Players.Domain.Entities;
using CS2Players.Domain.Interfaces;
using CS2Players.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CS2Players.Infrastructure.Repositories
{
    public class TimeRepository : ITimeRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Time>> GetAllAsync()
        {
            return await _context.Times
                .Include(t => t.Jogadores)
                .OrderBy(t => t.Nome)
                .ToListAsync();
        }

        public async Task<Time> GetByIdAsync(int id)
        {
            return await _context.Times
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Time> GetByIdWithJogadoresAsync(int id)
        {
            return await _context.Times
                .Include(t => t.Jogadores)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Time>> SearchAsync(string termo)
        {
            return await _context.Times
                .Include(t => t.Jogadores)
                .Where(t => t.Nome.Contains(termo) || t.Regiao.Contains(termo))
                .OrderBy(t => t.Nome)
                .ToListAsync();
        }

        public async Task AddAsync(Time time)
        {
            await _context.Times.AddAsync(time);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Time time)
        {
            _context.Times.Update(time);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var time = await GetByIdAsync(id);
            if (time != null)
            {
                _context.Times.Remove(time);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Times.AnyAsync(t => t.Id == id);
        }
    }
}