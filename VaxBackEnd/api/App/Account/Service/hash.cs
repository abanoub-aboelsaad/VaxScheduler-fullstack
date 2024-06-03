    // public virtual string HashPassword(TUser user, string password)
    // {
    //     ArgumentNullThrowHelper.ThrowIfNull(password);

    //     if (_compatibilityMode == PasswordHasherCompatibilityMode.IdentityV2)
    //     {
    //         return Convert.ToBase64String(HashPasswordV2(password, _rng));
    //     }
    //     else
    //     {
    //         return Convert.ToBase64String(HashPasswordV3(password, _rng));
    //     }
    // }

    // private static byte[] HashPasswordV2(string password, RandomNumberGenerator rng)
    // {
    //     const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
    //     const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
    //     const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
    //     const int SaltSize = 128 / 8; // 128 bits

    //     // Produce a version 2 (see comment above) text hash.
    //     byte[] salt = new byte[SaltSize];
    //     rng.GetBytes(salt);
    //     byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

    //     var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
    //     outputBytes[0] = 0x00; // format marker
    //     Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
    //     Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
    //     return outputBytes;
    // }

 
    // * Version 2:
    //  * PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations.
    //  * (See also: SDL crypto guidelines v5.1, Part III)
    //  * Format: { 0x00, salt, subkey }