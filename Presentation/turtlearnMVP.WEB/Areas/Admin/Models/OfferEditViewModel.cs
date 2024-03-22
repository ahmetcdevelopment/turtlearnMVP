using Microsoft.AspNetCore.Mvc.Rendering;

namespace turtlearnMVP.WEB.Areas.Admin.Models;

public class OfferEditViewModel
{
    public SelectList SelTypes { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// Kampanyanın yüzde kaç indirim vereceği.
    /// </summary>
    public int DiscountRate { get; set; }
    /// <summary>
    /// Kampanyanın tipi, tüm kurslar için, tüm 
    /// </summary>
    public int Type { get; set; }
    /// <summary>
    /// Kampanya kodu service'de Guid olarak arka plandan atılacak.
    /// </summary>
    public string Code { get; set; }
    public string Description { get; set; }
}
