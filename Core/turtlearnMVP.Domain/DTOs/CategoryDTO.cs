using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs
{
    public class CategoryDTO : IDto
    {
        public int? Id { get; set; }

        /// <summary>
        /// 12. Sınıf, 8.Sınıf vb.
        /// </summary>
        public int? SinifDuzeyiId { get; set; } //enum
        public string? SinifDuzeyiTitle { get; set; }//Sınıf Düzeyinin Title'ı

        /// <summary>
        /// İçerik (Üzerine düşünülecek gerekirse yeni bir tablo oluşturulacak)
        /// </summary>
        public string? Content { get; set; } //üslü sayilar;limit;türev;integral;

        /// <summary>
        /// Kategori Adı. Örneğin: "Matematik"
        /// </summary>
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Picture { get; set; }
    }
}
