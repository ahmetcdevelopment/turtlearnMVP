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
    [TableInfo("Ödev", 15)]
    public class Homework : EntityBase<int>, IEntity
    {

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

        //ödevin dosyalarnı içeren bir property gerekli
        public DateTime StartDate { get; set; }//ödevin başlangıç zamanı
        public DateTime EndDate { get; set; }//ödevin bitiş zamanı

        [StringLength(350)]
        public string Description { get; set; }//ödev açıklaması.
    }
}
