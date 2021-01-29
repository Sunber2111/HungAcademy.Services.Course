using System;
using System.Threading.Tasks;
using Application.Errors;
using Application.Services.Interfaces;
using Application.UserWatchers;
using Domain;

namespace Application.Services.Implements
{
    public class UserWatcherServices : BaseServices<UserWatcherDto, UserWatcher>, IUserWatcherServices
    {
        public UserWatcherServices(IServiceProvider services) : base(services)
        {

        }


        public async Task<UserWatcherDto> GetUserWatcher(Guid courseId, Guid userId)
        {
            try
            {
                using (var unitOfWorks = NewDbContext())
                {
                    var repoCourse = unitOfWorks.Repository<Course>();

                    var repoUserWatcher = unitOfWorks.Repository<UserWatcher>();

                    var course = repoCourse.GetById(courseId);

                    if (course == null)
                        throw new NotFoundException("khóa học");

                    var sections = course.Sections;

                    var userWatcher = new UserWatcherDto();

                    foreach (var section in sections)
                    {
                        var secdto = new SectionCourseDto
                        {
                            Id = section.Id,
                            IsWatchSection = true
                        };
                        foreach (var lecture in section.Lectures)
                        {
                            var isWatch = await repoUserWatcher.GetByIdAsync(userId, lecture.Id);
                            if (isWatch != null)
                            {
                                secdto.LecturesWatcher.Add(new LectureCourseDto
                                {
                                    Id = lecture.Id,
                                    IsWatchLecture = true
                                });
                            }
                            else
                            {
                                secdto.LecturesWatcher.Add(new LectureCourseDto
                                {
                                    Id = lecture.Id,
                                    IsWatchLecture = false
                                });
                                secdto.IsWatchSection = false;
                            }
                        }
                        userWatcher.SectionsWatcher.Add(secdto);
                    }

                    return userWatcher;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}