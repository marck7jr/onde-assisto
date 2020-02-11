using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OndeAssisto.Common.Models
{
   public class Platform : Entity
    {
        string nome;
        string logo;
        string site;

        //[]atributos de validacao
        [Required]public string Nome
        {
            get => nome;

            set => Set(ref nome, value);
        }
        public string Logo 
        {
            get => logo;

            set => Set(ref nome, value);
        }
        public string Site
        {
            get => site;

            set => Set(ref nome, value);
        }


        
    }
}
