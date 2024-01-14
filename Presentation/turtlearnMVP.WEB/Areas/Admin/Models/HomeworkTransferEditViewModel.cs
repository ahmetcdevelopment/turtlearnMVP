using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class HomeworkTransferEditViewModel
    {
        public SelectList SelStatus { get; set; }
        public int? Id { get; set; }

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
        public IFormFile AttachmentForm { get; set; }

        /// <summary>
        /// Veriliş tarihi
        /// </summary>
        public DateTime TransferDate { get; set; }
    }
}
