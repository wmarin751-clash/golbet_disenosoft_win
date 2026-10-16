// GolBet.Repositories/Implementations/MatchRepository.cs
using GolBet.Entities;
using GolBet.Entities.Enums;
using GolBet.Repositories.Data;
using GolBet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Repositories.Implementations;

public class MatchRepository : GenericRepository<Match>, IMatchRepository
{
    public MatchRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Match>> GetAllWithTeamsAsync(MatchStatus? status = null, string? searchTerm = null)
    {
        var query = _dbSet
            .Include(m => m.HomeTeam) //El include viene siendo un JOIN en SQL
            .Include(m => m.AwayTeam)
            .Where(m => m.IsActive)
            .AsNoTracking()
            .AsQueryable(); //El AsQueryable es para poder aplicar filtros dinámicos

        if (status.HasValue)
            query = query.Where(m => m.Status == status.Value);

        // Nuevo filtro de búsqueda por nombre de equipo local o visitante
        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(m => m.HomeTeam.Name.ToLower().Contains(searchTerm) ||
                                     m.AwayTeam.Name.ToLower().Contains(searchTerm));
        }

        return await query.OrderBy(m => m.Date).ToListAsync();
    }

    public async Task<Match?> GetByIdWithDetailsAsync(int id)
        => await _dbSet
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Bets)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
}
