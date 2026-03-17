using System.Security.Cryptography;

namespace aeroWebApi.Services
{
    public class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Iterations = 10000;

        private readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256; 
        private const char Delimiter = ':';


        public string HashPassword (string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);
            return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool VerifyPassword(string passwordHash, string inputPassword)
        {
            var parts = passwordHash.Split(Delimiter);
            if (parts.Length != 2)
                throw new FormatException("Unexpected hash format. Should be 'salt:hash'");
            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, HashAlgorithm, KeySize);

            return CryptographicOperations.FixedTimeEquals(hashInput, hash);
        }
    }
}   