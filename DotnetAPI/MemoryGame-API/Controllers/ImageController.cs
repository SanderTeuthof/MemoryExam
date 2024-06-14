using MemoryGame_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryGame_API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly MemorygameContext _context;

        public ImageController(MemorygameContext context)
        {
            _context = context;
        }

        // GET: api/Image
        [HttpGet]
        public async Task<IEnumerable<Image>> Get()
        {
            return await _context.Images.ToListAsync();
        }

        // GET api/Image/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            return File(image.ImageData, "image/png");
        }

        // POST api/Image
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Image image)
        {
            if (_context.Images.Any(i => i.ImageName == image.ImageName))
            {
                return Conflict("An image with the same name already exists.");
            }

            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = image.Id }, image);
        }

        // PUT api/Image/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Image image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            var existingImage = await _context.Images.FindAsync(id);
            if (existingImage == null)
            {
                return NotFound();
            }

            existingImage.ImageName = image.ImageName;
            existingImage.ImageData = image.ImageData;

            _context.Entry(existingImage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/Image/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
