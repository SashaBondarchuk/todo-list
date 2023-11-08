using System.Security.Cryptography;

namespace TodoList.Common.Auth.Helpers
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using Rfc2898DeriveBytes pbkdf2 = new(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, 20);

            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public static bool VerifyPassword(string hashedPassword, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SaltSize];

            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using Rfc2898DeriveBytes pbkdf2 = new(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] testHash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + SaltSize] != testHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}