using AssetwiseApi.Context;
using AssetwiseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetwiseApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly AWContext _context;

        public AssetController(AWContext context)
        {
            _context = context;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            return await _context.Assets.ToListAsync();
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            var product = await _context.Assets.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Assets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(int id, Asset product)
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
                if (!AssetExists(id))
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

        // POST: api/Assets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asset>> PostAsset(Asset product)
        {
            _context.Assets.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsset", new { id = product.Id }, product);
        }

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var product = await _context.Assets.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Assets.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }

        //loading related data
        [HttpGet("GetSupplierFromAssetName")]
        public async Task<Asset?> GetAssetName(string? productName)
        {
            return await _context.Assets
                .Include(p => p.ServiceHistory)
                // .Where(p => p. == productName)
                .FirstOrDefaultAsync();
        }

    }
