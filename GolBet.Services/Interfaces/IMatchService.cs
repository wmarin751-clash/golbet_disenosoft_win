// GolBet.Services/Interfaces/IMatchService.cs
using GolBet.Entities.Enums;
using GolBet.Services.DTOs;

namespace GolBet.Services.Interfaces;

public interface IMatchService
{
    /// <summary>Match board: all active matches ordered by date.</summary>
    Task<IEnumerable<MatchDto>> GetAllWithTeamsAsync(MatchStatus? status = null, string? searchTerm = null);
    Task<MatchDetailDto?> GetDetailAsync(int id);

    Task<MatchFormDto?> GetForEditAsync(int id);
    Task CreateAsync(MatchFormDto dto);
    Task UpdateAsync(MatchFormDto dto);
    Task DeactivateAsync(int id);

}

