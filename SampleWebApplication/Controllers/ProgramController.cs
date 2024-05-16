using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebApplication.Data;
using SampleWebApplication.Model;
using SampleWebApplication.Domain;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using SampleWebApplication.Services;

namespace SampleWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly SampleWebApplicationContext _context;
        private IProgramService _programService;

        public ProgramController( IProgramService programService , SampleWebApplicationContext context)
        {
            _programService = programService;
            _context = context;
        }

        // GET: api/Program/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramDTO>> GetProgramDTO(string id)
        {
            return await _programService.GetProgramById(id);
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
            return await _programService.CreateProgram(programDTO);
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
