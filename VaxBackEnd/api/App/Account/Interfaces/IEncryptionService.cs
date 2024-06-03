using System;

namespace api.App.Account.Interfaces
{
    public interface IEncryptionService
    {
         string GenerateKey();
        string Encrypt(string plaintext, string key);
        string Decrypt(string ciphertext, string key);

    }
}
