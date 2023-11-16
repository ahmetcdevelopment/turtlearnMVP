using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtLearn.Shared.Utilities.Messages
{
    public static class Messages
    {
        public static string SomethingWrong => $"Bir şeyler yanlış gitti.";
        public static string PageIsNotFound => $"Aradığınız sayfa bulunamadı.";
        public static string ResultIsNotFound => $"Aradığınız sonuç bulunamadı.";
        public static string WrongPasswordOrEmail => $"E-Posta adresiniz veya şifreniz yanlıştır.";
        public static string SuccessAdd(string entityName)
        {
            return $"{entityName} başarıyla eklendi";
        }
        public static string FailedAdd(string entityName)
        {
            return $"{entityName} eklenirken bir hata oluştu.";
        }
        public static string SuccessUpdate(string entityName)
        {
            return $"{entityName} başarıyla güncellendi.";
        }
        public static string FailedUpdate(string entityName)
        {
            return $"{entityName} güncellenirken bir hata oluştu.";
        }
        public static string SuccessDelete(string entityName)
        {
            return $"{entityName} başarıyla silindi.";
        }
        public static string FailedDelete(string entityName)
        {
            return $"{entityName} silinirken bir hata oluştu.";
        }
    }
}
