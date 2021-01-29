using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CaseStudies;
using Application.Errors;
using Application.Services.Interfaces;
using Domain;

namespace Application.Services.Implements
{
    public class CaseStudyServices : BaseServices<CaseStudyDto, CaseStudy>, ICaseStudyServices
    {
        public CaseStudyServices(IServiceProvider services) : base(services)
        {

        }

        public async Task<List<CaseStudyDto>> GetByCourseId(Guid courseId)
        {
            try
            {
                using (var unitOfWork = NewDbContext())
                {
                    var repo = unitOfWork.Repository<Course>();

                    var course = await repo.GetByIdAsync(courseId);

                    if (course == null)
                        throw new NotFoundException();

                    return _mapper.Map<ICollection<CaseStudy>, List<CaseStudyDto>>(course.CaseStudies);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}