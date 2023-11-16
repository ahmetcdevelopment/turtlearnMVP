using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtLearn.Shared.Utilities.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Enum'un Title'ını getirir.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetEnumTitle<TEnum>(int Value) where TEnum : Enum
        {
            if (Enum.IsDefined(typeof(TEnum), Value))
            {
                var enumMember = (TEnum)Enum.ToObject(typeof(TEnum), Value);
                var fieldInfo = enumMember.GetType().GetField(enumMember.ToString());
                var attribute = (EnumTitleAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(EnumTitleAttribute));
                return attribute != null ? attribute.Title : enumMember.ToString();
            }

            return "Geçersiz Değer";
        }
    }
}
