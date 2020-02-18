using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    public class Genre : Entity
    {
        private string name;

        [Required]
        public string Name { get => name; set => Set(ref name, value); }
    }
}
