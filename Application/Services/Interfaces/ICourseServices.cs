using Application.Courses;
using Domain;
using System;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICourseServices:IBaseServices<CourseDto,Course>
    {
        Task<bool> UpdateCategory(Guid id, Guid cateId);
    }
}
