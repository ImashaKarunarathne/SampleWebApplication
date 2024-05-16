using Microsoft.AspNetCore.Mvc;
using SampleWebApplication.Model;

namespace SampleWebApplication.Domain
{
    public interface ISubmittedAnswerFormService
    {
        Task<FormAnswersDTO> GetAswerFormById(string aswerFormById);

        Task<ActionResult<FormAnswersDTO>> CreateAswerForm(FormAnswersDTO formAnswersDTO);

        Task<ActionResult<FormAnswersDTO>> UpdateAswerForm(string id, FormAnswersDTO formAnswersDTO);
    }
}
