using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KindsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public KindsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Kinds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kind>>> GetKinds()
        {
          if (_context.Kinds == null)
          {
              return NotFound();
          }
            return await _context.Kinds.ToListAsync();
        }

        // GET: api/Kinds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kind>> GetKind(int id)
        {
          if (_context.Kinds == null)
          {
              return NotFound();
          }
            var kind = await _context.Kinds.FindAsync(id);

            if (kind == null)
            {
                return NotFound();
            }

            return kind;
        }

        // PUT: api/Kinds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKind(int id, Kind kind)
        {
            if (id != kind.Id)
            {
                return BadRequest();
            }

            _context.Entry(kind).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KindExists(id))
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

        // POST: api/Kinds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kind>> PostKind(Kind kind)
        {
          if (_context.Kinds == null)
          {
              return Problem("Entity set 'MyDbContext.Kinds'  is null.");
          }
            _context.Kinds.Add(kind);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKind", new { id = kind.Id }, kind);
        }

        // DELETE: api/Kinds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKind(int id)
        {
            if (_context.Kinds == null)
            {
                return NotFound();
            }
            var kind = await _context.Kinds.FindAsync(id);
            if (kind == null)
            {
                return NotFound();
            }

            _context.Kinds.Remove(kind);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KindExists(int id)
        {
            return (_context.Kinds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
