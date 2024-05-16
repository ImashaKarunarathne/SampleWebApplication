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
    public class FormService : IFormService
    {
        private readonly Container _container;

        public FormService(Container container)
        {
            _container = container;
        }

        public async Task<FormDTO> GetFormById(string formId)
        {
            var query = _container.GetItemLinqQueryable<FormDTO>()
                              .Where(f => f.Id == formId)
                              .AsEnumerable()
                              .FirstOrDefault();


            return await Task.FromResult(query);
        }

        public async Task<ActionResult<FormDTO>> CreateForm(FormDTO formDTO)
        {
            ActionResult<FormDTO> actionResult = new ActionResult<FormDTO>(formDTO);

            var item = new { id = formDTO.Id, customQuestions = formDTO.CustomQuestions , fixedQuestions = formDTO.FixedQuestions };

            try
            {
                var response = await _container.UpsertItemAsync(item, new PartitionKey(formDTO.Id));
            }
            catch (DbUpdateException)
            {
                throw;
            }

            Task<ActionResult<FormDTO>> taskResult = Task.FromResult(actionResult);

            return await taskResult;
        }

        public async Task<ActionResult<FormDTO>> UpdateForm(string formId, FormDTO formDTO)
        {
            ActionResult<FormDTO> actionResult = new ActionResult<FormDTO>(formDTO); 
            var existingItemResponse = await _container.ReadItemAsync<dynamic>(formId, new PartitionKey(formId));

            var existingItem = existingItemResponse.Resource;
            existingItem.id = formDTO.Id;
            existingItem.customQuestions = formDTO.CustomQuestions;
            existingItem.fixedQuestions = formDTO.FixedQuestions;

            var response = await _container.UpsertItemAsync(existingItem, new PartitionKey(formDTO.Id));

            return actionResult;
        }

    }
}
