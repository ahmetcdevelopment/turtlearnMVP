using System.ComponentModel.DataAnnotations;

namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class HomeworkEditViewModel
    {
        public int? Id { get; set; }
        /// <summary>
        /// Ödevin ait olduğu oturum.
        /// </summary>
        public int SessionId { get; set; }//her ödev bir oturuma aittir.
                                          //bir oturumda birden fazla ödev olabilir.

        /// <summary>
        /// Ödev başlığı
        /// </summary>
        [StringLength(200)]
        public string Title { get; set; }
        public string UploadFile { get; set; }// Eğitmen bir ödev dosyası yüklediyse burada tutulur.
        public IFormFile UploadFormFile { get; set; }

        //ödevin dosyalarnı içeren bir property gerekli
        public DateTime StartDate { get; set; }//ödevin başlangıç zamanı
        public DateTime EndDate { get; set; }//ödevin bitiş zamanı

        [StringLength(350)]
        public string Description { get; set; }//ödev açıklaması.
    }
}
