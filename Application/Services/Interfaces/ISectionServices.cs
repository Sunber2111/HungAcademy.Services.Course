using System;
using System.Threading.Tasks;
using Application.Sections;
using Domain;

namespace Application.Services.Interfaces
{
    public interface ISectionServices : IBaseServices<SectionDto, Section>
    {
        Task<Guid> InsertSection(string name, Guid courseId);
    }
}