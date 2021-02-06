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
    public class MachineProcessesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MachineProcessesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MachineProcesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineProcess>>> GetMachineProcesses()
        {
            return await _context.MachineProcesses.ToListAsync();
        }

        // GET: api/MachineProcesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MachineProcess>> GetMachineProcess(int id)
        {
            var machineProcess = await _context.MachineProcesses.FindAsync(id);

            if (machineProcess == null)
            {
                return NotFound();
            }

            return machineProcess;
        }

        // PUT: api/MachineProcesses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMachineProcess(int id, MachineProcess machineProcess)
        {
            if (id != machineProcess.Id)
            {
                return BadRequest();
            }

            _context.Entry(machineProcess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineProcessExists(id))
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

        // POST: api/MachineProcesses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MachineProcess>> PostMachineProcess(MachineProcess machineProcess)
        {
            _context.MachineProcesses.Add(machineProcess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMachineProcess", new { id = machineProcess.Id }, machineProcess);
        }

        // DELETE: api/MachineProcesses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MachineProcess>> DeleteMachineProcess(int id)
        {
            var machineProcess = await _context.MachineProcesses.FindAsync(id);
            if (machineProcess == null)
            {
                return NotFound();
            }

            _context.MachineProcesses.Remove(machineProcess);
            await _context.SaveChangesAsync();

            return machineProcess;
        }

        private bool MachineProcessExists(int id)
        {
            return _context.MachineProcesses.Any(e => e.Id == id);
        }
    }
}
