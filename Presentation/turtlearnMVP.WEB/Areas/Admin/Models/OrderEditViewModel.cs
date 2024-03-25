using Microsoft.AspNetCore.Mvc.Rendering;

namespace turtlearnMVP.WEB.Areas.Admin.Models;

public class OrderEditViewModel
{
    public SelectList SelStatus { get; set; }
    public int? Id { get; set; }
    /// <summary>
    /// Siparişin durumu : onaylandı, reddedildi, iptal edildi vesaire.
    /// </summary>
    public int Status { get; set; }
    /// <summary>
    /// User'ın sonradan ismini değiştirme ihtimaline karşın ismi de tutulur.
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// User'ın sonradan ismini değiştirme ihtimaline karşın soyismi de tutulur.
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Siparişin verildiği telefon numarası
    /// </summary>
    public string PhoneNumber { get; set; }
    /// <summary>
    /// Siparişi veren kullanıcının mail adresi.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Sipariş verilirken mail doğrulaması ile verilecek.
    /// </summary>
    public string EmailConfirmCode { get; set; }
    /// <summary>
    /// Sipariş için ödenen tutar nedir?
    /// </summary>
    public decimal AmountPaid { get; set; }
    /// <summary>
    /// Sipariş ne zaman gerçekleştirildi?
    /// </summary>
    public DateTime Date { get; set; }
}
