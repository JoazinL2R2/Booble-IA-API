using System.Collections.Generic;
using System.Threading.Tasks;
using Booble_IA_API._3___Repository.Entities;

namespace Booble_IA_API._2___Services.Interfaces
{
    public interface IRankingService
    {
        Task<List<RankingStreakDTO>> GetRankingStreakAsync();
    }
}
