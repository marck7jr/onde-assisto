using System;
using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    public class Work : Entity
    {
        private string name;
        private string description;
        private Author author;
        private string cover;
        private DateTime releaseYear;

        [Required]
        public string Name { get => name; set => Set(ref name, value); }
        public string Description { get => description; set => Set(ref description, value); }
        [Required]
        public Author Author { get => author; set => Set(ref author, value); }
        public string Cover { get => cover; set => Set(ref cover, value); }
        public DateTime ReleaseYear { get => releaseYear; set => Set(ref releaseYear, value); }
    }
}
