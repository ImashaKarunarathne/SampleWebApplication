using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebApplication.Data;
using SampleWebApplication.Domain;
using SampleWebApplication.Model;
using SampleWebApplication.Shared;

namespace SampleWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly SampleWebApplicationContext _context;
        private IFormService _formService;

        public FormController(IFormService formService, SampleWebApplicationContext context)
        {
            _formService = formService;
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
            return await _formService.GetFormById(id);
        }

        // PUT: api/Form/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<FormDTO>> PutFormDTO(string id, FormDTO formDTO)
        {

            return await _formService.UpdateForm(id , formDTO);
        }

        // POST: api/Form
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FormDTO>> PostFormDTO(FormDTO formDTO)
        {
            return await _formService.CreateForm(formDTO);
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
