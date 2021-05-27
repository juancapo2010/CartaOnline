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
    public class FormaEntregasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FormaEntregasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FormaEntregas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormaEntrega>>> GetFormasEntregas()
        {
            return await _context.FormasEntregas.ToListAsync();
        }

        // GET: api/FormaEntregas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormaEntrega>> GetFormaEntrega(int id)
        {
            var formaEntrega = await _context.FormasEntregas.FindAsync(id);

            if (formaEntrega == null)
            {
                return NotFound();
            }

            return formaEntrega;
        }

        // PUT: api/FormaEntregas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaEntrega(int id, FormaEntrega formaEntrega)
        {
            if (id != formaEntrega.FormaEntregaId)
            {
                return BadRequest();
            }

            _context.Entry(formaEntrega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormaEntregaExists(id))
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

        // POST: api/FormaEntregas
        [HttpPost]
        public async Task<ActionResult<FormaEntrega>> PostFormaEntrega(FormaEntrega formaEntrega)
        {
            _context.FormasEntregas.Add(formaEntrega);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormaEntrega", new { id = formaEntrega.FormaEntregaId }, formaEntrega);
        }

        // DELETE: api/FormaEntregas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FormaEntrega>> DeleteFormaEntrega(int id)
        {
            var formaEntrega = await _context.FormasEntregas.FindAsync(id);
            if (formaEntrega == null)
            {
                return NotFound();
            }

            _context.FormasEntregas.Remove(formaEntrega);
            await _context.SaveChangesAsync();

            return formaEntrega;
        }

        private bool FormaEntregaExists(int id)
        {
            return _context.FormasEntregas.Any(e => e.FormaEntregaId == id);
        }
    }
}
