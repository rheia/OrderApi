using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentalsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public RentalsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {   if (_context.Rentals == null)
            {
                return NotFound();
            }
        
            var rentals = await _context.Rentals.Include(_ => _.Kind).ToListAsync();
            return Ok(rentals);
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
          if (_context.Rentals == null)
          {
              return NotFound();
          }
            var rental = await _context.Rentals
                .Include(_ => _.Kind)
                .Include(_ => _.RentalItem)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

           /* if (rental != null && rental.RentalItem != null)
            {
                _context.Entry(rental.RentalItem).Reference(ri => ri.Item).Load();
            }*/

           
            return rental;
        }

        // PUT: api/Rentals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, Rental rental)
        {
            if (id != rental.Id)
            {
                return BadRequest();
            }

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
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

        // POST: api/Rentals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental(Rental rental)
        {
          if (_context.Rentals == null)
          {
              return Problem("Entity set 'MyDbContext.Rentals'  is null.");
          }
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRental", new { id = rental.Id }, rental);
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalExists(int id)
        {
            return (_context.Rentals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
