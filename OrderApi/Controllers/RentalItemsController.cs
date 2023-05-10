using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentalItemsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public RentalItemsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/RentalItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalItem>>> GetRentalItems(int? kindId)
        {
           if (_context.RentalItems == null)
           {
              return NotFound();
           }

            IQueryable<RentalItem> rentalItems = _context.RentalItems.Include(_ => _.Kind);

            
            if (kindId.HasValue)
            {
                rentalItems = rentalItems.Where(r => r.KindId == kindId);
            }
            return await rentalItems.ToListAsync();
        }

        // GET: api/RentalItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalItem>> GetRentalItem(int id)
        {
          if (_context.RentalItems == null)
          {
              return NotFound();
          }
            var rentalItem = await _context.RentalItems.FindAsync(id);

            if (rentalItem == null)
            {
                return NotFound();
            }

            return rentalItem;
        }

        // PUT: api/RentalItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalItem(int id, RentalItem rentalItem)
        {
            if (id != rentalItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(rentalItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalItemExists(id))
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

        // POST: api/RentalItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RentalItem>> PostRentalItem(RentalItem rentalItem)
        {
          if (_context.RentalItems == null)
          {
              return Problem("Entity set 'MyDbContext.RentalItems'  is null.");
          }
            _context.RentalItems.Add(rentalItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentalItem", new { id = rentalItem.Id }, rentalItem);
        }

        // DELETE: api/RentalItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalItem(int id)
        {
            if (_context.RentalItems == null)
            {
                return NotFound();
            }
            var rentalItem = await _context.RentalItems.FindAsync(id);
            if (rentalItem == null)
            {
                return NotFound();
            }

            _context.RentalItems.Remove(rentalItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalItemExists(int id)
        {
            return (_context.RentalItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
