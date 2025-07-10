using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Booble_IA_API._1___Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmizadeController : ControllerBase
    {
        private readonly IAmizadeService _amizadeService;

        public AmizadeController(IAmizadeService amizadeService)
        {
            _amizadeService = amizadeService;
        }

        // GET: api/Amizade/usuario?idfUsuario=5
        [HttpGet("usuario")]
        public async Task<ActionResult<List<Amizade>>> ObterAmizadePorIdUsuario([FromQuery] int idfUsuario)
        {
            var amizades = await _amizadeService.ObterAmizadePorIdUsuario(idfUsuario);
            if (amizades == null || amizades.Count == 0)
                return NotFound();
            return Ok(amizades);
        }

        // POST: api/Amizade/aceitar?idfAmizade=5
        [HttpPost("aceitar")]
        public async Task<ActionResult> AceitarAmizade([FromQuery] int idfAmizade)
        {
            var result = await _amizadeService.AceitarAmizade(idfAmizade);
            if (!result)
                return BadRequest("Não foi possível aceitar a amizade.");
            return Ok();
        }

        // POST: api/Amizade/recusar?idfAmizade=5
        [HttpPost("recusar")]
        public async Task<ActionResult> RecusarAmizade([FromQuery] int idfAmizade)
        {
            var result = await _amizadeService.RecusarAmizade(idfAmizade);
            if (!result)
                return BadRequest("Não foi possível recusar a amizade.");
            return Ok();
        }

        // POST: api/Amizade/desfazer?idfAmizade=5
        [HttpPost("desfazer")]
        public async Task<ActionResult> DesfazerAmizade([FromQuery] int idfAmizade)
        {
            var result = await _amizadeService.DesfazerAmizade(idfAmizade);
            if (!result)
                return BadRequest("Não foi possível desfazer a amizade.");
            return Ok();
        }
    }
}