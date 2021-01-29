using System;
using System.Threading.Tasks;
using Application.Sections;
using Application.Services.Interfaces;
using Domain;

namespace Application.Services.Implements
{
    public class SectionServices : BaseServices<SectionDto, Section>, ISectionServices
    {
        public SectionServices(IServiceProvider services) : base(services)
        {

        }

        public async Task<Guid> InsertSection(string name, Guid courseId)
        {
            using (var uow = NewDbContext())
            {
                var courseRepo = uow.Repository<Course>();

                var check = courseRepo.Exists(courseId);

                if (!check)
                    throw new Exception("Khóa học không tồn tại - không thể thêm phần học");
                
                var sec = new Section{
                    Name = name,
                    CourseId = courseId
                };

                var result = await InsertAsync(sec);

                return result.Id;

            }
        }
    }
}