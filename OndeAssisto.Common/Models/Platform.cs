using OndeAssisto.Common.Models.Converters;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    [TypeConverter(typeof(EntityConverter<Platform>))]
    public class Platform : Entity
    {
        public Platform()
        {
            medias = new List<MediaPlatform>();
        }

        private string name;
        private string logo;
        private string site;
        private List<MediaPlatform> medias;

        [Required]
        public string Name { get => name; set => Set(ref name, value); }
        public string Logo { get => logo; set => Set(ref logo, value); }
        public string Site { get => site; set => Set(ref site, value); }
        public List<MediaPlatform> Medias { get => medias; set => Set(ref medias, value); }
    }
}
