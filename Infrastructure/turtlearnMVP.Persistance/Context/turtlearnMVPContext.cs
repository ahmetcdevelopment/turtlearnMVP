using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Mappings;
using TurtLearn.Shared.Entities.Concrete;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Mappings;

namespace turtlearnMVP.Persistance.Context
{
    public class turtlearnMVPContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<HomeworkTransfer> HomeworkTransfers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionRollCall> SessionRollCalls { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public turtlearnMVPContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleClaimMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new UserClaimMap());
            builder.ApplyConfiguration(new UserLoginMap());
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new UserRoleMap());
            builder.ApplyConfiguration(new UserTokenMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new CommentMap());
            builder.ApplyConfiguration(new CourseEnrollmentMap());
            builder.ApplyConfiguration(new CourseMap());
            builder.ApplyConfiguration(new CourseStudentMap());
            builder.ApplyConfiguration(new HomeworkMap());
            builder.ApplyConfiguration(new HomeworkTransferMap());
            builder.ApplyConfiguration(new SessionMap());
            builder.ApplyConfiguration(new SessionRollCallMap());
            builder.ApplyConfiguration(new UserSettingMap());
        }
    }
}
