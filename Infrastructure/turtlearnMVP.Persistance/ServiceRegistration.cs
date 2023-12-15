using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TurtLearn.Shared.Searching;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Persistance.Repositories;
using turtlearnMVP.Persistance.Services;

namespace turtlearnMVP.Persistance
{
    public static class ServiceRegistration
    {
        public static void LoadMyPersistanceServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICourseService, CourseManager>();
            services.AddScoped<ICourseEnrollmentService, CourseEnrollmentManager>();
            services.AddScoped<ICourseStudentService, CourseStudentManager>();
            services.AddScoped<IHomeworkService, HomeworkManager>();
            services.AddScoped<IHomeworkTransferService, HomeworkTransferManager>();
            services.AddScoped<ISessionService, SessionManager>();
            services.AddScoped<ISessionRollCallService, SessionRollCallManager>();

            services.AddScoped<ISearch<CategoryDTO>, Search<CategoryDTO>>();
        }
    }
}
