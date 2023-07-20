using AssetwiseApi.Context;
using AssetwiseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetwiseApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly AWContext _context;

        public SitesController(AWContext context)
        {
            _context = context;
        }

        // GET: api/Sites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Site>>> GetSites()
        {
            return await _context.Sites.ToListAsync();
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Site>> GetSite(int id)
        {
            var product = await _context.Sites.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Sites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSite(int id, Site product)
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
                if (!SiteExists(id))
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

        // POST: api/Sites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Site>> PostSite(Site product)
        {
            _context.Sites.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSite", new { id = product.Id }, product);
        }

        // DELETE: api/Sites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            var product = await _context.Sites.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Sites.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteExists(int id)
        {
            return _context.Sites.Any(e => e.Id == id);
        }

        //loading related data
        [HttpGet("GetSupplierFromSiteName")]
        public async Task<Site?> GetSiteName(string? productName)
        {
            return await _context.Sites
                .Include(p => p.Users)
                // .Where(p => p. == productName)
                .FirstOrDefaultAsync();
        }

        // //calling a view
        // [HttpGet("GetAlphabeticalListOfSites")]
        // public async Task<IEnumerable<AlphabeticalListOfSite>> GetAlphabeticalListOfSites()
        // {
        //     return await _context.AlphabeticalListOfSites.ToListAsync();
        // }

        // //calling stored procedure
        // [HttpGet("GetCustOrderHistory")]
        // public async Task<IEnumerable<CustOrderHistory>> GetCustOrderHistory(string customerId)
        // {
        //     return await _contextProcedures.CustOrderHistories
        //         .FromSqlRaw("call cust_order_history({0})", customerId)
        //         .ToListAsync();
        // }
    }
