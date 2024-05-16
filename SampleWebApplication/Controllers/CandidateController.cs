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
    public class CandidateController : ControllerBase
    {
        private readonly SampleWebApplicationContext _context;

        public CandidateController(SampleWebApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Candidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidateDTO()
        {
            return await _context.CandidateDTO.ToListAsync();
        }

        // GET: api/Candidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidateDTO(string id)
        {
            var candidateDTO = await _context.CandidateDTO.FindAsync(id);

            if (candidateDTO == null)
            {
                return NotFound();
            }

            return candidateDTO;
        }

        // PUT: api/Candidate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidateDTO(string id, CandidateDTO candidateDTO)
        {
            if (id != candidateDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(candidateDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateDTOExists(id))
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

        // POST: api/Candidate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> PostCandidateDTO(CandidateDTO candidateDTO)
        {
            _context.CandidateDTO.Add(candidateDTO);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CandidateDTOExists(candidateDTO.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCandidateDTO", new { id = candidateDTO.Id }, candidateDTO);
        }

        // DELETE: api/Candidate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidateDTO(string id)
        {
            var candidateDTO = await _context.CandidateDTO.FindAsync(id);
            if (candidateDTO == null)
            {
                return NotFound();
            }

            _context.CandidateDTO.Remove(candidateDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidateDTOExists(string id)
        {
            return _context.CandidateDTO.Any(e => e.Id == id);
        }
    }
}
