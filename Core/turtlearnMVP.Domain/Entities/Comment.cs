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
    [TableTitle("Yorum")]
    /// <summary>
    /// Öğrencilerin öğretmenleri değerlendirme sistemidir. E ticaret satıcı yorumları ya da bionluk freelancer yorumları gibi
    /// </summary>
    public class Comment : EntityBase<int>, IEntity
    {

        public int ParentId { get; set; }
        public int TableId { get; set; } // Yorum bir kursa veya session'a yapılmış olabilir.
        //hangisine yapıldı bilgisini enumdan alırız.
        public int RecordId { get; set; }//Yapılan yorum hangi kayıta ait.
        public int StudentId { get; set; }//Hangi öğrenci yorum yapıyor.

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
