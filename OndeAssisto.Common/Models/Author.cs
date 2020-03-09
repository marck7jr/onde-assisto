using OndeAssisto.Common.Models.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    [TypeConverter(typeof(EntityConverter<Author>))]

    public class Author : Entity
    {
        private string name;

        [Required]
        public string Name { get => name; set => Set(ref name, value); }

        public override string ToString() => Name;
    }
}
