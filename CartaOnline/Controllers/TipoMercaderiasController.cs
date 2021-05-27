using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartaOnline.Config;
using CartaOnline.Models;

namespace CartaOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMercaderiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoMercaderiaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoMercaderia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMercaderia>>> GetTipoMercaderia()
        {
            return await _context.TipoMercaderia.ToListAsync();
        }

        // GET: api/TipoMercaderia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMercaderia>> GetTipoMercaderia(int id)
        {
            var tipoMercaderia = await _context.TipoMercaderia.FindAsync(id);

            if (tipoMercaderia == null)
            {
                return NotFound();
            }

            return tipoMercaderia;
        }

        // PUT: api/TipoMercaderia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoMercaderia(int id, TipoMercaderia tipoMercaderia)
        {
            if (id != tipoMercaderia.TipoMercaderiaId)
            {
                return BadRequest();
            }

            _context.Entry(tipoMercaderia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoMercaderiaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TipoMercaderia
        [HttpPost]
        public async Task<ActionResult<TipoMercaderia>> PostTipoMercaderia(TipoMercaderia tipoMercaderia)
        {
            _context.TipoMercaderia.Add(tipoMercaderia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoMercaderia", new { id = tipoMercaderia.TipoMercaderiaId }, tipoMercaderia);
        }

        // DELETE: api/TipoMercaderia/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoMercaderia>> DeleteTipoMercaderia(int id)
        {
            var tipoMercaderia = await _context.TipoMercaderia.FindAsync(id);
            if (tipoMercaderia == null)
            {
                return NotFound();
            }

            _context.TipoMercaderia.Remove(tipoMercaderia);
            await _context.SaveChangesAsync();

            return tipoMercaderia;
        }

        private bool TipoMercaderiaExists(int id)
        {
            return _context.TipoMercaderia.Any(e => e.TipoMercaderiaId == id);
        }
    }
}
