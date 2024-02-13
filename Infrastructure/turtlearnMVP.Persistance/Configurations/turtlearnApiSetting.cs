using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Extensions;
namespace turtlearnMVP.Persistance.Configurations;

public static class turtlearnApiSetting
{
    private static string _Key { get; set; }
    /// <summary>
    /// Programın anahtarını döndürür.
    /// </summary>
    /// <returns></returns>
    public static async Task<string> getKey()
    => _Key.Trim();

    /// <summary>
    /// Apiden gelen anahtar programın anahtarı ile eşleşiyor mu?
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static async Task<bool> isKeyValid(string key)
    => _Key.Trim().ToLower().Equals(key.Trim().ToLower(), StringComparison.OrdinalIgnoreCase);
    public static async Task generateKey()
    {
        _Key = _Key.GenerateRandomString(20);
    }
}
