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
    public class MachinePerformancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MachinePerformancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MachinePerformances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachinePerformance>>> GetMachinePerformance()
        {
            return await _context.MachinePerformance.ToListAsync();
        }

        // GET: api/MachinePerformances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MachinePerformance>> GetMachinePerformance(int id)
        {
            var machinePerformance = await _context.MachinePerformance.FindAsync(id);

            if (machinePerformance == null)
            {
                return NotFound();
            }

            return machinePerformance;
        }

        // PUT: api/MachinePerformances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMachinePerformance(int id, MachinePerformance machinePerformance)
        {
            if (id != machinePerformance.Id)
            {
                return BadRequest();
            }

            _context.Entry(machinePerformance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachinePerformanceExists(id))
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

        // POST: api/MachinePerformances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MachinePerformance>> PostMachinePerformance(MachinePerformance machinePerformance)
        {
            _context.MachinePerformance.Add(machinePerformance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMachinePerformance", new { id = machinePerformance.Id }, machinePerformance);
        }

        // DELETE: api/MachinePerformances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MachinePerformance>> DeleteMachinePerformance(int id)
        {
            var machinePerformance = await _context.MachinePerformance.FindAsync(id);
            if (machinePerformance == null)
            {
                return NotFound();
            }

            _context.MachinePerformance.Remove(machinePerformance);
            await _context.SaveChangesAsync();

            return machinePerformance;
        }

        private bool MachinePerformanceExists(int id)
        {
            return _context.MachinePerformance.Any(e => e.Id == id);
        }
    }
}
