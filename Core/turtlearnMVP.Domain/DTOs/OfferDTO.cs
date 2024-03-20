using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs;

public class OfferDTO : IDto
{
    public int Id { get; set; }
    /// <summary>
    /// Kampanyanın adı - Kadınlar günü, yeni kayıt, eğitmen kampanyası vesaire
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Kampanyanın yüzde kaç indirim vereceği.
    /// </summary>
    public int DiscountRate { get; set; }
    /// <summary>
    /// Kampanyanın tipi, tüm kurslar için, tüm 
    /// </summary>
    public int Type { get; set; }
    public string TypeTitle { get; set; }
    /// <summary>
    /// Kampanya kodu service'de Guid olarak arka plandan atılacak.
    /// </summary>
    public string Code { get; set; }
    public string Description { get; set; }
}
