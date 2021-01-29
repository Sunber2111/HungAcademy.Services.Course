using Application.Categories;
using Application.Courses;
using Application.Lectures;
using Application.Sections;
using Application.UserWatchers;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryDto, Category>();

            CreateMap<Course, CourseDto>();

            CreateMap<CourseDto, Course>();

            CreateMap<Section, SectionDto>();

            CreateMap<SectionDto, Section>();

            CreateMap<Lecture, LectureDto>();

            CreateMap<LectureDto, Lecture>();

            CreateMap<UserWatcher, UserWatcherDto>();

            CreateMap<UserWatcherDto, UserWatcher>();

        }
    }
}
