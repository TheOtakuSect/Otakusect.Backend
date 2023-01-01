using System.Security.Cryptography;
using System.Text;

namespace OtakuSect.BussinessLayer.Helper
{
    public static class PasswordHasher
    {
        public static string Password2hash(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] hash = hashAlgorithm.ComputeHash(passwordBytes);
            string hashString = BitConverter.ToString(hash).Replace("-", string.Empty);
            return hashString;
        }
    }
}
