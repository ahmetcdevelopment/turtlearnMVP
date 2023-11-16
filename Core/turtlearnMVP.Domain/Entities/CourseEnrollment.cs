using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableTitle("Kurs Başvuru")]
    ///<summary>
    /// Kursa kayıt olmak isteyen öğrencilerin istekleri bu tabloya düşer
    /// approved alanı öğretmen tarafıdan yönetilir.
    /// örnek :
    /// 1- Ahmet Çiftçi {approved=true;} - derse kabul edildi.
    /// 2 - Fehmi Yüce {approved=false;} - derse reddedildi.
    /// </summary>
    public class CourseEnrollment : EntityBase<int>, IEntity
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; } // userdan alınacak.

        /// <summary>
        /// Kursa kabul edilme durumu
        /// (Enum ya da bool hakkında düşünmek gerek)
        /// </summary>
        public bool Approved { get; set; } // öğretmen onayı.
    }
}
