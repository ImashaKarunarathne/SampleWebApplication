using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleWebApplication.Model;

namespace SampleWebApplication.Data
{
    public class SampleWebApplicationContext : DbContext
    {
        public SampleWebApplicationContext (DbContextOptions<SampleWebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<SampleWebApplication.Model.EmployerDTO> EmployerDTO { get; set; } = default!;

        public DbSet<SampleWebApplication.Model.ProgramDTO> ProgramDTO { get; set; } = default!;

        public DbSet<SampleWebApplication.Model.FormDTO> FormDTO { get; set; } = default!;

        public DbSet<SampleWebApplication.Model.CandidateDTO> CandidateDTO { get; set; } = default!;
        public DbSet<SampleWebApplication.Model.FormAnswersDTO> FormAnswersDTO { get; set; } = default!;
    }
}
