using MemoryGame_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryGame_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private readonly MemorygameContext _context;

        public NamesController(MemorygameContext context)
        {
            _context = context;
        }

        // GET: api/Names
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Names>>> GetNames()
        {
            return await _context.Names.ToListAsync();
        }

        // GET api/Names/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Names>> GetNames(int id)
        {
            var names = await _context.Names.FindAsync(id);

            if (names == null)
            {
                return NotFound();
            }

            return names;
        }

        // POST api/Names
        [HttpPost]
        public async Task<ActionResult<Names>> PostNames(Names names)
        {
            _context.Names.Add(names);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNames), new { id = names.Id }, names);
        }

        // PUT api/Names/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNames(int id, Names names)
        {
            if (id != names.Id)
            {
                return BadRequest();
            }

            _context.Entry(names).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NamesExists(id))
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

        // DELETE api/Names/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNames(int id)
        {
            var names = await _context.Names.FindAsync(id);
            if (names == null)
            {
                return NotFound();
            }

            _context.Names.Remove(names);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NamesExists(int id)
        {
            return _context.Names.Any(e => e.Id == id);
        }
    }
}
