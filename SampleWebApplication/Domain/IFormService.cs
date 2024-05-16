using Microsoft.AspNetCore.Mvc;
using SampleWebApplication.Model;

namespace SampleWebApplication.Domain
{
    public interface IFormService
    {
        Task<FormDTO> GetFormById(string formId);

        Task<ActionResult<FormDTO>> CreateForm(FormDTO formDTO);

        Task<ActionResult<FormDTO>> UpdateForm(string id,  FormDTO formDTO);
    }
}
