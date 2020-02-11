using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
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

            set => Set(ref name, value);
        }
        public string Site
        {
            get => site;

            set => Set(ref name, value);
        }
    }
}
