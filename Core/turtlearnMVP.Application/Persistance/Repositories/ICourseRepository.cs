﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Abstract
{
    public interface ICourseRepository : IRepository<Course>
    {
        IQueryable<CourseDTO> GetAllQueryableRecords();
        Task<CourseDetailApiDTO> GetCourseDetailsAsync(int courseId);
    }
}
