using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CocaAssetMonitoring.Models.SystemModels;
using CocaAssetMonitoring.Persistence.Context;

namespace CocaAssetMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MCAttributesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MCAttributesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MCAttributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MCAttributes>>> GetMCAttributes()
        {
            return await _context.MCAttributes.ToListAsync();
        }

        // GET: api/MCAttributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MCAttributes>> GetMCAttributes(int id)
        {
            var mCAttributes = await _context.MCAttributes.FindAsync(id);

            if (mCAttributes == null)
            {
                return NotFound();
            }

            return mCAttributes;
        }

        // PUT: api/MCAttributes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMCAttributes(int id, MCAttributes mCAttributes)
        {
            if (id != mCAttributes.Id)
            {
                return BadRequest();
            }

            _context.Entry(mCAttributes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MCAttributesExists(id))
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

        // POST: api/MCAttributes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MCAttributes>> PostMCAttributes(MCAttributes mCAttributes)
        {
            _context.MCAttributes.Add(mCAttributes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMCAttributes", new { id = mCAttributes.Id }, mCAttributes);
        }

        // DELETE: api/MCAttributes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MCAttributes>> DeleteMCAttributes(int id)
        {
            var mCAttributes = await _context.MCAttributes.FindAsync(id);
            if (mCAttributes == null)
            {
                return NotFound();
            }

            _context.MCAttributes.Remove(mCAttributes);
            await _context.SaveChangesAsync();

            return mCAttributes;
        }

        private bool MCAttributesExists(int id)
        {
            return _context.MCAttributes.Any(e => e.Id == id);
        }
    }
}
