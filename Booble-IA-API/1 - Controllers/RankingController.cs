using System.Collections.Generic;
using System.Threading.Tasks;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Booble_IA_API._1___Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        [HttpGet("streak")]
        public async Task<ActionResult<List<RankingStreakDTO>>> GetRankingStreak()
        {
            var ranking = await _rankingService.GetRankingStreakAsync();
            if (ranking == null || ranking.Count == 0)
                return NotFound();
            return Ok(ranking);
        }
    }
}
