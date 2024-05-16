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
    public class EmployerController : ControllerBase
    {
        private readonly SampleWebApplicationContext _context;

        public EmployerController(SampleWebApplicationContext context)
        {
            _context = context;
        }

        // GET: api/EmployerDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployerDTO>>> GetEmployerDTO()
        {
            return await _context.EmployerDTO.ToListAsync();
        }

        // GET: api/EmployerDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployerDTO>> GetEmployerDTO(string id)
        {
            var employerDTO = await _context.EmployerDTO.FindAsync(id);

            if (employerDTO == null)
            {
                return NotFound();
            }

            return employerDTO;
        }

        // PUT: api/EmployerDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployerDTO(string id, EmployerDTO employerDTO)
        {
            if (id != employerDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(employerDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployerDTOExists(id))
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

        // POST: api/EmployerDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployerDTO>> PostEmployerDTO(EmployerDTO employerDTO)
        {
            _context.EmployerDTO.Add(employerDTO);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployerDTOExists(employerDTO.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployerDTO", new { id = employerDTO.Id }, employerDTO);
        }

        // DELETE: api/EmployerDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployerDTO(string id)
        {
            var employerDTO = await _context.EmployerDTO.FindAsync(id);
            if (employerDTO == null)
            {
                return NotFound();
            }

            _context.EmployerDTO.Remove(employerDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployerDTOExists(string id)
        {
            return _context.EmployerDTO.Any(e => e.Id == id);
        }
    }
}
