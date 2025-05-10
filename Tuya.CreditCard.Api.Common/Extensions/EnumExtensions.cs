using System.ComponentModel.DataAnnotations;

namespace Tuya.CreditCard.Api.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null) return string.Empty;
            var attribute = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
            return attribute?.Name ?? value.ToString();
        }
    }
}
