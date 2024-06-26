﻿using System;
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
        IUserSettingRepository UserSettings { get; }
        IUserResumeRepository UserResumes { get; }
        IClaimRepository Claims { get; }
        IRoleRepository Roles { get; }
        #region SIGNALR
        IChatRepository Chats { get; }
        IChatUserRepository ChatUsers { get; }
        IMessageRepository Messages { get; }
        #endregion

        #region OFFER AND ORDER OPERATIONS 
        IOfferRepository Offers { get; }
        IOfferDetailRepository OffersDetail { get; }
        IOrderRepository Orders { get; }
        IOrderDetailRepository OrderDetails { get; }
        #endregion
        Task<int> SaveChanges();
    }
}
