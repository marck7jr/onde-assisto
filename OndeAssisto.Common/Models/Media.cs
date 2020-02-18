using OndeAssisto.Common.Models.Converters;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    [TypeConverter(typeof(EntityConverter<Media>))]

    public class Media : Entity
    {
        private bool isOutDated = false;
        private List<Platform> platforms;
        private Account account;
        private Work work;

        public Media()
        {
            Platforms = new List<Platform>();
            Account = new Account();
            Work = new Work();
        }

        public bool IsOutDated { get => isOutDated; set => Set(ref isOutDated, value); }
        [Required]
        public List<Platform> Platforms { get => platforms; set => Set(ref platforms, value); }
        [Required]
        public Account Account { get => account; set => Set(ref account, value); }
        [Required]
        public Work Work { get => work; set => Set(ref work, value); }
    }
}
