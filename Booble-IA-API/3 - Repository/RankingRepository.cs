using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Booble_IA_API._3___Repository.Data;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API._3___Repository.Interfaces;

namespace Booble_IA_API._3___Repository
{
    public class RankingRepository : IRankingRepository
    {
        private readonly BoobleContext _context;

        public RankingRepository(BoobleContext context)
        {
            _context = context;
        }

        public async Task<List<RankingStreakDTO>> GetRankingStreakAsync()
        {
            return await _context.Set<RankingStreakDTO>()
                .FromSqlRaw("SELECT * FROM GetRankingStreak()")
                .ToListAsync();
        }
    }
}
