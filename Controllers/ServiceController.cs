using AssetwiseApi.Context;
using AssetwiseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetwiseApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly AWContext _context;

        public ServiceController(AWContext context)
        {
            _context = context;
        }

        // GET: api/Service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetService()
        {
            return await _context.Services.ToListAsync();
        }

        // GET: api/Service/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Service/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Service
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service product)
        {
            _context.Services.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = product.Id }, product);
        }

        // DELETE: api/Service/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var product = await _context.Services.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Services.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }

        //loading related data
        [HttpGet("GetSupplierFromServiceName")]
        public async Task<Service?> GetServiceName(string? productName)
        {
            return await _context.Services
                .Include(p => p.Comments)
                // .Where(p => p. == productName)
                .FirstOrDefaultAsync();
        }

    }
