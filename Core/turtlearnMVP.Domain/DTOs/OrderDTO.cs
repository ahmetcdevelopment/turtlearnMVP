using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs;

public class OrderDTO : IDto
{
    public int Id { get; set; }
    /// <summary>
    /// Siparişi oluşturan kullanıcı.
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Siparişin durumu : onaylandı, reddedildi, iptal edildi vesaire.
    /// </summary>
    public int Status { get; set; }
    public string StatusTitle { get; set; }
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
    public string DateStr => Date.ToString("dd/MM/yyyy");
}
