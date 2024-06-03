using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using api.App.Account.Interfaces; 

namespace api.App.Account.Service
{
    public class EncryptionService : IEncryptionService
    {
        private const int KeySize = 256;

        public string GenerateKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[KeySize / 8];
                rng.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }
        }




        public string Encrypt(string plaintext, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes;

                using (var msEncrypt = new MemoryStream())
                {
                    
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
                        csEncrypt.Write(plaintextBytes, 0, plaintextBytes.Length);
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }

                return Convert.ToBase64String(encryptedBytes);
            }
        }













public string Decrypt(string ciphertext, string key)
{
    try
    {
        byte[] cipherBytes = Convert.FromBase64String(ciphertext);

        using (Aes aesAlg = Aes.Create())
        {
            
            byte[] iv = new byte[aesAlg.BlockSize / 8];
            Array.Copy(cipherBytes, iv, iv.Length);
            aesAlg.IV = iv;

            aesAlg.Key = Convert.FromBase64String(key);
            aesAlg.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            string plaintext;

            using (var msDecrypt = new MemoryStream(cipherBytes, iv.Length, cipherBytes.Length - iv.Length))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    plaintext = srDecrypt.ReadToEnd();
                }
            }

            return plaintext;
        }
    }
    catch (FormatException ex)
    {
      
        Console.WriteLine("Invalid Base64 string format: " + ex.Message);
        return null; 
    }
}


    }
}
