using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHashHandler
{
    private const int SaltSize = 16; // 128 bit
    private const int KeySize = 32;  // 256 bit
    private const int Iterations = 100_000; // Recommended minimum

    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

    public static string HashPassword(string password)
    {
        // Generate salt
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        // Derive key
        var key = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            HashAlgorithm,
            KeySize);

        // Combine salt + key into one string (Base64 encoded)
        var outputBytes = new byte[SaltSize + KeySize];
        Buffer.BlockCopy(salt, 0, outputBytes, 0, SaltSize);
        Buffer.BlockCopy(key, 0, outputBytes, SaltSize, KeySize);

        return Convert.ToBase64String(outputBytes);
    }

    public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        var decoded = Convert.FromBase64String(hashedPassword);

        // Extract salt
        var salt = new byte[SaltSize];
        Buffer.BlockCopy(decoded, 0, salt, 0, SaltSize);

        // Extract original key
        var originalKey = new byte[KeySize];
        Buffer.BlockCopy(decoded, SaltSize, originalKey, 0, KeySize);

        // Derive key from provided password using the same salt
        var newKey = Rfc2898DeriveBytes.Pbkdf2(
            providedPassword,
            salt,
            Iterations,
            HashAlgorithm,
            KeySize);

        // Compare the original key with the newly derived one
        return CryptographicOperations.FixedTimeEquals(originalKey, newKey);
    }
}
