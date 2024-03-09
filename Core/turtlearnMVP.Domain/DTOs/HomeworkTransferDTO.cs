using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class HomeworkTransferDTO : IDto
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public string HomeworkInfo { get; set; } // Başlık veya ödev veriliş tarihi olabilir.

        /// <summary>
        /// Ödevin muhatabı öğrenci
        /// </summary>
        public int StudentId { get; set; }//ödevi yapması gereken öğrenci
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        /// <summary>
        /// Teslim durumu (enum)
        /// </summary>
        public int Status { get; set; }//teslim edildi - geç teslim edildi - teslim edilmedi
        public string StatusTitle { get; set; }
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
        public string TransferDateStr => TransferDate.ToString("dd/MM/yyyy");
    }
}
