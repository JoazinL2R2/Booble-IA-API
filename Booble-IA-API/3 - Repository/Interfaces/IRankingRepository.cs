using System.Collections.Generic;
using System.Threading.Tasks;
using Booble_IA_API._3___Repository.Entities;

namespace Booble_IA_API._3___Repository.Interfaces
{
    public interface IRankingRepository
    {
        Task<List<RankingStreakDTO>> GetRankingStreakAsync();
    }
}
