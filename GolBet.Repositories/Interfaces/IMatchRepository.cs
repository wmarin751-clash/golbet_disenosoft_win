using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GolBet.Entities;
using GolBet.Entities.Enums;

namespace GolBet.Repositories.Interfaces
{
    public interface IMatchRepository
    {
        Task<IEnumerable<Match>> GetAllWithTeamsAsync(MatchStatus? status = null);
    }
}