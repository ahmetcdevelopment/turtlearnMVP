using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class CategoryEditViewModel
    {
        public int? Id { get; set; }

        public int? SinifDuzeyiId { get; set; } //enum

        public SelectList? SelSinifDuzeyi { get; set; }

        [Required(ErrorMessage = "Content alanı boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Content alanı en fazla {1} karakter olmalıdır.")]
        public string Content { get; set; } //üslü sayilar;limit;türev;integral;

        [Required(ErrorMessage = "Name alanı boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Name alanı en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description alanı boş bırakılamaz.")]
        [StringLength(250, ErrorMessage = "Description alanı en fazla {1} karakter olmalıdır.")]
        public string Description { get; set; }

        [RegularExpression(@"\.(jpg|jpeg|png|gif)$", ErrorMessage = "Geçersiz resim dosya formatı.")]
        public string Picture { get; set; } = Path.Combine("~/images/avatar/", "default.jpg");
    }
}
