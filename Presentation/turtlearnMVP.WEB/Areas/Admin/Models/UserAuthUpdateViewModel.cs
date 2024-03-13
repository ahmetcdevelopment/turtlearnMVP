﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using turtlearnMVP.Domain.DTOs;

namespace turtlearnMVP.WEB.Areas.Admin.Models
{
    public class UserAuthUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        //[Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        //[MaxLength(50, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        //[MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string? UserName { get; set; }
        //[DisplayName("E-Posta Adresi")]
        //[Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        //[MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        //[MinLength(10, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        //[DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        //[DisplayName("Telefon Numarası")]
        //[Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        //[MaxLength(13, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        //[MinLength(13, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        //[DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        //[DisplayName("Resim Ekle")]
        //[DataType(DataType.Upload)]
        //public IFormFile PictureFile { get; set; }
        //[DisplayName("Resim")]
        public string? Photo { get; set; }

        public IList<ClaimsByModulesDTO>? Modules { get; set; }
        public string? SelectedIds { get; set; }

        public IList<RoleDTO>? Roles { get; set; }
        public string? SelectedRoleIds { get; set; }

        //public string? RegisterDate { get; set; }
        public string? UserType { get; set; }
    }
}
