using System.ComponentModel;

namespace Noticias.Model.Helpers
{
    public static class EnumExtencion
    {
        public static string GetDescription(this Enum enumValue)
        {
            var fi = enumValue?.GetType()?.GetField(enumValue.ToString());
            if (fi == null) return null;
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : enumValue.ToString();
        }
    }
}
