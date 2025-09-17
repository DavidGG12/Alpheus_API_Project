using Alpheus_API.Helpers.Crypto.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Alpheus_API.Helpers.Crypto
{
    public class Crypto : ICrypto
    {
        private static Crypto? _instance;

        private readonly string _secretKey;
        private readonly string _secretSalt;

        private Crypto(string secretKey, string secretSalt)
        {
            _secretKey = secretKey;
            _secretSalt = secretSalt;
        }

        public static Crypto GetInsCrypto()
        {
            if(_instance != null)
                return _instance;

            try
            {
                string? secretKey = Environment.GetEnvironmentVariable("API_SECRET_KEY");
                string? secretSalt = Environment.GetEnvironmentVariable("API_SECRET_SALT");

                if (string.IsNullOrEmpty(secretKey))
                    throw new InvalidOperationException("Can't recover Secret Key value.");
                if (string.IsNullOrEmpty(secretSalt))
                    throw new InvalidOperationException("Can't recover Secret Salt value.");

                _instance = new Crypto(secretKey, secretSalt);

                return _instance;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CloseInsCrypto()
        {
            if (_instance != null)
                _instance = null;
        }

        public string Encrypt(string word)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] saltBytes = Encoding.UTF8.GetBytes(_secretSalt);
                var key = new Rfc2898DeriveBytes(_secretKey, saltBytes, 10000);
                aes.Key = key.GetBytes(32);
                aes.IV = key.GetBytes(16);

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                        sw.Write(word);

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string word)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] saltBytes = Encoding.UTF8.GetBytes(_secretSalt);
                    var key = new Rfc2898DeriveBytes(_secretKey, saltBytes, 10000);
                    aes.Key = key.GetBytes(32);
                    aes.IV = key.GetBytes(16);

                    byte[] buffer = Convert.FromBase64String(word);

                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var ms = new MemoryStream(buffer))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                        return sr.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
