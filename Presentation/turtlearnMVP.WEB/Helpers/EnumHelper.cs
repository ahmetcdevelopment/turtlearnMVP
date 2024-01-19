using Microsoft.AspNetCore.Mvc.Rendering;
using STM.Core.Utilities.Attributes;
using System.Reflection;
using turtlearnMVP.Domain.Enums;

namespace turtlearnMVP.WEB.Helpers
{
    public static class EnumHelper
    {
        public static SelectList GetEnumSelectList<TEnum>() where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            var items = new List<SelectListItem>();

            foreach (var value in values)
            {
                var field = typeof(TEnum).GetField(value.ToString());
                var displayAttribute = field.GetCustomAttribute<EnumTitleAttribute>();
                var title = displayAttribute != null ? displayAttribute.Title : value.ToString();

                items.Add(new SelectListItem
                {
                    Value = Convert.ToInt32(value).ToString(),
                    Text = title
                });
            }

            return new SelectList(items, "Value", "Text");
        }

        public static Type GetEnumByTypeId(int typeId)
        {
            switch (typeId)
            {
                case 1:
                    return typeof(UserSettingVerify);
                case 2:
                // return typeof(Notification);
                case 3:
                // return typeof(EnumForCase3);
                default:
                    throw new ArgumentException($"Belirtilen TypeId ({typeId}) için bir enum değeri tanımlanmamış.");
            }
        }
    }
}
