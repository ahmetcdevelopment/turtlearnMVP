using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs;

public class OrderDetailDTO : IDto
{
    public int Id { get; set; }
    /// <summary>
    /// Hangi siparişe ait
    /// </summary>
    public int OrderId { get; set; }//sipariş numarası yerine geçebilir daha sonra düşünmek gerek.

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
    public string CurrencyTitle { get; set; }
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
