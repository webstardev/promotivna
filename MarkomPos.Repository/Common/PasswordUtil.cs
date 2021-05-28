using MarkomPos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Common
{
    public class PasswordUtil
    {
        const int hashIterations = 100;
        const int saltLength = 32;

        public static HashSaltVm GetHashSaltPassword(string plainPassword)
        {
            byte[] saltBytes = GenerateRandomCryptographicBytes(saltLength);
            var derivedBytesPwd = new Rfc2898DeriveBytes(plainPassword, saltBytes, hashIterations);
            byte[] hashBytes = derivedBytesPwd.GetBytes(32);
            return new HashSaltVm(Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }

        public static bool VerifyPassword(string plainPassword, string hashToCompareWithBase64, string saltBase64)
        {
            byte[] saltBytes = Convert.FromBase64String(saltBase64);
            var derivedBytesPwd = new Rfc2898DeriveBytes(plainPassword, saltBytes, hashIterations);
            byte[] hashBytes = derivedBytesPwd.GetBytes(32);
            string passwordHashBase64 = Convert.ToBase64String(hashBytes);
            return hashToCompareWithBase64 == passwordHashBase64;
        }

        private static byte[] GenerateRandomCryptographicBytes(int keyLength)
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }
    }
}
