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

namespace SampleWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmittedAswerFormController : ControllerBase
    {
        private readonly SampleWebApplicationContext _context;
        private readonly ISubmittedAnswerFormService _submittedAnswerFormService;


        public SubmittedAswerFormController(SampleWebApplicationContext context, ISubmittedAnswerFormService submittedAnswerFormService)
        {
            _context = context;
            _submittedAnswerFormService = submittedAnswerFormService;
        }

        // GET: api/SubmittedAswerForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormAnswersDTO>>> GetFormAnswersDTO()
        {
            return await _context.FormAnswersDTO.ToListAsync();
        }

        // GET: api/SubmittedAswerForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormAnswersDTO>> GetFormAnswersDTO(string id)
        {
            return await _submittedAnswerFormService.GetAswerFormById(id);
        }

        // PUT: api/SubmittedAswerForm/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<FormAnswersDTO>> PutFormAnswersDTO(string id, [FromBody] FormAnswersDTO formAnswersDTO)
        {
            return await _submittedAnswerFormService.UpdateAswerForm(id, formAnswersDTO);
        }

        // POST: api/SubmittedAswerForm
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FormAnswersDTO>> PostFormAnswersDTO(FormAnswersDTO formAnswersDTO)
        {
            return await _submittedAnswerFormService.CreateAswerForm(formAnswersDTO);
        }

        // DELETE: api/SubmittedAswerForm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormAnswersDTO(string id)
        {
            var formAnswersDTO = await _context.FormAnswersDTO.FindAsync(id);
            if (formAnswersDTO == null)
            {
                return NotFound();
            }

            _context.FormAnswersDTO.Remove(formAnswersDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormAnswersDTOExists(string id)
        {
            return _context.FormAnswersDTO.Any(e => e.Id == id);
        }
    }
}
