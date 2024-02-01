using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableInfo("Ödev Gönderimi", 16)]
    /// <summary>
    /// Ödevlere öğrenci görevlendirme
    /// </summary>
    public class HomeworkTransfer : EntityBase<int>, IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Verilen ödev
        /// </summary>
        public int HomeworkId { get; set; }//verilen ödev

        /// <summary>
        /// Ödevin muhatabı öğrenci
        /// </summary>
        public int StudentId { get; set; }//ödevi yapması gereken öğrenci

        /// <summary>
        /// Teslim durumu (enum)
        /// </summary>
        public int Status { get; set; }//teslim edildi - geç teslim edildi - teslim edilmedi
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Ödev dosyası
        /// </summary>
        public string Attachment { get; set; }//veritabanında IFromFile tutulmaz, string tutulur.

        /// <summary>
        /// Veriliş tarihi
        /// </summary>
        public DateTime TransferDate { get; set; }

    }
}
