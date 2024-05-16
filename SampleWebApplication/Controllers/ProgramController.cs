using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebApplication.Data;
using SampleWebApplication.Model;

namespace SampleWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly SampleWebApplicationContext _context;

        public ProgramController(SampleWebApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Program
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramDTO>>> GetProgramDTO()
        {
            return await _context.ProgramDTO.ToListAsync();
        }

        // GET: api/Program/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramDTO>> GetProgramDTO(string id)
        {
            var programDTO = await _context.ProgramDTO.FindAsync(id);

            if (programDTO == null)
            {
                return NotFound();
            }

            return programDTO;
        }

        // PUT: api/Program/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramDTO(string id, ProgramDTO programDTO)
        {
            if (id != programDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(programDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramDTOExists(id))
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

        // POST: api/Program
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProgramDTO>> PostProgramDTO(ProgramDTO programDTO)
        {
            _context.ProgramDTO.Add(programDTO);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProgramDTOExists(programDTO.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProgramDTO", new { id = programDTO.Id }, programDTO);
        }

        // DELETE: api/Program/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramDTO(string id)
        {
            var programDTO = await _context.ProgramDTO.FindAsync(id);
            if (programDTO == null)
            {
                return NotFound();
            }

            _context.ProgramDTO.Remove(programDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramDTOExists(string id)
        {
            return _context.ProgramDTO.Any(e => e.Id == id);
        }
    }
}
