using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class CommentDTO : IDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int CourseId { get; set; }//Yapılan yorum hangi kursa ait.
        public string CourseTitle { get; set; }//Kurs başlığı
        public int StudentId { get; set; }//Hangi öğrenci yorum yapıyor.
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        /// <summary>
        /// Yorum içeriği
        /// </summary>
        [StringLength(300)]
        public string Text { get; set; }//yapılan yorumun içeriği

        /// <summary>
        /// 1 den 5 e kadar yıldız ile puanlama
        /// </summary>
        public short Rating { get; set; }//1 - 2 - 3 - 4 - 5 puanlama
    }
}
