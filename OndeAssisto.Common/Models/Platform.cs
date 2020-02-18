using OndeAssisto.Common.Models.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    [TypeConverter(typeof(EntityConverter<Platform>))]
    public class Platform : Entity
    {
        private string name;
        private string logo;
        private string site;

        //[]atributos de validacao
        [Required]
        public string Name
        {
            get => name;

            set => Set(ref name, value);
        }
        public string Logo
        {
            get => logo;

            set => Set(ref logo, value);
        }
        public string Site
        {
            get => site;

            set => Set(ref site, value);
        }
    }
}
