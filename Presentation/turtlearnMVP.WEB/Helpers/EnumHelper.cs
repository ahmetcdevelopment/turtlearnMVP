using Microsoft.AspNetCore.Mvc.Rendering;
using STM.Core.Utilities.Attributes;
using System.Reflection;

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
    }
}
