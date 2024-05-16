using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWebApplication.Domain;
using SampleWebApplication.Model;
using SampleWebApplication.Services;

namespace SampleWebApplication.Domain
{
    public interface IProgramService
    {
        Task<ProgramDTO> GetProgramById(string programId);

        Task<ActionResult<ProgramDTO>> CreateProgram(ProgramDTO programDTO);
    }
}