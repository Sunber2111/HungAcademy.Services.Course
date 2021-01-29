using Application.Courses;
using Application.Mapper;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using FluentValidation.AspNetCore;
using HungAcademy.Services.Courses.Middlewares;
using Application.Services.Implements;
using Application.Services.Interfaces;
using Infrastructure.Photos;
using Infrastructure.Videos;
using Microsoft.AspNetCore.Http.Features;

namespace HungAcademy.Services.Courses
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(Configuration.GetConnectionString("CourseDataBase"));
            });

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 10; //default 1024
                options.ValueLengthLimit = int.MaxValue; //not recommended value
                options.MultipartBodyLengthLimit = long.MaxValue; //not recommended value
            });

            services.AddMediatR(typeof(List.Handler).Assembly);

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddMvc().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Create>());

            services.AddTransient<ICourseServices, CourseServices>();

            services.AddTransient<ICategoryServices, CategoryServices>();

            services.AddTransient<IPhotoAccessor, PhotoAccessor>();

            services.AddTransient<ISectionServices, SectionServices>();

            services.AddTransient<ILectureServices, LectureServices>();

            services.AddTransient<ICaseStudyServices,CaseStudyServices>();

            services.AddTransient<IUserWatcherServices,UserWatcherServices>();

            services.Configure<CloudinarySettings>(Configuration.GetSection("Cloudinary"));

            services.AddTransient<IVideoAccessor, VideoAccessor>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
