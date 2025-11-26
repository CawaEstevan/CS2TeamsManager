using CS2Players.Domain.Entities;
using CS2Players.Domain.Interfaces;
using CS2Players.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CS2Players.Infrastructure.Repositories
{
    public class JogadorRepository : IJogadorRepository
    {
        private readonly ApplicationDbContext _context;

        public JogadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Jogador>> GetAllAsync()
        {
            return await _context.Jogadores
                .Include(j => j.Time)
                .OrderBy(j => j.Nickname)
                .ToListAsync();
        }

        public async Task<Jogador> GetByIdAsync(int id)
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
                .OrderBy(j => j.Nickname)
                .ToListAsync();
        }

        public async Task<IEnumerable<Jogador>> SearchAsync(string termo)
        {
            return await _context.Jogadores
                .Include(j => j.Time)
                .Where(j => j.Nickname.Contains(termo) || 
                           j.NomeCompleto.Contains(termo) ||
                           j.Time.Nome.Contains(termo))
                .OrderBy(j => j.Nickname)
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
            {
                query = query.Where(j => j.Id != excludeId.Value);
            }
            
            return await query.AnyAsync();
        }
    }
}