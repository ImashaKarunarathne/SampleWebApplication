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
    public class FormController : ControllerBase
    {
        private readonly SampleWebApplicationContext _context;

        public FormController(SampleWebApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Form
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormDTO>>> GetFormDTO()
        {
            return await _context.FormDTO.ToListAsync();
        }

        // GET: api/Form/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormDTO>> GetFormDTO(string id)
        {
            var formDTO = await _context.FormDTO.FindAsync(id);

            if (formDTO == null)
            {
                return NotFound();
            }

            return formDTO;
        }

        // PUT: api/Form/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormDTO(string id, FormDTO formDTO)
        {
            if (id != formDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(formDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormDTOExists(id))
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

        // POST: api/Form
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FormDTO>> PostFormDTO(FormDTO formDTO)
        {
            _context.FormDTO.Add(formDTO);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FormDTOExists(formDTO.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFormDTO", new { id = formDTO.Id }, formDTO);
        }

        // DELETE: api/Form/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormDTO(string id)
        {
            var formDTO = await _context.FormDTO.FindAsync(id);
            if (formDTO == null)
            {
                return NotFound();
            }

            _context.FormDTO.Remove(formDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormDTOExists(string id)
        {
            return _context.FormDTO.Any(e => e.Id == id);
        }
    }
}
