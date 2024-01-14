using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities
{
    [TableTitle("Kullanıcı Ayarları")]
    /// <summary>
    /// Her ders dalı bir kategoriyi temsil eder. Örneğin Matematik kategorisi ya da Biyoloji kategorisi
    /// </summary>
    public class UserSetting : EntityBase<int>, IEntity
    {
        /// <summary>
        /// Ayar Türü. Enum. Örneğin(Doğrulama, Bildirim ya da olabilecek diğer hesap ayarları)
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Ayar Türü. Enum. Örneğin(Doğrulama, Bildirim ya da olabilecek diğer hesap ayarları)
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Ayar Metni. Gerekirse Eklenecek
        /// </summary>
        //public string Text { get; set; }

        /// <summary>
        /// Ayar Türünün Türü. Enum. Örneğin(Doğrulama için sms, mail, facebook; Bildirim için kapalı, açık, tüm, sadece derslerim, kampanyalar...)
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// Ayar Türünün Türünün değeri. Örneğin(Doğrulama için true, false, Başka türler için parse edilip kullanılacak)
        /// </summary>
        public string Value { get; set; }
    }
}
