using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.Services;
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
        }
    }
}
