using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Concrete;
using turtlearnMVP.Domain.Entities;

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
        public turtlearnMVPContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
