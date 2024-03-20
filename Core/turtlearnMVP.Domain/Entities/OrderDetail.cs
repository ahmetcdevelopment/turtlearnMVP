using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Utilities.Attributes;

namespace turtlearnMVP.Domain.Entities;
//Siparişin kalemleri (her course bir kalemdir.)
[TableInfo("Sipariş Detay", 26)]
public class OrderDetail : EntityBase<int>, IEntity
{
    /// <summary>
    /// Hangi siparişe ait
    /// </summary>
    public int OrderId { get; set; }
    public int CourseId { get; set; }
    /// <summary>
    /// Kalemdeki kursun ismi (Daha sonra değişme veya silme ihtimaline karşın burada da tutulur)
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Eğer bir kampanya kodu girdiyse ve geçerliyse indirim uygulanacak.
    /// </summary>
    public string OfferCode { get; set; }
    /// <summary>
    /// Tutar
    /// </summary>
    public decimal Amount { get; set; }
    /// <summary>
    /// Para birimi. -Enumda tutuluyor.
    /// </summary>
    public int Currency { get; set; }
    /// <summary>
    /// Uygulanacak vergi oranı
    /// </summary>
    public int TaxRate { get; set; }
    /// <summary>
    /// Vergilendirilen ücret
    /// </summary>
    public decimal TaxPrice { get; set; }
    /// <summary>
    /// İndirim uygulandıysa indirim miktarı
    /// </summary>
    public decimal DiscountRate { get; set; }
}
