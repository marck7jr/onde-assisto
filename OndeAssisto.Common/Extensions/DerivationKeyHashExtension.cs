using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace OndeAssisto.Common.Extensions
{
    public static class DerivationKeyHashExtension
    {
        public static string GetDerivationKeyHash(this string target, byte[] salt = null)
        {
            if (salt is null)
            {
                salt = new byte[128 / 8];
                using var random = RandomNumberGenerator.Create();
                random.GetBytes(salt);
            }

            return Convert.ToBase64String
            (
                KeyDerivation.Pbkdf2
                (
                    password: target,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8

                )
            );
        }

        public static void GetDerivationKeyHash<T>(this T target, string passwordPropertyName, string saltPropertyName) where T : class
        {
            var passwordProperty = typeof(T).GetProperty(passwordPropertyName);
            var saltProperty = typeof(T).GetProperty(saltPropertyName);

            if (!(passwordProperty.PropertyType == typeof(string) && (saltProperty.PropertyType == typeof(string))))
            {
                throw new InvalidOperationException("The given parameters must be a reference to a string type.");
            }

            var password = passwordProperty.GetValue(target).ToString();
            var salt = Encoding.UTF8.GetBytes(saltProperty.GetValue(target).ToString());

            passwordProperty.SetValue(target, password.GetDerivationKeyHash(salt));
        }
    }
}
