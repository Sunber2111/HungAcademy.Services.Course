using Application.Courses;
using Application.Services.Interfaces;
using Domain;
using System;
using System.Threading.Tasks;

namespace Application.Services.Implements
{
    public class CourseServices : BaseServices<CourseDto, Course>, ICourseServices
    {
        public CourseServices(IServiceProvider services) : base(services)
        {

        }

        public async Task<bool> UpdateCategory(Guid id, Guid cateId)
        {
            try
            {
                using (var unitOfWorks = NewDbContext())
                {
                    var repo = unitOfWorks.Repository<Course>();
                    var item = await repo.FindAsync(id);
                    item.CategoryId = cateId;
                    var result = await unitOfWorks.SaveChangeAsync();
                    if (result)
                        return true;
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
