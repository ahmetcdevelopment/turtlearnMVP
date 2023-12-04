using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.Abstract;
using turtlearnMVP.Application.Persistance.Repositories;
using turtlearnMVP.Persistance.Context;
using turtlearnMVP.Persistance.Repositories.EntityFramework;

namespace turtlearnMVP.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly turtlearnMVPContext _Context;

        public UnitOfWork(turtlearnMVPContext context)
        {
            _Context = context;
        }
        private EfCategoryRepository _EfCategoryRepository;
        private EfCourseRepository _EfCourseRepository;
        private EfCommentRepository _EfCommentRepository;
        private EfCourseEnrollmentRepository _EfCourseEnrollmentRepository;
        private EfCourseStudentRepository _EfCourseStudentRepository;
        private EfHomeworkRepository _EfHomeworkRepository;
        private EfHomeworkTransferRepository _EfHomeworkTransferRepository;
        private EfSessionRepository _EfSessionRepository;
        private EfSessionRollCallRepository _EfSessionRollCallRepository;
        public ICategoyRepository Categories => _EfCategoryRepository ?? new EfCategoryRepository(_Context);

        public ICourseRepository Courses => _EfCourseRepository ?? new EfCourseRepository(_Context);

        public ICommentRepository Comments => _EfCommentRepository ?? new EfCommentRepository(_Context);

        public ICourseEnrollmentRepository CoursesEnrollments => _EfCourseEnrollmentRepository ?? new EfCourseEnrollmentRepository(_Context);

        public ICourseStudentRepository CoursesStudents => _EfCourseStudentRepository ?? new EfCourseStudentRepository(_Context);

        public IHomeworkRepository Homeworks => _EfHomeworkRepository ?? new EfHomeworkRepository(_Context);

        public IHomeworkTransferRepository HomeworkTransfers => _EfHomeworkTransferRepository ?? new EfHomeworkTransferRepository(_Context);

        public ISessionRepository Sessions => _EfSessionRepository ?? new EfSessionRepository(_Context);

        public ISessionRollCallRepository SessionRollCalls => _EfSessionRollCallRepository ?? new EfSessionRollCallRepository(_Context);

        public async ValueTask DisposeAsync()
        {
            await _Context.DisposeAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _Context.SaveChangesAsync();
        }
    }
}
