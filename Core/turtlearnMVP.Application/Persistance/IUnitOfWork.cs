using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Application.Persistance.Abstract;
using turtlearnMVP.Application.Persistance.Repositories;

namespace turtlearnMVP.Application.Persistance
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICategoyRepository Categories { get; }
        ICourseRepository Courses { get; }
        ICommentRepository Comments { get; }
        ICourseEnrollmentRepository CoursesEnrollments { get; }
        ICourseStudentRepository CoursesStudents { get; }
        IHomeworkRepository Homeworks { get; }
        IHomeworkTransferRepository HomeworkTransfers { get; }
        ISessionRepository Sessions { get; }
        ISessionRollCallRepository SessionRollCalls { get; }
        Task<int> SaveChanges();
    }
}
