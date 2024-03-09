using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Domain.Enums;

/// <summary>
/// Ödev gönderiminde, gönderim durumunu gösteren enum.
/// </summary>
public enum HomeworkTransferStatus
{
    [EnumTitle("Teslim Edildi")]
    TeslimEdildi=1,
    [EnumTitle("Geç Teslim")]
    GecTeslim=2,
    [EnumTitle("Teslim Edilmedi")]
    TeslimEdilmedi=3,
}
