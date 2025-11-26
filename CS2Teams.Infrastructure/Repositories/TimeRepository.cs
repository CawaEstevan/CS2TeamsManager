using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS2Teams.Domain.Entities;
using CS2Teams.Domain.Interfaces;
using CS2Teams.Infrastructure.Data;

namespace CS2Teams.Infrastructure.Repositories
{
    public class TimeRepository : ITimeRepository
    {
        private readonly AppDbContext _context;

        public TimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Time>> GetAllAsync()
        {
            return await _context.Times
                .Include(t => t.Jogadores)
                .OrderBy(t => t.Ranking)
                .ToListAsync();
        }

        public async Task<Time?> GetByIdAsync(int id)
        {
            return await _context.Times
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Time?> GetByIdWithJogadoresAsync(int id)
        {
            return await _context.Times
                .Include(t => t.Jogadores)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Time>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync();
            }

            searchTerm = searchTerm.ToLower().Trim();

            return await _context.Times
                .Include(t => t.Jogadores)
                .Where(t => t.Nome.ToLower().Contains(searchTerm) ||
                           t.Tag.ToLower().Contains(searchTerm) ||
                           t.Pais.ToLower().Contains(searchTerm))
                .OrderBy(t => t.Ranking)
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

        public async Task<bool> TagExistsAsync(string tag, int? excludeId = null)
        {
            var query = _context.Times.Where(t => t.Tag == tag);
            
            if (excludeId.HasValue)
            {
                query = query.Where(t => t.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}