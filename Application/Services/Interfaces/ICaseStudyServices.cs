using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CaseStudies;
using Domain;

namespace Application.Services.Interfaces
{
    public interface ICaseStudyServices : IBaseServices<CaseStudyDto, CaseStudy>
    {
        Task<List<CaseStudyDto>> GetByCourseId(Guid courseId);
    }
}