using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TurtLearn.Shared.Searching;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Persistance.Configurations;
using turtlearnMVP.Persistance.Context;
using turtlearnMVP.Persistance.Repositories;
using turtlearnMVP.Persistance.Services;

namespace turtlearnMVP.Persistance
{
    public static class ServiceRegistration
    {
        public static void LoadMyPersistanceServices(this IServiceCollection services, IConfiguration configuration)
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
            services.AddScoped<IMailService, MailManager>();

            services.AddScoped<ISearch<CategoryDTO>, Search<CategoryDTO>>();

        }
    }
}
