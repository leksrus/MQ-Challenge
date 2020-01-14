using Api.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services
{
    public class CryptoManager : ICryptoManager
    {
        public string GetMD5Hash(string stringToHash)
        {
            using var md5 = MD5.Create();
            var bytes = new UnicodeEncoding().GetBytes(stringToHash.Trim());
            var hash = md5.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
