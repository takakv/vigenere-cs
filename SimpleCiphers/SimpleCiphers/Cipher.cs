using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SimpleCiphers
{
    public static class Cipher
    {
        internal const int Modulus = byte.MaxValue + 1;

        // Combining encryption and decryption into one function, which handles
        // both mono- and polyalphabetic substitutions, is perhaps not the most
        // elegant solution, but avoids code duplication/repeated function calls.
        private static byte[] ProcessByteCipher(byte[] bytes, IReadOnlyList<byte> key,
            Actions action)
        {
            int keyLength = key.Count, keyIndex = 0;
            int shift = key[0];

            for (var i = 0; i < bytes.Length; ++i)
            {
                // Conditional check not necessarily needed,
                // however I like it for clarity.
                if (keyLength > 1)
                    shift = key[++keyIndex % keyLength];

                bytes[i] = action switch
                {
                    Actions.Encrypt => (byte)((bytes[i] + shift) % Modulus),
                    Actions.Decrypt => (byte)((bytes[i] - shift + Modulus) % Modulus),
                    _ => throw new InvalidEnumArgumentException("Invalid cipher argument")
                };
            }

            return bytes;
        }

        internal static void Encipher(string message, string key)
        {
            byte[] byteStream = ProcessByteCipher(Encoding.UTF8.GetBytes(message),
                Encoding.UTF8.GetBytes(key), Actions.Encrypt);

            Console.WriteLine($"The ciphertext is {Convert.ToBase64String(byteStream)}");
        }

        internal static void Decipher(string ciphertext, string key)
        {
            byte[] byteStream = ProcessByteCipher(Convert.FromBase64String(ciphertext),
                Encoding.UTF8.GetBytes(key), Actions.Decrypt);

            Console.WriteLine("The message is:");
            Console.WriteLine($"- base64: {Convert.ToBase64String(byteStream)}");
            Console.WriteLine($"- plain: {Encoding.UTF8.GetString(byteStream)}");
        }

        private enum Actions
        {
            Encrypt,
            Decrypt
        }
    }
}