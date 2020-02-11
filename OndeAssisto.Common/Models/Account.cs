using OndeAssisto.Common.Contracts.Jwt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OndeAssisto.Common.Models
{
    public enum AccountRoleType
    {
        Administrator,
        Moderator,
        User
    }

    public class Account : Entity, IValidatableObject, IJwtCredentialsIdentity, IJwtClaimsIdentity
    {
        private string name;
        private string email;
        private string password;
        private string role;

        public string Name { get => name; set => Set(ref name, value); }
        [Required, EmailAddress]
        public string Email { get => email; set => Set(ref email, value); }
        //limitando campo de texto 
        [Required, StringLength(maximumLength: 64, MinimumLength = 8)]
        public string Password { get => password; set => Set(ref password, value); }
        [Required]
        public string Role { get => role; set => Set(ref role, value); }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.IsDefined(typeof(AccountRoleType), Role))
            {
                yield return new ValidationResult($"Invalid role type: {role}", new[]
                {
                    nameof(Role)
                });
            }
        }
    }
}
