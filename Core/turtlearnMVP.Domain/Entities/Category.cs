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
    [TableInfo("Kategori", 8)]
    /// <summary>
    /// Her ders dalı bir kategoriyi temsil eder. Örneğin Matematik kategorisi ya da Biyoloji kategorisi
    /// </summary>
    public class Category : EntityBase<int>, IEntity
    {


        /// <summary>
        /// 12. Sınıf, 8.Sınıf vb.
        /// </summary>
        public int? SinifDuzeyiId { get; set; } //enum

        /// <summary>
        /// İçerik (Üzerine düşünülecek gerekirse yeni bir tablo oluşturulacak)
        /// </summary>
        public string Content { get; set; } //üslü sayilar;limit;türev;integral;

        /// <summary>
        /// Kategori Adı. Örneğin: "Matematik"
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public string Picture { get; set; }

    }
}
