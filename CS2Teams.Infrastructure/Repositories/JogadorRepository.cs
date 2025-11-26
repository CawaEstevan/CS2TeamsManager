using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS2Teams.Domain.Entities;
using CS2Teams.Domain.Interfaces;
using CS2Teams.Infrastructure.Data;

namespace CS2Teams.Infrastructure.Repositories
{
    public class JogadorRepository : IJogadorRepository
    {
        private readonly AppDbContext _context;

        public JogadorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Jogador>> GetAllAsync()
        {
            return await _context.Jogadores
                .Include(j => j.Time)
                .OrderByDescending(j => j.Rating)
                .ToListAsync();
        }

        public async Task<Jogador?> GetByIdAsync(int id)
        {
            return await _context.Jogadores
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<Jogador?> GetByIdWithTimeAsync(int id)
        {
            return await _context.Jogadores
                .Include(j => j.Time)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<IEnumerable<Jogador>> GetByTimeIdAsync(int timeId)
        {
            return await _context.Jogadores
                .Include(j => j.Time)
                .Where(j => j.TimeId == timeId)
                .OrderByDescending(j => j.Rating)
                .ToListAsync();
        }

        public async Task<IEnumerable<Jogador>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            searchTerm = searchTerm.ToLower().Trim();

            return await _context.Jogadores
                .Include(j => j.Time)
                .Where(j => j.Nickname.ToLower().Contains(searchTerm) ||
                           j.NomeReal.ToLower().Contains(searchTerm) ||
                           j.Nacionalidade.ToLower().Contains(searchTerm) ||
                           (j.Time != null && j.Time.Nome.ToLower().Contains(searchTerm)))
                .OrderByDescending(j => j.Rating)
                .ToListAsync();
        }

        public async Task AddAsync(Jogador jogador)
        {
            await _context.Jogadores.AddAsync(jogador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Jogador jogador)
        {
            _context.Jogadores.Update(jogador);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var jogador = await GetByIdAsync(id);
            if (jogador != null)
            {
                _context.Jogadores.Remove(jogador);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Jogadores.AnyAsync(j => j.Id == id);
        }

        public async Task<bool> NicknameExistsAsync(string nickname, int? excludeId = null)
        {
            var query = _context.Jogadores.Where(j => j.Nickname == nickname);

            if (excludeId.HasValue)
                query = query.Where(j => j.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}
