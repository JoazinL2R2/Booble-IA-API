using System.Collections.Generic;
using System.Threading.Tasks;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API._3___Repository.Interfaces;

namespace Booble_IA_API._2___Services
{
    public class RankingService : IRankingService
    {
        private readonly IRankingRepository _rankingRepository;

        public RankingService(IRankingRepository rankingRepository)
        {
            _rankingRepository = rankingRepository;
        }

        public async Task<List<RankingStreakDTO>> GetRankingStreakAsync()
        {
            return await _rankingRepository.GetRankingStreakAsync();
        }
    }
}
