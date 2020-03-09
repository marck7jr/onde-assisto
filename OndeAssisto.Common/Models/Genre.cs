using OndeAssisto.Common.Models.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    [TypeConverter(typeof(EntityConverter<Genre>))]
    public class Genre : Entity
    {
        private string name;

        [Required]
        public string Name { get => name; set => Set(ref name, value); }
    }
}
