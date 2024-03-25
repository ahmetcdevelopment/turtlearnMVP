using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using TurtLearn.Shared.Searching;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.SearchFilters;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Persistance.Configurations;
using turtlearnMVP.Persistance.Context;
using turtlearnMVP.Persistance.Repositories;
using turtlearnMVP.Persistance.SearchFilters;
using turtlearnMVP.Persistance.Services;
//using static TurtLearn.Shared.Searching.BaseExpressions<T>;

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
            services.AddScoped<IUserSettingService, UserSettingManager>();
            services.AddScoped<IClaimService, ClaimManager>();
            services.AddScoped<IZrfRoleService, ZrfRoleManager>();
            services.AddScoped<IOfferService, OfferManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOfferDetailService, OfferDetailManager>();
            services.AddScoped<IOrderDetailService, OrderDetailManager>();

            //services.AddScoped<ISearchService, SearchManager>();

            services.AddTransient<Expression<Func<CourseDTO, bool>>>(_ => _ => true);

            //services.AddScoped<ISearch<CourseDTO>, Search<CourseDTO>>();
            //services.AddScoped<ISearch<CourseDTO>, StringContains>();
            services.AddScoped<ISearch<CourseDTO>, AndSearch<CourseDTO>>();
            services.AddScoped<BaseExpressions<CourseDTO>.StringContains>();
            services.AddScoped<AndSearch<CourseDTO>>();
            //services.AddScoped(typeof(ISearch<CourseDTO>), typeof(AndSearch<CourseDTO>));


            //search denemesi
            services.AddScoped<ICourseFilter<CourseDTO>, CourseFilter>();
            services.AddScoped(typeof(ISearchService<CourseDTO>), typeof(SearchManager<CourseDTO>));


        }
    }
}
