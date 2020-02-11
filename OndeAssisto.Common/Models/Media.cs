using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    public class Media : Entity
    {
        private string name;
        private string email;
        private bool isOutDated = false;
        private List<Platform> platforms;
        private Account account;
        private Work work;

        [Required]
        public string Name { get => name; set => Set(ref name, value); }
        public string Email { get => email; set => Set(ref email, value); }
        public bool IsOutDated { get => isOutDated; set => Set(ref isOutDated, value); }
        [Required]
        public List<Platform> Platforms { get => platforms; set => Set(ref platforms, value); }
        [Required]
        public Account Account { get => account; set => Set(ref account, value); }
        [Required]
        public Work Work { get => work; set => Set(ref work, value); }
    }
}
