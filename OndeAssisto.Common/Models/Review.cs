using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    public class Review : Entity
    {
        private Account account;
        private Media media;

        public Account Account { get => account; set => Set(ref account, value); }

        [Required]
        public Media Media { get => media; set => Set(ref media, value); }
    }
}
