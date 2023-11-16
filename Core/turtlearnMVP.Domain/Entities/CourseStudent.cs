using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableTitle("Kurs Öğrencileri")]
    /// <summary>
    /// hangi öğrenci hangi kurslara dahil onun belirlenmesi için tutuldu.
    /// bir öğrenci birden fazla kursa dahil olabilir.
    /// bir kursa dahil olan birden fazla öğrenci olabilir.
    /// çoka çok ilişki için üçüncü bir tablo şart.
    /// </summary>
    public class CourseStudent : EntityBase<int>, IEntity
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }//user tablosundan alınacak.
    }
}
