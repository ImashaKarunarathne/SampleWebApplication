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
    public class SubmittedAnswerFormService : ISubmittedAnswerFormService
    {
        private readonly Container _container;

        public SubmittedAnswerFormService(Container container)
        {
            _container = container;
        }

        public async Task<FormAnswersDTO> GetAswerFormById(string answerFormId)
        {
            var query = _container.GetItemLinqQueryable<FormAnswersDTO>()
                              .Where(f => f.Id == answerFormId)
                              .AsEnumerable()
                              .FirstOrDefault();


            return await Task.FromResult(query);
        }

        public async Task<ActionResult<FormAnswersDTO>> CreateAswerForm(FormAnswersDTO formAnswersDTO)
        {
            ActionResult<FormAnswersDTO> actionResult = new ActionResult<FormAnswersDTO>(formAnswersDTO);

            var item = new { id = formAnswersDTO.Id, formId = formAnswersDTO.FormId, candidateId = formAnswersDTO.CandidateId , questionAnswers= formAnswersDTO.QuestionAnswers };

            try
            {
                var response = await _container.UpsertItemAsync(item, new PartitionKey(formAnswersDTO.Id));
            }
            catch (DbUpdateException)
            {
                throw;
            }

            Task<ActionResult<FormAnswersDTO>> taskResult = Task.FromResult(actionResult);

            return await taskResult;
        }

        public async Task<ActionResult<FormAnswersDTO>> UpdateAswerForm(string id, FormAnswersDTO formAnswersDTO)
        {
            ActionResult<FormAnswersDTO> actionResult = new ActionResult<FormAnswersDTO>(formAnswersDTO);
            var existingItemResponse = await _container.ReadItemAsync<dynamic>(id, new PartitionKey(id));

            var existingItem = existingItemResponse.Resource;
            existingItem.id = formAnswersDTO.Id;
            existingItem.candidateId = formAnswersDTO.CandidateId;
            existingItem.questionAnswers = formAnswersDTO.QuestionAnswers;

            var response = await _container.UpsertItemAsync(existingItem, new PartitionKey(formAnswersDTO.Id));

            return actionResult;
        }
    }
}
