using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json;

namespace OndeAssisto.Common.Models.Converters
{
    public class EntityConverter<T> : TypeConverter where T : Entity
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string)
                ? true
                : base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value is string parsed
                ? JsonSerializer.Deserialize<T>(parsed, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                : base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value is T parsed
                ? JsonSerializer.Serialize(parsed)
                : base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
