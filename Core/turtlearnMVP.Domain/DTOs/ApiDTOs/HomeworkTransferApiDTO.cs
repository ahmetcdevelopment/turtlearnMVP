using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace turtlearnMVP.Domain.DTOs.ApiDTOs;

public class HomeworkTransferApiDTO : IDto
{
    public int Id { get; set; }
    public int HomeworkId { get; set; }

    public int StudentId { get; set; }//ödevi yapması gereken öğrenci
    public string Description { get; set; }

    /// <summary>
    /// Ödev dosyası
    /// </summary>
    public string Attachment { get; set; }//veritabanında IFromFile tutulmaz, string tutulur.

    /// <summary>
    /// Veriliş tarihi
    /// </summary>
}
