using System;
using System.Security.Cryptography;
using System.Text;

namespace cineweb_user_api.Util
{
    public class Criptography : ICriptography
    {
        public string Decrypt(string value)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string value)
        {
            using (var md5Hash = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(value);

                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash;
            }
        }
    }
}
