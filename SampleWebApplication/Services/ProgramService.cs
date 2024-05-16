using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleWebApplication.Domain;
using SampleWebApplication.Model;
using SampleWebApplication.Shared;
using SampleWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos;


namespace SampleWebApplication.Services
{
    public class ProgramService : IProgramService
    {

        private readonly SampleWebApplicationContext _context;
        private readonly Container _container;

        public ProgramService(SampleWebApplicationContext context , Container container)
        {
            _context = context;
            _container = container;
        }
        public async Task<ProgramDTO> GetProgramById(string programId)
        {
            var query = _container.GetItemLinqQueryable<ProgramDTO>()
                              .Where(p => p.Id == programId)
                              .AsEnumerable() 
                              .FirstOrDefault();

           
            return await Task.FromResult(query);
        }

        public async Task<ActionResult<ProgramDTO>> CreateProgram(ProgramDTO programDTO)
        {
            ActionResult<ProgramDTO> actionResult = new ActionResult<ProgramDTO>(programDTO);

            var item = new { id = programDTO.Id, name = programDTO.Name, description = programDTO.Description };

            _context.ProgramDTO.Add(programDTO);

            try
            {
                var response = await _container.UpsertItemAsync(item, new PartitionKey(programDTO.Id));
            }
            catch (DbUpdateException)
            {
                throw;
            }

            Task<ActionResult<ProgramDTO>> taskResult = Task.FromResult(actionResult);

            return await taskResult;
        }
    }
}