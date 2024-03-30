using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Searching;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface ISearchService<T>
    {
        /// <summary>
        /// İki filtreleme ifadesini `AND` operatörü ile bağlar.
        /// </summary>
        /// <param name="left">Ve'den Önce</param>
        /// <param name="right">Ve'den Sonra</param>
        /// <returns>önce VE sonra</returns>
        ISearch<T> AndConcat(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right);

        /// <summary>
        /// Gönderilen alanda belirtilen metin içerenleri filtreler.
        /// </summary>
        /// <param name="propertyName">Aranacak property adı</param>
        /// <param name="value">Aranacak metin</param>
        /// <returns>value içeren propertyler</returns>
        Expression<Func<T, bool>> StringContains(string propertyName, string value);

        /// <summary>
        /// Gönderilen alanda belirli bir değere eşit olanları filtreler.
        /// </summary>
        /// <param name="propertyName">Aranacak property adı</param>
        /// <param name="value">Eşitlik Kontrolü yapılacak değer</param>
        /// <returns>property = value</returns>
        Expression<Func<T, bool>> Equal(string propertyName, string value);

        /// <summary>
        /// Gönderilen alanda belirli bir değerden büyük olanları filtreler.
        /// </summary>
        /// <param name="propertyName">Aranacak property adı</param>
        /// <param name="value">Büyüklük kontrolü yapılacak değer</param>
        /// <returns>property BÜYÜKTÜR value</returns>
        Expression<Func<T, bool>> GreaterThan(string propertyName, string value);

        /// <summary>
        /// Gönderilen alanda belirli bir değerden küçük olanları filtreler.
        /// </summary>
        /// <param name="propertyName">Aranacak property adı</param>
        /// <param name="value">Küçüklük kontrolü yapılacak değer</param>
        /// <returns>property KÜÇÜKTÜR value</returns>
        Expression<Func<T, bool>> LessThan(string propertyName, string value);

        /// <summary>
        /// Göndrerilen bir tarih alanında belirli bir tarih aralığında olanları filtreler.
        /// </summary>
        /// <param name="propertyName">Aranacak Tarih Alanı Adı</param>
        /// <param name="baslangicTarihi">Tarih Başlangıç</param>
        /// <param name="bitisTarihi">Tarih Bitiş</param>
        /// <returns>Başlangıç KÜÇÜKTÜR Alan KÜÇÜKTÜR Bitiş </returns>
        Expression<Func<T, bool>> DateRange(string propertyName, DateTime baslangicTarihi, DateTime bitisTarihi);

        /// <summary>
        /// Gönderilen alanda true olanları filtreler
        /// </summary>
        /// <param name="propertyName">Aranacak property adı</param>
        /// <returns>property == true</returns>
        Expression<Func<T, bool>> IsTrue(string propertyName);

        /// <summary>
        /// Gönderilen alanda false olanları filtreler
        /// </summary>
        /// <param name="propertyName">Aranacak property adı</param>
        /// <returns>property == false</returns>
        Expression<Func<T, bool>> IsFalse(string propertyName);

    }
}
